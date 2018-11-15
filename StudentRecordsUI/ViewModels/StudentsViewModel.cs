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
        public List<User> students = new List<User>();
        public User selectedStudent;

        private IUsersService _usersService;

        public StudentsViewModel(IUsersService usersService)
        {
            _usersService = usersService;

            students = _usersService.GetAllStudents().Result.ToList();
        }

        public void UserSelected(int selectionIndex)
        {
            selectedStudent = students[selectionIndex];
        }
    }
}
