using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsUI.ViewModels
{
    public class LecturerStudentManagementViewModel
    {
        public User selectedLecturer;

        private IAuthService _authService;
        private ILecturerService _lecturerService;

        public LecturerStudentManagementViewModel(IAuthService authService, ILecturerService lecturerService)
        {
            _authService = authService;
            _lecturerService = lecturerService;

            selectedLecturer = _authService.authorisedUser;
        }

        public List<User> GetLecturerStudents()
        {
            return _lecturerService.GetLecturerStudents(selectedLecturer);
        }
    }
}
