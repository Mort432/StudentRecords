using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    //Defines the functionality an Assignment repo must implement.
    public interface IAssignmentRepo : IRepo<Assignment>
    {
        //Given a user, return all the assignments in module runs they're enrolled to.
        List<Assignment> GetUserAssignments(User user);
    }
}
