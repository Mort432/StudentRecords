using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;
using StudentRecordsRepositories.Repos;

namespace StudentRecordsServices.Services
{
    public class ResultsService : IResultsService
    {
        public IResultRepo _resultRepo;

        public ResultsService(IResultRepo resultRepo)
        {
            _resultRepo = resultRepo;
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            return _resultRepo.Select(x => x.Student.Id.Equals(user.Id)).Result.ToList();
        }
    }
}
