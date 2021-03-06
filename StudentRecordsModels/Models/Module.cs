﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    // Represents a module.
    public class Module : Identifiable
    {
        public string ModuleTitle { get; set; }
        public string ModuleCode { get; set; }
        // List of runs.
        public List<Identifier> ModuleRuns { get; set; }

        public string GetCodeAndName()
        {
            return ModuleCode + ": " + ModuleTitle;
        }

        public override string ToString()
        {
            return GetCodeAndName();
        }
    }
}
