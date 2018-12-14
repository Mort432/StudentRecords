using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleModuleRunRepo : OracleRepo<ModuleRun>, IModuleRunRepo
    {
        public override string Table => ModuleRuns;

        // FUNCTIONS
        public List<ModuleRun> GetLecturerModuleRuns(User lecturer)
        {
            return Select(x => x.Lecturer.Id.Equals(lecturer.Id)).Result.ToList();
        }

        // OVERRIDES
        public override ModuleRun ToModel(DbDataReader reader)
        {
            var module = new Module
            {
                Id = reader.GetInt32(1),
                ModuleTitle = reader.GetString(2),
                ModuleCode = reader.GetString(3)
            }.ToIdentifier();
            var lecturer = new User
            {
                Id = reader.GetInt32(4),
                FirstName = reader.GetString(5),
                LastName = reader.GetString(6)
            }.ToIdentifier();
            var moduleRun = new ModuleRun
            {
                Id = reader.GetInt32(0),
                Module = module,
                Lecturer = lecturer
            };

            return moduleRun;
        }

        public override OracleParameter[] ToOracleParameters(ModuleRun item)
        {
            return new OracleParameter[]
            {
                new OracleParameter("lecturer", item.Lecturer.Id),
                new OracleParameter("module", item.Module.Id)
            };
        }

        public override string SelectCommandText => $@"
           SELECT
                {ModuleRuns}.ID ID,
                {Modules}.ID MODULEID,
                {Modules}.MODULETITLE MODULETITLE,
                {Modules}.MODULECODE MODULECODE,
                {Users}.ID LECTURERID,
                {Users}.FIRSTNAME LECTURERFIRSTNAME,
                {Users}.LASTNAME LECTURERLASTNAME
            FROM
                {ModuleRuns}
            LEFT OUTER JOIN
                {Modules} ON {Modules}.ID = {ModuleRuns}.MODULE
            LEFT OUTER JOIN
                {Users} ON {Users}.ID = {ModuleRuns}.LECTURER
        ";

        public override async Task<IEnumerable<ModuleRun>> ToEnumerable(DbDataReader reader)
        {
            var moduleRuns = new List<ModuleRun>();

            while (await reader.ReadAsync())
            {
                var index = moduleRuns.FindIndex(c => c.Id.Equals(reader.GetInt32(0)));

                if (index < 0)
                {
                    moduleRuns.Add(ToModel(reader));
                    index = moduleRuns.Count - 1;
                    moduleRuns[index].Students.AddRange(await GetStudents(reader.GetInt32(0)));
                    moduleRuns[index].Assignments.AddRange(await GetAssignments(reader.GetInt32(0)));
                }
            }

            return moduleRuns;
        }

        private async Task<IEnumerable<Identifier>> GetStudents(object moduleRunId)
        {
            var students = new List<Identifier>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
                        SELECT
                            {Users}.ID ID,
                            {Users}.FIRST_NAME FIRST_NAME,
                            {Users}.LAST_NAME LAST_NAME
                        FROM
                            {Users}
                        LEFT OUTER JOIN
                            {Enrollments} ON {Enrollments}.USERID = USERS.ID
                        WHERE
                            {Enrollments}.MODULERUNID = :moduleRunId
                            AND USERROLE = 1
                    ";
                    command.Parameters.Add(new OracleParameter("moduleRunId", moduleRunId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            }.ToIdentifier();

                            students.Add(user);
                        }
                    }
                }
            }

            return students;
        }

        private async Task<IEnumerable<Identifier>> GetAssignments(object moduleRunId)
        {
            var assignments = new List<Identifier>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
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
                        WHERE
                            {ModuleRuns}.ID = :moduleRunId
                    ";
                    command.Parameters.Add(new OracleParameter("moduleRunId", moduleRunId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
                            }.ToIdentifier();

                            assignments.Add(assignment);
                        }
                    }
                }
            }
            return assignments;
        }

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
