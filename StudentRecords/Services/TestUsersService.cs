using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecords.Models;
using StudentRecords.Models.Enums;

namespace StudentRecords.Services
{
    class TestUsersService : IUsersService
    {
        List<User> usersList = new List<User>();

        TestUsersService()
        {
            //INIT TEST USERS
            #region Test users init
            User user1 = new User
            {
                UserId = "s0000001",
                FirstName = "Connagh",
                LastName = "Muldoon",
                Role = UserRole.Student
            };
            User user2 = new User
            {
                UserId = "s0000002",
                FirstName = "Thomas",
                LastName = "Clarke",
                Role = UserRole.Student
            };
            User user3 = new User
            {
                UserId = "s0000003",
                FirstName = "Abu",
                LastName = "Alam",
                Role = UserRole.Student
            };
            #endregion
        }

        public void AddUser(User newUser)
        {
            usersList.Add(newUser);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return usersList;
        }

        public User GetUser(string UserId)
        {
            return usersList.Where(x => x.UserId == UserId).SingleOrDefault();
        }
    }
}
