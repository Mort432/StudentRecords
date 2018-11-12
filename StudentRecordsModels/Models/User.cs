using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class User : Identifiable
    {
        public string UniversityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public UserRole Role { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
