using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class Module : Identifiable
    {
        public string ModuleTitle { get; set; }
        public string ModuleCode { get; set; }
        public IEnumerable<Identifier> ModuleRuns { get; set; }

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
