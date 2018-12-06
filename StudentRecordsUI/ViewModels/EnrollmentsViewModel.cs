using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsUI.ViewModels
{
    public class EnrollmentsViewModel
    {
        public User selectedUser;

        public List<ModuleRun> allModuleRuns = new List<ModuleRun>();

        private IAuthService _authService;
        private IModulesService _modulesService;

        public EnrollmentsViewModel(IAuthService authService, IModulesService modulesService)
        {
            _authService = authService;
            _modulesService = modulesService;

            selectedUser = _authService.authorisedUser;
            allModuleRuns = _modulesService.GetAllModuleRuns().Result.ToList();
        }

        public List<ModuleRun> GetAvailableModules()
        {
            var returnVal = allModuleRuns.Where(p => !selectedUser.Enrollments.Any(x => x.Id != p.Id)).ToList();
            return returnVal;
        }
    }
}
