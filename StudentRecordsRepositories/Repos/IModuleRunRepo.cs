using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    //Defines the functionality a module run repo must implement.
    public interface IModuleRunRepo : IRepo<ModuleRun>
    {
        //Given a lecturer, return all the module runs they teach.
        List<ModuleRun> GetLecturerModuleRuns(User lecturer);
    }
}
