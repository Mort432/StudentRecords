using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public class MockModuleRepo : MockRepo<Module>, IModuleRepo
    {
        public MockModuleRepo()
        {
            InsertSet(data.ModulesCollection);
        }
    }
}
