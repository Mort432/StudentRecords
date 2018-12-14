using MongoDB.Driver;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mongo
{
    public class MongoAssignmentRepo : MongoRepo<Assignment>, IAssignmentRepo
    {
        protected override IMongoCollection<Assignment> Collection => Assignments;

        public List<Assignment> GetUserAssignments(User user)
        {
            //Get assignments where user enrollments matches assignment module run
            return Select(x => user.Enrollments.Contains(x.ModuleRun)).Result.ToList();
        }
    }
}
