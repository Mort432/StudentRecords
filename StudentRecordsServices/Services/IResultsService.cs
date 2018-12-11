using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    public interface IResultsService
    {
        IEnumerable<Result> GetUserResults(User user);
        void DeleteResultByIdentifier(Identifier result);
    }
}
