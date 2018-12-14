using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    //Declares the functionality a Result repo must inherit.
    public interface IResultRepo : IRepo<Result>
    {
        //Given a list of module runs, return all their results.
        List<Result> GetModuleRunsResults(List<ModuleRun> moduleRuns);

        //Given a user, retrieve all their results.
        IEnumerable<Result> GetUserResults(User user);

        //Given an assignment and a user, return a result if one is present.
        Identifier GetExistingResult(Assignment assignment, User student);
    }
}
