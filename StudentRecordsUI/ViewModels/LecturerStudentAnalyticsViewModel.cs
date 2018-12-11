using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsUI.ViewModels
{
    public class LecturerStudentAnalyticsViewModel
    {
        public User selectedLecturer;

        private IAuthService _authService;
        private ILecturerService _lecturerService;

        public LecturerStudentAnalyticsViewModel(IAuthService authService, ILecturerService lecturerService)
        {
            _authService = authService;
            _lecturerService = lecturerService;

            selectedLecturer = _authService.authorisedUser;
        }

        public string GetGraduatedStudents()
        {
            //Return zero if the lecturer is not a course leader
            if (selectedLecturer.Course == null) return 0.ToString();

            return _lecturerService.GetGraduatedStudents(selectedLecturer.Course).ToString();
        }
    }
}
