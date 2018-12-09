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
        public IEnumerable<Identifier> Results { get; set; }
        public Identifier ModuleRun { get; set; }

        public override string ToString()
        {
            return AssignmentName;
        }
    }
}
