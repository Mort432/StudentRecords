﻿using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    //Defines the behaviour that must be implemented by a ModulesService
    public interface IModulesService
    {
        Task<Module> GetModuleById(object id);

        Task<IEnumerable<Module>> GetAllModules();

        Task<ModuleRun> GetModuleRunById(object id);

        Task<IEnumerable<ModuleRun>> GetAllModuleRuns();
    }
}
