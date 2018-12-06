using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    class MockCourseRepo : MockRepo<Course>, ICourseRepo
    {
        public MockCourseRepo()
        {
            Items = data.CoursesCollection;
            mockIdentityTracker = Items.Count + 1;
        }
    }
}
