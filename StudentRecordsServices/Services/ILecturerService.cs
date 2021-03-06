﻿using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    //Defines the behaviour that must be implemented by a LecturerService
    public interface ILecturerService
    {
        List<User> GetLecturerStudents(User lecturer);
        int GetGraduatedStudents(Identifier courseIdentifier);
        List<Result> GetLecturerResults(User lecturer);
    }
}
