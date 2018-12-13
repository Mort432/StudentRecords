﻿using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mock
{
    public class MockResultRepo : MockRepo<Result>, IResultRepo
    {
        public MockResultRepo()
        {
            Items = data.ResultsCollection;
            mockIdentityTracker = Items.Count + 1;
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

        public List<Result> GetModuleRunsResults(List<ModuleRun> moduleRuns)
        {
            List<Identifier> assignments = moduleRuns.SelectMany(x => x.Assignments).ToList();
            return Select(x => assignments.Any(z => x.Assignment.Id.Equals(z.Id))).Result.ToList();
        }

        public IEnumerable<Result> GetUserResults(User user)
        {
            return Select(x => x.Student.Id.Equals(user.Id)).Result.ToList();
        }
    }
}