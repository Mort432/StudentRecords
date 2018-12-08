using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsUI.ViewModels
{
    public class StudentsViewModel
    {
        public User selectedStudent;
        public string StudentCourseFormattedString { get { return "(" + selectedStudent.Course + ")"; } }

        private IAuthService _authService;

        public StudentsViewModel(IAuthService authService)
        {
            _authService = authService;

            selectedStudent = _authService.authorisedUser;
        }
    }
}
