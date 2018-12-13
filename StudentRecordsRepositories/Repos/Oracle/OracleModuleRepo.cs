using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleModuleRepo : OracleRepo<Module>, IModuleRepo
    {
        public override string SelectCommandText => throw new NotImplementedException();

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();

        public override string Table => throw new NotImplementedException();

        public override Module ToModel(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override OracleParameter[] ToOracleParameters(Module item)
        {
            throw new NotImplementedException();
        }
    }
}
