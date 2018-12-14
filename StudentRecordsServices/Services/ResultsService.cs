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
        //Inject dependancies
        public IResultRepo _resultRepo;

        public ResultsService(IResultRepo resultRepo)
        {
            _resultRepo = resultRepo;
        }

        //Get a user's results
        public IEnumerable<Result> GetUserResults(User user)
        {
            return _resultRepo.GetUserResults(user);
        }

        //Delete a result, by it's identifier.
        public void DeleteResultByIdentifier(Identifier result)
        {
            var resultObj = _resultRepo.SelectById(result.Id).Result;
            _resultRepo.Delete(resultObj);
        }

        //Assign somebody a new result
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
                //If they don't already have a result, just insert the new one.
                _resultRepo.Insert(result);
                return;
            }
            //If they don't, then we can just update the existing result.
            result.Id = existingResult.Id;
            _resultRepo.Update(result);
        }

        //Get a user's result on an assignment
        public Identifier GetExistingResult(Assignment assignment, User student)
        {
            return _resultRepo.GetExistingResult(assignment, student);
            
        }
    }
}
