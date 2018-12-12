using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public interface IAssignmentRepo : IRepo<Assignment>
    {
        List<Assignment> GetUserAssignments(User user);
    }
}
