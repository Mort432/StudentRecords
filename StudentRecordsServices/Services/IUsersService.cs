using StudentRecordsServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    public interface IUsersService
    {
        User GetUser(string UserId);

        IEnumerable<User> GetAllUsers();

        void AddUser(User newUser);
    }
}
