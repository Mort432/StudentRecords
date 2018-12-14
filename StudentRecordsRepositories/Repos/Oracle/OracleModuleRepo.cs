using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;

namespace StudentRecordsRepositories.Repos.Oracle
{
    public class OracleModuleRepo : OracleRepo<Module>, IModuleRepo
    {
        public override string Table => Modules;

        // OVERRIDES

        //Convert result set to model
        public override Module ToModel(DbDataReader reader)
        {
            var module = new Module
            {
                Id = reader.GetInt32(0),
                ModuleTitle = reader.GetString(1),
                ModuleCode = reader.GetString(2)
            };

            return module;
        }

        //Convert model to params for query injection
        public override OracleParameter[] ToOracleParameters(Module item)
        {
            return new OracleParameter[]
            {
                new OracleParameter("moduleTitle", item.ModuleTitle),
                new OracleParameter("moduleCode", item.ModuleCode)
            };
        }

        //Convert result set to list of Modules
        public override async Task<IEnumerable<Module>> ToEnumerable(DbDataReader reader)
        {
            var modules = new List<Module>();

            while(await reader.ReadAsync())
            {
                var index = modules.FindIndex(m => m.Id.Equals(reader.GetInt32(0)));

                if(index < 0)
                {
                    modules.Add(ToModel(reader));

                    index = modules.Count - 1;
                }
                //If there's a run
                if (!reader.IsDBNull(3))
                {
                    var module = modules[index].ToIdentifier();
                    Identifier lecturer = null;

                    //If the run has a lecturer
                    if (!reader.IsDBNull(4)) {
                        lecturer = new User
                        {
                            Id = reader.GetInt32(4),
                            FirstName = reader.GetString(5),
                            LastName = reader.GetString(6)
                        }.ToIdentifier();
                    }

                    var moduleRun = new ModuleRun
                    {
                        Id = reader.GetInt32(3),
                        Module = module,
                        Lecturer = lecturer
                    }.ToIdentifier();

                    modules[index].ModuleRuns.Add(moduleRun);
                }
            }

            return modules;
        }

        //Base Module Select String
        public override string SelectCommandText => $@"
            SELECT
                {Modules}.ID ID,
                {Modules}.MODULETITLE MODULETITLE,
                {Modules}.MODULECODE MODULECODE,
                {ModuleRuns}.ID MODULERUN_ID,
                {ModuleRuns}.LECTURER LECTURER,
                {Users}.FIRSTNAME FIRSTNAME,
                {Users}.LASTNAME LASTNAME
            FROM
                {Modules}
            LEFT OUTER JOIN
                {ModuleRuns} ON {ModuleRuns}.MODULE = {Modules}.ID
            LEFT OUTER JOIN
                {Users} ON {Users}.ID = {ModuleRuns}.LECTURER
            ORDER BY
                {Modules}.ID,
                {ModuleRuns}.ID
        ";

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
