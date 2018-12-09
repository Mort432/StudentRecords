﻿using StudentRecordsModels.Models;
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

        Task<IEnumerable<User>> GetAllStudents();

        Task<IEnumerable<User>> GetAllLecturers();

        void AddUser(User newUser);

        void UpdateUser(User user);
    }
}
