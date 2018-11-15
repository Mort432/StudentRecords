﻿using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    class MockAssignmentRepo : MockRepo<Assignment>, IAssignmentRepo
    {
        public MockAssignmentRepo()
        {
            InsertSet(data.AssignmentsCollection);
        }
    }
}