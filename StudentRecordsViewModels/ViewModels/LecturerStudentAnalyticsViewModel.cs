using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System.Collections.Generic;

namespace StudentRecordsViewModels.ViewModels
{
    public class LecturerStudentAnalyticsViewModel
    {
        public User selectedLecturer;
        public int NoOfGraduatedStudents { get; }
        public List<Result> LecturerResults { get; }

        //Inject dependancies
        private IAuthService _authService;
        private ILecturerService _lecturerService;

        public LecturerStudentAnalyticsViewModel(IAuthService authService, ILecturerService lecturerService)
        {
            _authService = authService;
            _lecturerService = lecturerService;

            selectedLecturer = _authService.authorisedUser;

            NoOfGraduatedStudents = selectedLecturer.Course == null ?
                0 :
                _lecturerService.GetGraduatedStudents(selectedLecturer.Course);

            LecturerResults = _lecturerService.GetLecturerResults(selectedLecturer);
        }

        public List<Result> GetLecturerResults()
        {
            return _lecturerService.GetLecturerResults(selectedLecturer);
        }

        public bool LecturerHasCourse
        {
            get {
                if (selectedLecturer.Course == null)
                {
                    return false;
                }
                return true;
            }
            
        }
    }
}
