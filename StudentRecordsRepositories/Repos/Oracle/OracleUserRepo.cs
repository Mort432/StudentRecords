using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleUserRepo : OracleRepo<User>, IUserRepo
    {
        public int CountGraduatedCourseUsers(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllLecturers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
