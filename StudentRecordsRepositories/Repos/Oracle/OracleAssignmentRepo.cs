using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleAssignmentRepo : OracleRepo<Assignment>, IAssignmentRepo
    {
        public List<Assignment> GetUserAssignments(User user)
        {
            throw new NotImplementedException();
        }
    }
}
