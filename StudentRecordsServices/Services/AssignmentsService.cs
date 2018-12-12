using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;
using StudentRecordsRepositories.Repos;

namespace StudentRecordsServices.Services
{
    public class AssignmentsService : IAssignmentsService
    {
        public IAssignmentRepo _assignmentRepo;

        public AssignmentsService(IAssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        public IEnumerable<Assignment> GetUserAssignments(User user)
        {
            return _assignmentRepo.GetUserAssignments(user);
        }
    }
}
