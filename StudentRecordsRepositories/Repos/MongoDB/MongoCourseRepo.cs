using MongoDB.Driver;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mongo
{
    public class MockCourseRepo : MongoRepo<Course>, ICourseRepo
    {
        protected override IMongoCollection<Course> Collection => Courses;
    }
}
