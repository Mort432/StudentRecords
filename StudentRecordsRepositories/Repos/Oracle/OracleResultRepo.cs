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
    public class OracleResultRepo : OracleRepo<Result>, IResultRepo
    {
        public override string Table => Results;

        //FUNCTIONS
        public Identifier GetExistingResult(Assignment assignment, User student)
        {
            var results = GetUserResults(student);
            //Find matches
            var linqQuery =
                from result1 in assignment.Results
                join result2 in results on result1.Id equals result2.Id
                select result1;

            return linqQuery.FirstOrDefault();
        }

        public List<Result> GetModuleRunsResults(List<ModuleRun> moduleRuns)
        {
            List<Identifier> assignments = moduleRuns.SelectMany(x => x.Assignments).ToList();
            return Select(x => assignments.Any(y => y.Id.Equals(x.Assignment.Id))).Result.ToList();
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            return Select(x => x.Student.Id.Equals(user.Id)).Result.ToList();
        }

        //OVERRIDES

        public override Result ToModel(DbDataReader reader)
        {
            var module = new Module
            {
                Id = reader.GetInt32(8),
                ModuleTitle = reader.GetString(9),
                ModuleCode = reader.GetString(10)
            }.ToIdentifier();
            var lecturer = new User
            {
                Id = reader.GetInt32(11),
                FirstName = reader.GetString(12),
                LastName = reader.GetString(13)
            }.ToIdentifier();
            var moduleRun = new ModuleRun
            {
                Id = reader.GetInt32(7),
                Lecturer = lecturer,
                Module = module
            }.ToIdentifier();
            var student = new User
            {
                Id = reader.GetInt32(2),
                FirstName = reader.GetString(3),
                LastName = reader.GetString(4)
            }.ToIdentifier();
            var assignment = new Assignment
            {
                Id = reader.GetInt32(5),
                AssignmentName = reader.GetString(6),
                ModuleRun = moduleRun
            }.ToIdentifier();
            var result = new Result
            {
                Id = reader.GetInt32(0),
                Grade = reader.GetInt32(1),
                Student = student,
                Assignment = assignment
            };

            return result;
        }

        public override OracleParameter[] ToOracleParameters(Result item)
        {
            return new OracleParameter[]
            {
                new OracleParameter("grade", item.Grade),
                new OracleParameter("student", item.Student.Id),
                new OracleParameter("assignment", item.Assignment.Id)
            };
        }

        public override async Task<IEnumerable<Result>> ToEnumerable(DbDataReader reader)
        {
            var results = new List<Result>();

            while (await reader.ReadAsync())
            {
                var index = results.FindIndex(c => c.Id.Equals(reader.GetInt32(0)));

                if (index < 0)
                {
                    results.Add(ToModel(reader));
                    index = results.Count - 1;
                }
            }

            return results;
        }

        public override string SelectCommandText => $@"
            SELECT
                {Results}.ID RES_ID,
                {Results}.GRADE RES_GRADE,
                {Results}.STUDENT RES_STUDENTID,
                STUDENTS.FIRSTNAME RES_STUDENT_FIRSTNAME,
                STUDENTS.LASTNAME RES_STUDENT_LASTNAME,
                {Assignments}.ID ASSIGNMENTID,
                {Assignments}.ASSIGNMENTNAME ASS_NAME,
                {ModuleRuns}.ID MODRUN_ID,
                {Modules}.ID MODULE_ID,
                {Modules}.MODULETITLE MODULE_TITLE,
                {Modules}.MODULECODE MODULE_CODE,
                LECTURERS.ID LECTURER_ID,
                LECTURERS.FIRSTNAME LEC_FIRSTNAME,
                LECTURERS.LASTNAME LEC_LASTNAME
            FROM
                {Results}
            LEFT OUTER JOIN
                {Users} STUDENTS ON STUDENTS.ID = {Results}.STUDENT
            LEFT OUTER JOIN
                {Assignments} ON {Assignments}.ID = {Results}.ASSIGNMENT
            LEFT OUTER JOIN
                {ModuleRuns} ON {ModuleRuns}.ID = {Assignments}.MODULERUN
            LEFT OUTER JOIN
                {Modules} ON {Modules}.ID = {ModuleRuns}.MODULE
            LEFT OUTER JOIN
                {Users} LECTURERS ON LECTURERS.ID = {ModuleRuns}.LECTURER
        ";

        public override string InsertCommandText => $@"
            INSERT INTO
                {Results}
                (ASSIGNMENT, STUDENT, GRADE)
            VALUES
                (
                    :assignment,
                    :student,
                    :grade
                )
        ";

        public override string UpdateCommandText => $@"
            UPDATE
                {Results}
            SET
                ASSIGNMENT = :assignment,
                STUDENT = :student,
                GRADE = :grade
        ";
    }
}
