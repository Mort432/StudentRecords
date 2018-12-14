using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mongo
{
    public class MongoUserRepo : MongoRepo<User>, IUserRepo
    {
        protected override IMongoCollection<User> Collection => Users;

        public Task<IEnumerable<User>> GetAllLecturers()
        {
            //Select where role is lecturer
            return Select(x => x.Role == UserRole.Lecturer);
        }

        public Task<IEnumerable<User>> GetAllStudents()
        {
            //Select where role is student
            return Select(x => x.Role == UserRole.Student);
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            //Select students on lecturer course
            var courseStudents = Select(x => (x.Course != null) && lecturer.Course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student).Result.ToList();
            //Select students in lecturer modules
            var moduleStudents = Select(user => user.Enrollments.Any(userEnrollment => lecturer.Enrollments.Contains(userEnrollment)) && user.Role == UserRole.Student).Result;
            //Merge both into one collection
            var all = new List<User>();
            all.AddRange(courseStudents);
            all.AddRange(moduleStudents);
            //Eliminate duplicates.
            var myStudents = all.GroupBy(x => x.Id).Select(g => g.First());

            return myStudents.ToList();
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            //Select where users course = course
            return Select(x => (x.Course != null) && course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student).Result.ToList();
        }

        public int CountGraduatedCourseUsers(Course course)
        {
            //return GetUsersFromCourse(course).Where(x => x.Graduated).Count();


            // map function
            var map = new BsonJavaScript(@"
                function() {
                    emit(this.course, 1);
                }
            ");

            // reduce function
            var reduce = new BsonJavaScript(@"
                function(key, values) {
                    return Array.sum(values);
                }
            ");

            // map reduce filters
            var filter = Builders<User>.Filter.Eq(x => x.Graduated, true) & Builders<User>.Filter.Eq(x => x.Course.Id, course.Id);

            // map reduce finalize
            var finalize = new BsonJavaScript(@"
                function(key, value) {
                    return parseInt(value);
                }
            ");

            // map reduce options
            var options = new MapReduceOptions<User, BsonDocument>
            {
                Filter = filter,
                Finalize = finalize
            };

            // execute and retrieve
            var cursor = Collection.MapReduceAsync(map, reduce, options).Result;

            var studentsPassed = cursor.ToList();

            // parse to dictionary
            var dictionary = studentsPassed.ToDictionary(
                bson => BsonSerializer.Deserialize<Identifier>(bson.GetValue("_id").ToBsonDocument()),
                bson => bson.GetValue("value").ToInt32()
            );

            // get count, or return none
            var count = dictionary.Select(x => x.Value).SingleOrDefault();

            return count;
        }

        public override void Update(User item)
        {
            //Perform base update
            base.Update(item);
            //Update enrollments students.
            UpdateEnrollmentsStudents(item);
            //Update course.
            UpdateCourseStudents(item);
        }

        private void UpdateEnrollmentsStudents(User student)
        {
            //Get student identifier
            var identifier = student.ToIdentifier();
            
            //Get modules runs for that student
            var moduleRunStudentsFilter = Builders<ModuleRun>.Filter.AnyEq(x => x.Students, identifier);
            var oldIds = ModuleRuns.Find(moduleRunStudentsFilter).Project(x => x.Id).ToList();
            //Get student enrollments
            var studentEnrollmentsFilter = Builders<ModuleRun>.Filter.In(x => x.Id, student.Enrollments.Select(x => x.Id));
            var newIds = ModuleRuns.Find(studentEnrollmentsFilter).Project(x => x.Id).ToList();

            //Remove enrollments not present, ignoring enrollments we want to insert
            oldIds.Except(newIds).ToList().ForEach(runId =>
            {
                var moduleRunIdFilter = Builders<ModuleRun>.Filter.Eq(x => x.Id, runId);
                var moduleRunStudentsUpdate = Builders<ModuleRun>.Update.Pull(x => x.Students, identifier);
                ModuleRuns.UpdateOne(moduleRunIdFilter, moduleRunStudentsUpdate);
            });
            //Insert new set of enrollments, ignoring enrollments already present
            newIds.Except(oldIds).ToList().ForEach(runId =>
            {
                var moduleRunIdFilter = Builders<ModuleRun>.Filter.Eq(x => x.Id, runId);
                var moduleRunStudentsUpdate = Builders<ModuleRun>.Update.Push(x => x.Students, identifier);
                ModuleRuns.UpdateOne(moduleRunIdFilter, moduleRunStudentsUpdate);
            });
        }

        private void UpdateCourseStudents(User student)
        {
            //Student as identifier
            var identifier = student.ToIdentifier();

            //Get student course
            var courseStudentsFilter = Builders<Course>.Filter.AnyEq(x => x.Students, identifier);
            var oldId = Courses.Find(courseStudentsFilter).Project(x => x.Id).SingleOrDefault();

            var newId = student.Course?.Id;

            if (oldId != null && newId != null && oldId.Equals(newId))
            {
                //Update the course
                var courseStudentsUpdate = Builders<Course>.Update.Set(x => x.Students[-1], identifier);
                Courses.UpdateOne(courseStudentsFilter, courseStudentsUpdate);
            }
            else
            {
                if (oldId != null)
                {
                    //Remove the student enrollment
                    var courseIdFilter = Builders<Course>.Filter.Eq(x => x.Id, oldId);
                    var courseStudentsUpdate = Builders<Course>.Update.Pull(x => x.Students, identifier);
                    Courses.UpdateOne(courseIdFilter, courseStudentsUpdate);
                }

                if (newId != null)
                {
                    //Add the student enrollment
                    var courseIdFilter = Builders<Course>.Filter.Eq(x => x.Id, newId);
                    var courseStudentsUpdate = Builders<Course>.Update.Push(x => x.Students, identifier);
                    Courses.UpdateOne(courseIdFilter, courseStudentsUpdate);
                }
            }
        }
    }
}
