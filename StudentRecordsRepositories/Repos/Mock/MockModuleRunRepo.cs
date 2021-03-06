﻿using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Mock
{
    public class MockModuleRunRepo : MockRepo<ModuleRun>, IModuleRunRepo
    {
        public MockModuleRunRepo()
        {
            Items = data.ModuleRunsCollection;
            mockIdentityTracker = Items.Count + 1;
        }

        public List<ModuleRun> GetLecturerModuleRuns(User lecturer)
        {
            //Select where module run lecturer = lecturer
            return Select(x => x.Lecturer.Id.Equals(lecturer.Id)).Result.ToList();
        }
    }
}
