using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleModuleRunRepo : OracleRepo<ModuleRun>, IModuleRunRepo
    {
        public List<ModuleRun> GetLecturerModuleRuns(User lecturer)
        {
            throw new NotImplementedException();
        }
    }
}
