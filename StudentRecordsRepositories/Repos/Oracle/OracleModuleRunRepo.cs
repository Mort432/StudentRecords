using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleModuleRunRepo : OracleRepo<ModuleRun>, IModuleRunRepo
    {
        public override string SelectCommandText => throw new NotImplementedException();

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();

        public override string Table => throw new NotImplementedException();

        public List<ModuleRun> GetLecturerModuleRuns(User lecturer)
        {
            throw new NotImplementedException();
        }

        public override ModuleRun ToModel(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override OracleParameter[] ToOracleParameters(ModuleRun item)
        {
            throw new NotImplementedException();
        }
    }
}
