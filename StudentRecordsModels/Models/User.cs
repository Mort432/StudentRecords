﻿using StudentRecordsModels.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    // Represents users.
    public class User : Identifiable
    {
        public string UniversityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //Represents modules taken as a student, or modules taught as a lecturer
        public List<Identifier> Enrollments { get; set; }
        //Represents the course if you're a student, or offers a two-way relationship between lecturers and courses
        public Identifier Course { get; set; }
        public bool Graduated { get; set; }
        
        public UserRole Role { get; set; }

        public override string ToString()
        {
            return GetFullName();
        }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        public string GetFullNameWithRole { get { return FirstName + " " + LastName + " " + "(" + Role.ToString() + ")"; } }
    }
}
