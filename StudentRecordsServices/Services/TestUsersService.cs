using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using StudentRecordsRepositories.Repos;

namespace StudentRecordsServices.Services
{
    public class TestUsersService : IUsersService
    {
        List<User> usersList = new List<User>();

        public TestUsersService()
        {
            //INIT TEST USERS
            #region Test users init
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
            #endregion

            usersList.Add(user1);
            usersList.Add(user2);
            usersList.Add(user3);
        }

        public void AddUser(User newUser)
        {
            usersList.Add(newUser);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return usersList;
        }

        public User GetUserById(object UserId)
        {
            return usersList.Where(x => x.Id == UserId).SingleOrDefault();
        }
    }
}
