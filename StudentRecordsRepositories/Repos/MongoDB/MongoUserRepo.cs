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
            return Select(x => x.Role == UserRole.Lecturer);
        }

        public Task<IEnumerable<User>> GetAllStudents()
        {
            return Select(x => x.Role == UserRole.Student);
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            var courseStudents = Select(x => (x.Course != null) && lecturer.Course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student);
            var moduleStudents = Select(x => lecturer.Enrollments.Any(z => z.Id.Equals(x.Id)));
            var myStudents = courseStudents.Result.ToList().Union(moduleStudents.Result.ToList()).ToList();

            return myStudents;
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            return Select(x => course.Students.Any(z => z.Id.Equals(x.Id))).Result.ToList();
        }

        public int CountGraduatedCourseUsers(Course course)
        {
            return GetUsersFromCourse(course).Where(x => x.Graduated).Count();
        }

        public override void Update(User item)
        {
            base.Update(item);

            UpdateEnrollmentsStudents(item);

            UpdateCourseStudents(item);
        }

        private void UpdateEnrollmentsStudents(User student)
        {
            var identifier = student.ToIdentifier();

            var moduleRunStudentsFilter = Builders<ModuleRun>.Filter.AnyEq(x => x.Students, identifier);
            var oldIds = ModuleRuns.Find(moduleRunStudentsFilter).Project(x => x.Id).ToList();

            var studentEnrollmentsFilter = Builders<ModuleRun>.Filter.In(x => x.Id, student.Enrollments.Select(x => x.Id));
            var newIds = ModuleRuns.Find(studentEnrollmentsFilter).Project(x => x.Id).ToList();

            oldIds.Except(newIds).ToList().ForEach(runId =>
            {
                var moduleRunIdFilter = Builders<ModuleRun>.Filter.Eq(x => x.Id, runId);
                var moduleRunStudentsUpdate = Builders<ModuleRun>.Update.Pull(x => x.Students, identifier);
                ModuleRuns.UpdateOne(moduleRunIdFilter, moduleRunStudentsUpdate);
            });

            newIds.Except(oldIds).ToList().ForEach(runId =>
            {
                var moduleRunIdFilter = Builders<ModuleRun>.Filter.Eq(x => x.Id, runId);
                var moduleRunStudentsUpdate = Builders<ModuleRun>.Update.Push(x => x.Students, identifier);
                ModuleRuns.UpdateOne(moduleRunIdFilter, moduleRunStudentsUpdate);
            });
        }

        private void UpdateCourseStudents(User student)
        {
            var identifier = student.ToIdentifier();

            var courseStudentsFilter = Builders<Course>.Filter.AnyEq(x => x.Students, identifier);
            var oldId = Courses.Find(courseStudentsFilter).Project(x => x.Id).SingleOrDefault();

            var newId = student.Course?.Id;

            if (oldId != null && newId != null && oldId.Equals(newId))
            {
                var courseStudentsUpdate = Builders<Course>.Update.Set(x => x.Students[-1], identifier);
                Courses.UpdateOne(courseStudentsFilter, courseStudentsUpdate);
            }
            else
            {
                if (oldId != null)
                {
                    var courseIdFilter = Builders<Course>.Filter.Eq(x => x.Id, oldId);
                    var courseStudentsUpdate = Builders<Course>.Update.Pull(x => x.Students, identifier);
                    Courses.UpdateOne(courseIdFilter, courseStudentsUpdate);
                }

                if (newId != null)
                {
                    var courseIdFilter = Builders<Course>.Filter.Eq(x => x.Id, newId);
                    var courseStudentsUpdate = Builders<Course>.Update.Push(x => x.Students, identifier);
                    Courses.UpdateOne(courseIdFilter, courseStudentsUpdate);
                }
            }
        }
    }
}
