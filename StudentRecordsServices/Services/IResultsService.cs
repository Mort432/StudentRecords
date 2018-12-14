using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    //Defines the behaviour that must be implemented by a ResultsService
    public interface IResultsService
    {
        IEnumerable<Result> GetUserResults(User user);
        void DeleteResultByIdentifier(Identifier result);
        void AssignResult(User student, Assignment assignment, int grade);
    }
}
