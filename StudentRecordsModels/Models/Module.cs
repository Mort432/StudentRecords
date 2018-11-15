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
    }
}
