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

        public void DeleteResultByIdentifier(Identifier result)
        {
            var resultObj = _resultRepo.SelectById(result.Id).Result;
            _resultRepo.Delete(resultObj);
        }

        public void AssignResult(User student, Assignment assignment, int grade)
        {
            //Build new result
            Result result = new Result();
            result.Assignment = assignment.ToIdentifier();
            result.Grade = grade;
            result.Student = student.ToIdentifier();

            //Check if we need to insert or update a result
            var existingResult = GetExistingResult(assignment, student);
            if(existingResult == null)
            {
                _resultRepo.Insert(result);
                return;
            }
            //Otherwise, just update the existing one.
            result.Id = existingResult.Id;
            _resultRepo.Update(result);
        }

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
    }
}
