﻿using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    public class OracleCourseRepo : OracleRepo<Course>, ICourseRepo
    {
        public override string Table => Courses;

        // OVERRIDES
        public override Course ToModel(DbDataReader reader)
        {
            var lecturer = new User
            {
                Id = reader.GetInt32(2),
                FirstName = reader.GetString(3),
                LastName = reader.GetString(4)
            }.ToIdentifier();
            var course = new Course
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                ModuleRuns = new List<Identifier>(),
                Students = new List<Identifier>(),
                CourseLeader = lecturer
            };

            return course;
        }

        // Convert model to params for query injection
        public override OracleParameter[] ToOracleParameters(Course item)
        {
            return new OracleParameter[]
            {
                new OracleParameter("title", item.Title)
            };
        }

        //Convert result set to list of models
        public override async Task<IEnumerable<Course>> ToEnumerable(DbDataReader reader)
        {
            var courses = new List<Course>();

            while (await reader.ReadAsync())
            {
                var index = courses.FindIndex(c => c.Id.Equals(reader.GetInt32(0)));

                if (index < 0)
                {
                    courses.Add(ToModel(reader));
                    index = courses.Count - 1;
                    //Subquery to get ModuleRuns for courses
                    courses[index].ModuleRuns.AddRange(await GetModuleRuns(reader.GetInt32(0)));
                    //Subquery to get Students for courses
                    courses[index].Students.AddRange(await GetUsers(reader.GetInt32(0)));
                }
            }

            return courses;
        }

        //Subquery to get course ModuleRuns
        private async Task<IEnumerable<Identifier>> GetModuleRuns(object courseId)
        {
            var moduleRuns = new List<Identifier>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
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
                        WHERE
                            {ModuleRuns}.COURSE = :courseId
                    ";
                    command.Parameters.Add(new OracleParameter("courseId", courseId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            //Compile result to ModuleRun identifier
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
                            }.ToIdentifier();

                            moduleRuns.Add(module);
                        }
                    }
                }
            }

            return moduleRuns;
        }

        //Subquery to get course Users
        private async Task<IEnumerable<Identifier>> GetUsers(object courseId)
        {
            var users = new List<Identifier>();

            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
                        SELECT
                            {Users}.ID ID,
                            {Users}.FIRSTNAME FIRST_NAME,
                            {Users}.LASTNAME LAST_NAME
                        FROM
                            {Users}
                        WHERE
                            COURSE = :courseId
                            AND USERROLE = 1
                    ";
                    command.Parameters.Add(new OracleParameter("courseId", courseId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            //Compile results into User identifier
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2)
                            }.ToIdentifier();

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        //Base Course Select Text
        public override string SelectCommandText => $@"
            SELECT
                {Courses}.ID ID,
                {Courses}.TITLE TITLE,
                {Courses}.COURSELEADER COURSELEADER,
                {Users}.FIRSTNAME LEADER_FIRSTNAME,
                {Users}.LASTNAME LEADER_LASTNAME
            FROM
                {Courses}
            LEFT OUTER JOIN
                {Users} ON {Users}.ID = {Courses}.COURSELEADER
            ORDER BY
                {Courses}.ID
        ";

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
