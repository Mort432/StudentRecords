using StudentRecordsModels.Models;
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
    }
}
