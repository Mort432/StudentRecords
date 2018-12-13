using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleAssignmentRepo : OracleRepo<Assignment>, IAssignmentRepo
    {
        public override string Table => Assignments;

        public List<Assignment> GetUserAssignments(User user)
        {
            throw new NotImplementedException();
        }

        public override Assignment ToModel(DbDataReader reader)
        {
            throw new NotImplementedException();
        }

        public override OracleParameter[] ToOracleParameters(Assignment item)
        {
            throw new NotImplementedException();
        }

        public override string SelectCommandText => throw new NotImplementedException();

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
