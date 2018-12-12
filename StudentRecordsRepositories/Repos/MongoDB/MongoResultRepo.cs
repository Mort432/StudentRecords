using MongoDB.Driver;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mongo
{
    public class MongoResultRepo : MongoRepo<Result>, IResultRepo
    {
        protected override IMongoCollection<Result> Collection => Results;

        public Identifier GetExistingResult(Assignment assignment, User student)
        {
            throw new NotImplementedException();
        }

        public List<Result> GetLecturerResults(User lecturer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            throw new NotImplementedException();
        }
    }
}
