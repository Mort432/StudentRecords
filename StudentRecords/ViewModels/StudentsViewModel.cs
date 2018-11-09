using StudentRecords.Models;
using StudentRecords.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecords.ViewModels
{
    class StudentsViewModel
    {
        public List<User> users = new List<User>();

        private IUsersService _usersService;

        public StudentsViewModel(IUsersService usersService)
        {
            _usersService = usersService;

            users = _usersService.GetAllUsers().ToList();
        }


    }
}
