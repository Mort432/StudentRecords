using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsViewModels.ViewModels
{
    public class LecturerStudentManagementViewModel
    {
        public User selectedLecturer;
        public User selectedStudent { get; set; }

        //Inject dependancies
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

        //Exposes lecturerStudents
        public List<User> lecturerStudents
        {
            get
            {
                return GetLecturerStudents();
            }
        }

        //Get the selected student's assignments
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

        //Check if the student is on the lecturer's course.
        //Only course leaders can graduate a student.
        public bool StudentOnLecturerCourse
        {
            get {
                if (selectedStudent == null) return false;
                if (selectedStudent.Course == null) return false;
                if (selectedStudent.Course.Equals(selectedLecturer.Course)) return true;
                return false;
            }
        }

        public void UpdateUserGraduation(bool graduation)
        {
            selectedStudent.Graduated = graduation;
            _usersService.UpdateUser(selectedStudent);
        }

        //Prepare to assign a new result
        public void NewResult(Assignment assignment, string grade)
        {
            int gradeValue;
            if (grade == "Cancel")
            {
                //Do nothing if cancelled
                return;
            }
            else if (string.IsNullOrEmpty(grade))
            {
                if (assignment.Results.ElementAt(0) == null)
                {
                    //If there isn't a result yet, don't do anything.
                    return;
                }
                //If the submitted string was empty, delete their grade.
                DeleteResult(assignment);
            }
            else if (int.TryParse(grade, out gradeValue)) //Ensure input parses to integer before proceeding
            {
                //Assign result given
                AssignResult(assignment, gradeValue);
            }

        }

        private void DeleteResult(Assignment assignment)
        {
            _resultsService.DeleteResultByIdentifier(assignment.Results.ElementAt(0));
        }

        private void AssignResult(Assignment assignment, int grade)
        {
            
            _resultsService.AssignResult(selectedStudent, assignment, grade);
        }
    }
}
