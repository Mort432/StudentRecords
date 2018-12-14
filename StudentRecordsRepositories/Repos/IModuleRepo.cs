using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    //Declares functionality that a Module repo must implement.
    public interface IModuleRepo : IRepo<Module>
    {
    }
}
