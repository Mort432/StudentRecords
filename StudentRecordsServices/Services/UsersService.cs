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
        //Inject dependancies
        IUserRepo _userRepo;

        public UsersService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        //Get all users
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var allUsers = await _userRepo.SelectAll();
            return allUsers;
        }

        //Get all students
        public async Task<IEnumerable<User>> GetAllStudents(){
            return await _userRepo.GetAllStudents();
        }

        //Get all lecturers
        public async Task<IEnumerable<User>> GetAllLecturers()
        {
            return await _userRepo.GetAllLecturers();
        }

        //Get user by Id
        public async Task<User> GetUserById(object UserId)
        {
            return await _userRepo.SelectById(UserId);
        }

        //Update user.
        //This typically includes their enrollments, their course and their graduation status.
        public void UpdateUser(User user)
        {
            _userRepo.Update(user);
        }
    }
}
