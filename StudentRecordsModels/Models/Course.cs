using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class Course : Identifiable
    {
        public string Title { get; set; }
        public User CourseLeader { get; set; }

        public IEnumerable<ModuleRun> ModuleRuns { get; set; }
        public IEnumerable<User> Students { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
