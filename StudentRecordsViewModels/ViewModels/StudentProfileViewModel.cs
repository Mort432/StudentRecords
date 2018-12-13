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
        public ICollection<Assignment> StudentAssignments { get; }

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

        public List<Assignment> GetStudentAssignments()
        {
            List<Assignment> assignments = _assignmentsService.GetUserAssignments(selectedStudent).ToList();
            List<Result> results = _resultsService.GetUserResults(selectedStudent).ToList();
            foreach(Assignment assignment in assignments)
            {
                assignment.Results = new List<Identifier>(results.Where(x => x.Assignment.Id.Equals(assignment.Id)).Select(x => x.ToIdentifier()));
            }

            return assignments;
        }
    }
}
