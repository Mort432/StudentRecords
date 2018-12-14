using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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
            return GetUsersFromCourse(course).Where(x => x.Graduated).Count();
        }

        public Task<IEnumerable<User>> GetAllLecturers()
        {
            return Select(x => x.Role == UserRole.Lecturer);
        }

        public Task<IEnumerable<User>> GetAllStudents()
        {
            return Select(x => x.Role == UserRole.Student);
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            var courseStudents = Select(x => (x.Course != null) && lecturer.Course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student).Result.ToList();
            var moduleStudents = Select(user => user.Enrollments.Any(userEnrollment => lecturer.Enrollments.Contains(userEnrollment)) && user.Role == UserRole.Student).Result;
            var all = new List<User>();
            all.AddRange(courseStudents);
            all.AddRange(moduleStudents);
            var myStudents = all.GroupBy(x => x.Id).Select(g => g.First());

            return myStudents.ToList();
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            return Select(x => (x.Course != null) && course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student).Result.ToList();
        }

        //UPDATES

        public override void Update(User user)
        {
            //Update enrollments
            UpdateUserEnrollments(user);

            //Update base user
            base.Update(user);
        }

        public async void UpdateUserEnrollments(User user)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                var studentRunIds = user.Enrollments.Select(e => e.Id).ToList();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
                        DELETE FROM
                            {Enrollments}
                        WHERE
                            USERID = :userId
                    ";
                    command.Parameters.Add(new OracleParameter("userId", user.Id));

                    if (studentRunIds.Count > 0)
                    {
                        command.CommandText += "AND MODULERUNID NOT IN (:moduleRunIds)";
                        AddArrayParametersForEnrollments(command, "runIds", studentRunIds);
                    }

                    await command.ExecuteNonQueryAsync();
                }

                var databaseRunIds = new List<object>();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $@"
                        SELECT
                            {Enrollments}.MODULERUNID MODULERUNID
                        FROM
                            {Enrollments}
                        WHERE
                            {Enrollments}.USERID = :userId
                    ";
                    command.Parameters.Add(new OracleParameter("userId", user.Id));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            databaseRunIds.Add(reader.GetInt32(0));
                        }
                    }
                }

                studentRunIds.Except(databaseRunIds).ToList().ForEach(async runId =>
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.BindByName = true;
                        command.CommandText = $@"
                            INSERT INTO
                                {Enrollments}
                                (USERID,
                                MODULERUNID)
                            VALUES
                                (:userId,
                                :moduleRunId)
                        ";
                        command.Parameters.AddRange(new OracleParameter[]
                        {
                            new OracleParameter("userId", user.Id),
                            new OracleParameter("moduleRunId", runId)
                        });

                        await command.ExecuteNonQueryAsync();
                    }
                });
            }
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
                Graduated = convertIntToBool(reader.GetInt32(7)),
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
                new OracleParameter("graduated", convertBoolToInt(item.Graduated)),
                new OracleParameter("userRole", (int)item.Role),
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

        public override string UpdateCommandText => $@"
            UPDATE
                {Users}
            SET
                UNIVERSITYID = :universityId,
                FIRSTNAME = :firstName,
                LASTNAME = :lastName,
                DATEOFBIRTH = :dateOfBirth,
                EMAIL = :email,
                PHONENUMBER = :phoneNumber,
                GRADUATED = :graduated,
                USERROLE = :userRole,
                COURSE = :course
        ";

        private int convertBoolToInt(bool grad)
        {
            if (grad)
            {
                return 1;
            }
            return 0;
        }

        private bool convertIntToBool(int grad)
        {
            if (grad == 1)
            {
                return true;
            }
            return false;
        }

        private void AddArrayParametersForEnrollments(OracleCommand command, string paramNameRoot, List<object> runIds)
        {
            var parameters = new List<OracleParameter>();
            var parameterNames = new List<string>();
            var paramNum = 1;

            foreach(var id in runIds)
            {
                var paramName = string.Format("{0}{1}", paramNameRoot, paramNum++);
                parameterNames.Add($":{paramName}");
                var parameter = new OracleParameter(paramName, id);
                command.Parameters.Add(parameter);
                parameters.Add(parameter);
            }

            command.CommandText = command.CommandText.Replace($":{paramNameRoot}", string.Join(",", parameterNames));
        }
    }
}
