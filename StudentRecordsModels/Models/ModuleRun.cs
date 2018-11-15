using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class ModuleRun : Identifiable
    {
        public User lecturer { get; set; }
        public Module module { get; set; }
        public IEnumerable<User> students { get; set; }
    }
}
