using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockResultRepo : MockRepo<Result>, IResultRepo
    {
        public MockResultRepo()
        {
            Items = data.ResultsCollection;
            mockIdentityTracker = Items.Count + 1;
        }

        public List<Result> GetLecturerResults(User lecturer)
        {
            return Select(x => lecturer.Enrollments.Any(y => y.Id.Equals(x.Assignment.Id))).Result.ToList();
        }
    }
}
