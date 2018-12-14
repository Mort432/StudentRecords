using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleAssignmentRepo : OracleRepo<Assignment>, IAssignmentRepo
    {
        public override string Table => Assignments;

        //FUNCTIONS
        public List<Assignment> GetUserAssignments(User user)
        {
            throw new NotImplementedException();
        }

        //OVERRIDES
        public override Assignment ToModel(DbDataReader reader)
        {
            var module = new Module
            {
                Id = reader.GetInt32(3),
                ModuleTitle = reader.GetString(4),
                ModuleCode = reader.GetString(5)
            }.ToIdentifier();
            var lecturer = new User
            {
                Id = reader.GetInt32(6),
                FirstName = reader.GetString(7),
                LastName = reader.GetString(8)
            }.ToIdentifier();
            var moduleRun = new ModuleRun
            {
                Id = reader.GetInt32(2),
                Lecturer = lecturer,
                Module = module
            }.ToIdentifier();
            var assignment = new Assignment
            {
                Id = reader.GetInt32(0),
                AssignmentName = reader.GetString(1),
                ModuleRun = moduleRun
            };

            return assignment;
        }

        public override OracleParameter[] ToOracleParameters(Assignment item)
        {
            return new OracleParameter[]
            {
                new OracleParameter("assignmentName", item.AssignmentName),
                new OracleParameter("moduleRun", item.ModuleRun.Id)
            };
        }

        public override async Task<IEnumerable<Assignment>> ToEnumerable(DbDataReader reader)
        {
            var assignments = new List<Assignment>();

            while (await reader.ReadAsync())
            {
                var index = assignments.FindIndex(c => c.Id.Equals(reader.GetInt32(0)));

                if (index < 0)
                {
                    assignments.Add(ToModel(reader));
                    index = assignments.Count - 1;
                    assignments[index].Results.AddRange(await GetResults(reader.GetInt32(0)));
                }
            }

            return assignments;
        }

        private async Task<IEnumerable<Identifier>> GetResults(object assignmentId)
        {
            var results = new List<Identifier>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
                        SELECT
                            {Results}.ID ID,
                            {Results}.GRADE GRADE
                        FROM
                            {Results}
                        WHERE
                            {Results}.ASSIGNMENT = :assignmentId
                    ";
                    command.Parameters.Add(new OracleParameter("assignmentId", assignmentId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var result = new Result()
                            {
                                Id = reader.GetInt32(0),
                                Grade = reader.GetInt32(1)
                            }.ToIdentifier();

                            results.Add(result);
                        }
                    }
                }
            }
            return results;
        }

        public override string SelectCommandText => $@"
            SELECT
                {Assignments}.ID ID,
                {Assignments}.ASSIGNMENTNAME ASS_NAME,
                {ModuleRuns}.ID MODRUN_ID,
                {Modules}.ID MOD_ID,
                {Modules}.MODULETITLE MOD_TITLE,
                {Modules}.MODULECODE MOD_CODE,
                {Users}.ID LECTURER_ID,
                {Users}.FIRSTNAME LECTURER_FIRSTNAME,
                {Users}.LASTNAME LECTURER_LASTNAME
            FROM
                {Assignments}
            LEFT OUTER JOIN
                {ModuleRuns} ON {ModuleRuns}.ID = {Assignments}.MODULERUN
            LEFT OUTER JOIN
                {Modules} ON {Modules}.ID = {ModuleRuns}.MODULE
            LEFT OUTER JOIN
                {Users} ON {Users}.ID = {ModuleRuns}.LECTURER
        ";

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
