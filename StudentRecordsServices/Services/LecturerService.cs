﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using StudentRecordsRepositories.Repos;

namespace StudentRecordsServices.Services
{
    public class LecturerService : ILecturerService
    {
        private IUserRepo _userRepo;
        private ICourseRepo _courseRepo;
        private IResultRepo _resultRepo;

        public LecturerService(IUserRepo userRepo, ICourseRepo courseRepo, IResultRepo resultRepo)
        {
            _userRepo = userRepo;
            _courseRepo = courseRepo;
            _resultRepo = resultRepo;
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            return _userRepo.GetLecturerStudents(lecturer);
        }

        public int GetGraduatedStudents(Identifier courseIdentifier)
        {
            Course course = _courseRepo.SelectById(courseIdentifier.Id).Result;
            List<User> users = _userRepo.GetUsersFromCourse(course);
            return users.Count();
        }

        public List<Result> GetLecturerResults(User lecturer)
        {
            return _resultRepo.GetLecturerResults(lecturer);
        }
    }
}
