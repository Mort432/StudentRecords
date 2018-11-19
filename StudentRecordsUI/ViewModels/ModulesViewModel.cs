using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsUI.ViewModels
{
    public class ModulesViewModel
    {
        public List<User> users = new List<User>();
        public User selectedUser;

        public List<ModuleRun> allModuleRuns = new List<ModuleRun>();

        private IUsersService _usersService;
        private IModulesService _modulesService;

        public ModulesViewModel(IUsersService usersService, IModulesService modulesService)
        {
            _usersService = usersService;
            _modulesService = modulesService;

            users = _usersService.GetAllUsers().Result.ToList();
            allModuleRuns = _modulesService.GetAllModuleRuns().Result.ToList();
        }

        public List<ModuleRun> GetAvailableModules()
        {
            if(selectedUser == null)
            {
                return new List<ModuleRun>();
            }

            var returnVal = allModuleRuns.Where(p => !selectedUser.Enrollments.Any(x => x.Id != p.Id)).ToList();
            return returnVal;
        }

        public void UserSelected(int selectionIndex)
        {
            selectedUser = users[selectionIndex];
            if(selectedUser.Enrollments == null)
            {
                selectedUser.Enrollments = new List<ModuleRun>();
            }
            GetAvailableModules();
        }
    }
}
