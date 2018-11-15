using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockRepoData
    {

        public MockRepoData()
        {
            User user1 = new User
            {
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
                UniversityId = "s0000003",
                FirstName = "Abu",
                LastName = "Alam",
                Email = "aalam@glos.ac.uk",
                PhoneNumber = "01452543876",
                DateOfBirth = new DateTime(1988, 08, 12),
                Role = UserRole.Lecturer
            };

            UsersCollection.Add(user1);
            UsersCollection.Add(user2);
            UsersCollection.Add(user3);
        }
        

        public List<User> UsersCollection = new List<User>();
    }
}
