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

        public int CountGraduatedCourseUsers(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
