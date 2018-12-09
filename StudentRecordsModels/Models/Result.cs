using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class Result : Identifiable
    {
        public Identifier Assignment { get; set; }
        public Identifier Student { get; set; }
        public int Grade { get; set; }

        public override string ToString()
        {
            return Grade.ToString();
        }
    }
}
