using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsViewModels.ViewModels
{
    public class StudentProfileViewModel
    {
        public User selectedStudent;
        public string StudentCourseFormattedString { get { return "(" + selectedStudent.Course + ")"; } }

        private IAuthService _authService;
        private IAssignmentsService _assignmentsService;
        private IResultsService _resultsService;

        public StudentProfileViewModel(IAuthService authService, IAssignmentsService assignmentsService, IResultsService resultsService)
        {
            _authService = authService;
            _assignmentsService = assignmentsService;
            _resultsService = resultsService;

            selectedStudent = _authService.authorisedUser;
        }

        public List<Assignment> GetStudentAssignments { get
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
}
