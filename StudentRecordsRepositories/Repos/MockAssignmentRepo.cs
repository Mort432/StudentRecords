using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockAssignmentRepo : MockRepo<Assignment>, IAssignmentRepo
    {
        public MockAssignmentRepo()
        {
            Items = data.AssignmentsCollection;
            mockIdentityTracker = Items.Count + 1;
        }

        public List<Assignment> GetUserAssignments(User user)
        {
            return Select(x => user.Enrollments.Contains(x.ModuleRun)).Result.ToList();
        }
    }
}
