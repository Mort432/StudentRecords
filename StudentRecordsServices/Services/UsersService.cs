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
    public class UsersService : IUsersService
    {
        IUserRepo _userRepo;

        public UsersService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public void AddUser(User newUser)
        {
            _userRepo.Insert(newUser);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepo.SelectAll();
        }

        public async Task<User> GetUserById(object UserId)
        {
            return await _userRepo.SelectById(UserId);
        }
    }
}
