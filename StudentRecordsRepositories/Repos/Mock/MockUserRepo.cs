using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mock
{
    public class MockUserRepo : MockRepo<User>, IUserRepo
    {
        public MockUserRepo()
        {
            Items = data.UsersCollection;
            mockIdentityTracker = Items.Count + 1;
        }

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
            var moduleStudents = Select(user => user.Enrollments.Any(userEnrollment => lecturer.Enrollments.Contains(userEnrollment)) && user.Role == UserRole.Student);
            var myStudents = courseStudents.Result.ToList().Union(moduleStudents.Result.ToList()).ToList();

            return myStudents;
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            return Select(x => (x.Course != null) && course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student).Result.ToList();
        }

        public int CountGraduatedCourseUsers(Course course)
        {
            var users = GetUsersFromCourse(course).Where(x => x.Graduated == true).ToList();
            return users.Count();
        }
    }
}
