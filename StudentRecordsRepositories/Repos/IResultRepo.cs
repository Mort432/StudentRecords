using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public interface IResultRepo : IRepo<Result>
    {
        List<Result> GetLecturerResults(User lecturer);

        IEnumerable<Result> GetUserResults(User user);

        Identifier GetExistingResult(Assignment assignment, User student);
    }
}
