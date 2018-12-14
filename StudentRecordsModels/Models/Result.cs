using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    //Represents results.
    public class Result : Identifiable
    {
        // Relationship to assignment
        public Identifier Assignment { get; set; }
        // Relationship to student
        public Identifier Student { get; set; }
        public int Grade { get; set; }

        public override string ToString()
        {
            return Grade.ToString();
        }
    }
}
