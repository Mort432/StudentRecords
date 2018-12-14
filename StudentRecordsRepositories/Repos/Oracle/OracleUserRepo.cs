using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    class OracleUserRepo : OracleRepo<User>, IUserRepo
    {
        public override string Table => Users;

        //FUNCTIONS
        public int CountGraduatedCourseUsers(Course course)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllLecturers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            throw new NotImplementedException();
        }

        //OVERRIDES
        public override User ToModel(DbDataReader reader)
        {
            //Remember course is nullable
            Identifier lecturer = null;
            Identifier course = null;
            if (!reader.IsDBNull(8))
            {
                lecturer = new User()
                {
                    Id = reader.GetInt32(10),
                    FirstName = reader.GetString(11),
                    LastName = reader.GetString(12)
                }.ToIdentifier();

                course = new Course()
                {
                    Id = reader.GetInt32(8),
                    Title = reader.GetString(9),
                    CourseLeader = lecturer
                }.ToIdentifier();
            }

            User user = new User
            {
                Id = reader.GetInt32(0),
                UniversityId = reader.GetString(1),
                FirstName = reader.GetString(2),
                LastName = reader.GetString(3),
                DateOfBirth = reader.GetDateTime(4),
                Email = reader.GetString(5),
                PhoneNumber = reader.GetString(6),
                Graduated = reader.GetBoolean(7),
                Role = (UserRole)reader.GetInt32(8)
            };

            return user;
        }

        public override OracleParameter[] ToOracleParameters(User item)
        {
            return new OracleParameter[]
            {
                new OracleParameter("universityId", item.UniversityId),
                new OracleParameter("firstName", item.FirstName),
                new OracleParameter("lastName", item.LastName),
                new OracleParameter("dateOfBirth", item.DateOfBirth),
                new OracleParameter("email", item.Email),
                new OracleParameter("phoneNumber", item.PhoneNumber),
                new OracleParameter("graduated", item.Graduated),
                new OracleParameter("userRole", item.Role),
                new OracleParameter("course", item.Course.Id)
            };
        }

        public override async Task<IEnumerable<User>> ToEnumerable(DbDataReader reader)
        {
            var users = new List<User>();

            while (await reader.ReadAsync())
            {
                var index = users.FindIndex(c => c.Id.Equals(reader.GetInt32(0)));

                if (index < 0)
                {
                    users.Add(ToModel(reader));
                    index = users.Count - 1;
                    users[index].Enrollments.AddRange(await GetEnrollments(reader.GetInt32(0)));
                }
            }

            return users;
        }

        private async Task<IEnumerable<Identifier>> GetEnrollments(object userId)
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
                            {Enrollments}.ID ID,
                            {ModuleRuns}.ID MODULERUNID,
                            {Modules}.Id MODULEID,
                            {Modules}.MODULETITLE MODULETITLE,
                            {Modules}.MODULECODE MODULECODE,
                            {Users}.ID LECTURERID,
                            {Users}.FIRSTNAME LECTURERFIRSTNAME,
                            {Users}.LASTNAME LECTURERLASTNAME
                        FROM
                            {Enrollments}
                        LEFT OUTER JOIN
                            {ModuleRuns} ON {Enrollments}.MODULERUNID = {ModuleRuns}.ID
                        LEFT OUTER JOIN
                            {Modules} ON {Modules}.ID = {ModuleRuns}.MODULE
                        LEFT OUTER JOIN
                            {Users} ON {Users}.ID = {ModuleRuns}.LECTURER
                        WHERE
                            {Enrollments}.UserId = :userId
                    ";
                    command.Parameters.Add(new OracleParameter("userId", userId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var module = new Module
                            {
                                Id = reader.GetInt32(2),
                                ModuleTitle = reader.GetString(3),
                                ModuleCode = reader.GetString(4)
                            }.ToIdentifier();
                            var lecturer = new User
                            {
                                Id = reader.GetInt32(5),
                                FirstName = reader.GetString(6),
                                LastName = reader.GetString(7)
                            }.ToIdentifier();
                            var moduleRun = new ModuleRun
                            {
                                Id = reader.GetInt32(1),
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

        public override string SelectCommandText => $@"
            SELECT
            STUDENTS.Id USR_ID,
            STUDENTS.FirstName USR_FIRSTNAME,
            STUDENTS.LastName USR_LASTNAME,
            STUDENTS.DateOfBirth USR_DOB,
            STUDENTS.Email USR_EMAIL,
            STUDENTS.PhoneNumber USR_PHONE,
            STUDENTS.Graduated USR_GRAD,
            STUDENTS.UserRole USR_ROLE,
            STUDENTS.Course USR_COURSE_ID,
            {Courses}.Title COURSE_TITLE,
            LECTURERS.Id COURSE_LECTURER_ID,
            LECTURERS.FirstName COURSE_LECTURER_FIRSTNAME,
            LECTURERS.LastName COURSE_LECTURER_LASTNAME
        FROM
            {Users} STUDENTS
        LEFT OUTER JOIN
            {Courses} ON STUDENTS.Course = {Courses}.Id
        LEFT OUTER JOIN
            {Users} LECTURERS ON LECTURERS.Id = {Courses}.CourseLeader
";

        public override string InsertCommandText => throw new NotImplementedException();

        public override string UpdateCommandText => throw new NotImplementedException();
    }
}
