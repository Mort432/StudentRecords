using StudentRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecords.Services
{
    interface IUsersService
    {
        User GetUser(string UserId);

        IEnumerable<User> GetAllUsers();

        void AddUser(User newUser);
    }
}
