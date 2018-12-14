using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models.Enums
{
    // Drives the user's role, and the views they can see.
    // Admins are unused.
    public enum UserRole
    {
        Student,
        Lecturer,
        Admin
    }
}
