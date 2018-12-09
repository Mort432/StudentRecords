using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockDatabase
    {

        public MockDatabase()
        {
            User user1 = new User
            {
                Id = 1,
                UniversityId = "s0000001",
                FirstName = "Connagh",
                LastName = "Muldoon",
                Email = "s0000001@glos.ac.uk",
                PhoneNumber = "01452699316",
                DateOfBirth = new DateTime(1995, 06, 01),
                Role = UserRole.Student
            };
            User user2 = new User
            {
                Id = 2,
                UniversityId = "s0000002",
                FirstName = "Thomas",
                LastName = "Clark",
                Email = "s0000002@glos.ac.uk",
                PhoneNumber = "01452234765",
                DateOfBirth = new DateTime(1995, 02, 23),
                Role = UserRole.Student,
                Enrollments = new List<Identifier>()
            };
            User user3 = new User
            {
                Id = 3,
                UniversityId = "s0000003",
                FirstName = "Abu",
                LastName = "Alam",
                Email = "aalam@glos.ac.uk",
                PhoneNumber = "01452543876",
                DateOfBirth = new DateTime(1988, 08, 12),
                Role = UserRole.Lecturer
            };

            Module module1 = new Module
            {
                Id = 1,
                ModuleTitle = "Applied Ass Kicking",
                ModuleCode = "CT1337"
            };

            ModuleRun moduleRun1 = new ModuleRun
            {
                Id = 1,
                Lecturer = user3.ToIdentifier(),
                Module = module1.ToIdentifier()
            };

            module1.ModuleRuns = new List<Identifier>() { moduleRun1.ToIdentifier() };

            Assignment assignment1 = new Assignment
            {
                Id = 1,
                AssignmentName = "1. Chew Bubblegum",
                ModuleRun = moduleRun1.ToIdentifier()
            };

            moduleRun1.Assignments = new List<Identifier>() { assignment1.ToIdentifier() };

            Course course1 = new Course
            {
                Id = 1,
                Title = "Computing - BSc W/Hons",
                CourseLeader = user3.ToIdentifier(),
                ModuleRuns = new List<Identifier>() { moduleRun1.ToIdentifier() },
                Students = new List<Identifier>() { user1.ToIdentifier() }
            };

            Result result1 = new Result()
            {
                Id = 1,
                Assignment = assignment1.ToIdentifier(),
                Grade = 74,
                Student = user1.ToIdentifier()
            };

            assignment1.Results = new List<Identifier>() { result1.ToIdentifier() };

            user1.Enrollments = new List<Identifier>() { moduleRun1.ToIdentifier() };
            user1.Course = new Identifier(course1);
            user3.Enrollments = new List<Identifier>() { moduleRun1.ToIdentifier() };

            UsersCollection.Add(user1);
            UsersCollection.Add(user2);
            UsersCollection.Add(user3);
            ModulesCollection.Add(module1);
            ModuleRunsCollection.Add(moduleRun1);
            AssignmentsCollection.Add(assignment1);
            ResultsCollection.Add(result1);
            CoursesCollection.Add(course1);
        }
        

        public List<User> UsersCollection = new List<User>();
        public List<Module> ModulesCollection = new List<Module>();
        public List<ModuleRun> ModuleRunsCollection = new List<ModuleRun>();
        public List<Course> CoursesCollection = new List<Course>();
        public List<Assignment> AssignmentsCollection = new List<Assignment>();
        public List<Result> ResultsCollection = new List<Result>();
    }
}
