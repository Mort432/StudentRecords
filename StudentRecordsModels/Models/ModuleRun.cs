using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class ModuleRun : Identifiable
    {
        public Identifier Lecturer { get; set; }
        public Identifier Module { get; set; }
        public IList<Identifier> Students { get; set; }
        public IEnumerable<Identifier> Assignments { get; set; }

        public override string ToString()
        {
            return Module.ToString() + " (" + Lecturer.ToString() + ")";
        }
    }
}
