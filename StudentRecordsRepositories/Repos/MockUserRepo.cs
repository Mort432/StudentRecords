using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockUserRepo : MockRepo<User>, IUserRepo
    {
        public MockUserRepo()
        {
            Items = data.UsersCollection;
            mockIdentityTracker = Items.Count + 1;
        }

        public List<User> GetUsersFromCourse(Course course)
        {
            return Select(x => course.Students.Any(z => z.Id.Equals(x.Id))).Result.ToList();
        }
    }
}
