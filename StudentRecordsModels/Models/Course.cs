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
        public Identifier CourseLeader { get; set; }

        public List<Identifier> ModuleRuns { get; set; }
        public List<Identifier> Students { get; set; }

        public override string ToString()
        {
            return Title + " (" + CourseLeader + ")";
        }
    }
}
