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
                Role = UserRole.Student
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
                Lecturer = user3,
                Module = module1
            };

            module1.ModuleRuns = new List<ModuleRun>() { moduleRun1 };

            Assignment assignment1 = new Assignment
            {
                Id = 1,
                AssignmentName = "1. Chew Bubblegum",
                ModuleRun = moduleRun1
            };

            moduleRun1.Assignments = new List<Assignment>() { assignment1 };

            Course course1 = new Course
            {
                Id = 1,
                CourseLeader = user3,
                ModuleRuns = new List<ModuleRun>() { moduleRun1 },
                Students = new List<User>()
            };

            Result result1 = new Result()
            {
                Id = 1,
                Assignment = assignment1,
                Grade = 74,
                Student = user1
            };

            assignment1.Results = new List<Result>() { result1 };

            user1.Enrollments = new List<ModuleRun>() { moduleRun1 };
            user3.Enrollments = new List<ModuleRun>() { moduleRun1 };

            UsersCollection.Add(user1);
            UsersCollection.Add(user2);
            UsersCollection.Add(user3);
            ModulesCollection.Add(module1);
            ModuleRunsCollection.Add(moduleRun1);
            AssignmentsCollection.Add(assignment1);
            ResultsCollection.Add(result1);
        }
        

        public List<User> UsersCollection = new List<User>();
        public List<Module> ModulesCollection = new List<Module>();
        public List<ModuleRun> ModuleRunsCollection = new List<ModuleRun>();
        public List<Course> CoursesCollection = new List<Course>();
        public List<Assignment> AssignmentsCollection = new List<Assignment>();
        public List<Result> ResultsCollection = new List<Result>();
    }
}
