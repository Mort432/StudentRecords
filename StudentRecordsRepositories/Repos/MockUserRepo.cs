using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockUserRepo : MockRepo<User>, IUserRepo
    {
        #region mock datasource

        public MockUserRepo()
        {
            User user1 = new User
            {
                UniversityId = "s0000001",
                FirstName = "Connagh",
                LastName = "Muldoon",
                Role = UserRole.Student
            };
            User user2 = new User
            {
                UniversityId = "s0000002",
                FirstName = "Thomas",
                LastName = "Clark",
                Role = UserRole.Student
            };
            User user3 = new User
            {
                UniversityId = "s0000003",
                FirstName = "Abu",
                LastName = "Alam",
                Role = UserRole.Student
            };

            Insert(user1);
            Insert(user2);
            Insert(user3);
        }
        #endregion
    }
}
