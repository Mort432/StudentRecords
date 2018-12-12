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
            var results = GetUserResults(student);
            //Find matches
            var linqQuery =
                from result1 in assignment.Results
                join result2 in results on result1.Id equals result2.Id
                select result1;

            return linqQuery.FirstOrDefault();
        }

        public List<Result> GetLecturerResults(User lecturer)
        {
            return Select(x => lecturer.Enrollments.Any(y => y.Id.Equals(x.Assignment.Id))).Result.ToList();
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            return Select(x => x.Student.Id.Equals(user.Id)).Result.ToList();
        }

        public override void Delete(Result item)
        {
            var result = item.ToIdentifier();

            var assignmentIdFilter = Builders<Assignment>.Filter.Eq(i => i.Id, item.Assignment.Id);
            var assignmentResultsUpdate = Builders<Assignment>.Update.Pull(a => a.Results, result);

            Assignments.UpdateOne(assignmentIdFilter, assignmentResultsUpdate);

            base.Delete(item);
        }

        public override void Update(Result item)
        {
            Delete(item);
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
