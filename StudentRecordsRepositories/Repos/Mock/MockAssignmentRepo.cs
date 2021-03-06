﻿using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mock
{
    public class MockAssignmentRepo : MockRepo<Assignment>, IAssignmentRepo
    {
        public MockAssignmentRepo()
        {
            Items = data.AssignmentsCollection;
            mockIdentityTracker = Items.Count + 1;
        }

        public List<Assignment> GetUserAssignments(User user)
        {
            //Select where assignment module run matches a user's enrollments
            return Select(x => user.Enrollments.Contains(x.ModuleRun)).Result.ToList();
        }
    }
}
