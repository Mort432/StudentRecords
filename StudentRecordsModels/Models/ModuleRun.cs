using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    // Represents the run of a module.
    public class ModuleRun : Identifiable
    {
        public Identifier Lecturer { get; set; }
        public Identifier Module { get; set; }
        public List<Identifier> Students { get; set; }
        public List<Identifier> Assignments { get; set; }

        public override string ToString()
        {
            return Module.ToString() + " (" + Lecturer.ToString() + ")";
        }
    }
}
