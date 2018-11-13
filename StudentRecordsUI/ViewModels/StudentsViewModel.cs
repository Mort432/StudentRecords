using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.ViewModels
{
    class StudentsViewModel
    {
        public List<User> users = new List<User>();
        public User selectedUser;

        private IUsersService _usersService;

        public StudentsViewModel(IUsersService usersService)
        {
            _usersService = usersService;

            users = _usersService.GetAllUsers().Result.ToList();
        }

        public void UserSelected(int selectionIndex)
        {
            selectedUser = users[selectionIndex];
        }
    }
}
