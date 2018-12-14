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
        //Inject dependancies
        public IAssignmentRepo _assignmentRepo;

        public AssignmentsService(IAssignmentRepo assignmentRepo)
        {
            _assignmentRepo = assignmentRepo;
        }

        //Get a user's assignments
        public IEnumerable<Assignment> GetUserAssignments(User user)
        {
            return _assignmentRepo.GetUserAssignments(user);
        }
    }
}
