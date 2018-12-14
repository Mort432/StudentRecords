using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System.Collections.Generic;
using System.Linq;

namespace StudentRecordsViewModels.ViewModels
{
    public class StudentProfileViewModel
    {
        public User selectedStudent { get; }
        public string StudentCourseFormattedString => $"({selectedStudent.Course})";
        public ICollection<Assignment> StudentAssignments { get; private set; }
        public ICollection<Result> StudentResults { get; private set; }

        //Inject dependancies
        private IAuthService _authService;
        private IAssignmentsService _assignmentsService;
        private IResultsService _resultsService;

        public StudentProfileViewModel(IAuthService authService, IAssignmentsService assignmentsService, IResultsService resultsService)
        {
            _authService = authService;
            _assignmentsService = assignmentsService;
            _resultsService = resultsService;

            selectedStudent = _authService.authorisedUser;
            StudentAssignments = GetStudentAssignments();
        }

        //On page load, get all the student's assignments.
        public List<Assignment> GetStudentAssignments()
        {
            var results = StudentResults = _resultsService.GetUserResults(selectedStudent).ToList();

            var assignments = _assignmentsService.GetUserAssignments(selectedStudent).ToList();

            foreach (var assignment in assignments)
            {
                //Fetch the results in addition.
                assignment.Results = new List<Identifier>(results.Where(x => x.Assignment.Id.Equals(assignment.Id)).Select(x => x.ToIdentifier()));
            }

            return assignments;
        }
    }
}
