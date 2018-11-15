using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class ModuleRun : Identifiable
    {
        public User Lecturer { get; set; }
        public Module Module { get; set; }
        public IEnumerable<User> Students { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
    }
}
