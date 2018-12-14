using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;
using StudentRecordsRepositories.Repos;

namespace StudentRecordsServices.Services
{
    public class ModulesService : IModulesService
    {
        //Inject dependancies
        IModuleRepo _moduleRepo;
        IModuleRunRepo _moduleRunRepo;

        public ModulesService(IModuleRepo moduleRepo, IModuleRunRepo moduleRunRepo)
        {
            _moduleRepo = moduleRepo;
            _moduleRunRepo = moduleRunRepo;
        }

        //Get all module runs
        public async Task<IEnumerable<ModuleRun>> GetAllModuleRuns()
        {
            return await _moduleRunRepo.SelectAll();
        }

        //Get all modules
        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _moduleRepo.SelectAll();
        }

        //Get module by Id
        public async Task<Module> GetModuleById(object id)
        {
            return await _moduleRepo.SelectById(id);
        }

        //Get module run by Id
        public async Task<ModuleRun> GetModuleRunById(object id)
        {
            return await _moduleRunRepo.SelectById(id);
        }
    }
}
