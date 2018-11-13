using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    public interface IUsersService
    {
        Task<User> GetUserById(object Id);

        Task<IEnumerable<User>> GetAllUsers();

        void AddUser(User newUser);
    }
}
