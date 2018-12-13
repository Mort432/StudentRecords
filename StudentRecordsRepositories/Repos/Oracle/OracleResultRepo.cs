using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleResultRepo : OracleRepo<Result>, IResultRepo
    {
        public override string SelectCommandText => throw new NotImplementedException();

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();

        public override string Table => throw new NotImplementedException();

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

        public override Result ToModel(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override OracleParameter[] ToOracleParameters(Result item)
        {
            throw new NotImplementedException();
        }
    }
}
