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
            //Get results
            var results = GetUserResults(student);
            //Find matches with assignments
            var linqQuery =
                from result1 in assignment.Results
                join result2 in results on result1.Id equals result2.Id
                select result1;

            return linqQuery.FirstOrDefault();
        }

        public List<Result> GetModuleRunsResults(List<ModuleRun> moduleRuns)
        {
            //Get assignments from module runs
            List<Identifier> assignments = moduleRuns.SelectMany(x => x.Assignments).ToList();
            //Select out results
            return Select(x => assignments.Any(y => y.Id.Equals(x.Assignment.Id))).Result.ToList();
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            //Select results where user matches
            return Select(x => x.Student.Id.Equals(user.Id)).Result.ToList();
        }

        public override void Delete(Result item)
        {
            var result = item.ToIdentifier();
            //Find and remove result
            var assignmentIdFilter = Builders<Assignment>.Filter.Eq(i => i.Id, item.Assignment.Id);
            var assignmentResultsUpdate = Builders<Assignment>.Update.Pull(a => a.Results, result);

            Assignments.UpdateOne(assignmentIdFilter, assignmentResultsUpdate);

            base.Delete(item);
        }

        public override void Update(Result item)
        {
            //Find and delete any existing
            Delete(item);
            //Insert replacement
            Insert(item);
        }

        public override void Insert(Result item)
        {
            var assignmentIdFilter = Builders<Assignment>.Filter.Eq(a => a.Id, item.Assignment.Id);
            var assignmentResultsUpdate = Builders<Assignment>.Update.Push(a => a.Results, item.ToIdentifier());

            // Insert child result to assignment
            Assignments.UpdateOneAsync(assignmentIdFilter, assignmentResultsUpdate);

            base.Insert(item);
        }
    }
}
