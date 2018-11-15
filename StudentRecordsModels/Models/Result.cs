using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class Result : Identifiable
    {
        public Assignment Assignment { get; set; }
        public User Student { get; set; }
        public int Grade { get; set; }
    }
}
