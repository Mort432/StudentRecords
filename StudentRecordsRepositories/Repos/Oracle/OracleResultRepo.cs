using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleResultRepo : OracleRepo<Result>, IResultRepo
    {
        public Identifier GetExistingResult(Assignment assignment, User student)
        {
            throw new NotImplementedException();
        }

        public List<Result> GetModuleRunsResults(List<ModuleRun> moduleRuns)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            throw new NotImplementedException();
        }
    }
}
