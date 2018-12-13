using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleUserRepo : OracleRepo<User>, IUserRepo
    {
        public override string Table => Users;

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

        public override User ToModel(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override OracleParameter[] ToOracleParameters(User item)
        {
            throw new NotImplementedException();
        }

        public override string SelectCommandText => throw new NotImplementedException();

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
