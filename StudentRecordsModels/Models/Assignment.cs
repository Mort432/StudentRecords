using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class Assignment : Identifiable
    {
        public string AssignmentName { get; set; }
        public IEnumerable<Result> Results { get; set; }
        public ModuleRun ModuleRun { get; set; }
    }
}
