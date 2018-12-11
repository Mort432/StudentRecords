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
        public User selectedStudent;

        private IAuthService _authService;
        private ILecturerService _lecturerService;
        private IUsersService _usersService;
        private IAssignmentsService _assignmentsService;
        private IResultsService _resultsService;

        public LecturerStudentManagementViewModel(IAuthService authService, ILecturerService lecturerService, IUsersService usersService, IAssignmentsService assignmentsService, IResultsService resultsService)
        {
            _authService = authService;
            _lecturerService = lecturerService;
            _usersService = usersService;
            _assignmentsService = assignmentsService;
            _resultsService = resultsService;

            selectedLecturer = _authService.authorisedUser;
        }

        public List<User> GetLecturerStudents()
        {
            return _lecturerService.GetLecturerStudents(selectedLecturer);
        }

        public List<User> lecturerStudents
        {
            get
            {
                return GetLecturerStudents();
            }
        }

        public List<Assignment> GetStudentAssignments
        {
            get
            {
                if(selectedStudent == null)
                {
                    return new List<Assignment>();
                }
                List<Assignment> assignments = _assignmentsService.GetUserAssignments(selectedStudent).ToList();
                List<Result> results = _resultsService.GetUserResults(selectedStudent).ToList();
                foreach (Assignment assignment in assignments)
                {
                    assignment.Results = new List<Identifier>(results.Where(x => x.Assignment.Id.Equals(assignment.Id)).Select(x => x.ToIdentifier()));
                }

                return assignments;
            }
        }

        public bool StudentOnLecturerCourse(User student)
        {
            if (student == null) return false;
            if (student.Course.Equals(selectedLecturer.Course)) return true;
            return false;
        }

        public void UpdateUserGraduation(bool graduation)
        {
            selectedStudent.Graduated = graduation;
            _usersService.UpdateUser(selectedStudent);
        }

        public void DeleteResult(Assignment assignment)
        {
            //Does this need a cascading delete? Double check
            _resultsService.DeleteResultByIdentifier(assignment.Results.ElementAt(0));
        }

        public void AssignResult(Assignment assignment, int grade)
        {
            
            _resultsService.AssignResult(selectedStudent, assignment, grade);
        }
    }
}
