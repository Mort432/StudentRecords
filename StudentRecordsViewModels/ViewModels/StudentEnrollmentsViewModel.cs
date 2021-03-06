﻿using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsViewModels.ViewModels
{
    public class StudentEnrollmentsViewModel
    {
        public User selectedUser { get; }

        public List<Identifier> allModuleRuns = new List<Identifier>();
        public List<Identifier> allCourses { get; }

        //Inject dependancies
        private IAuthService _authService;
        private IUsersService _usersService;
        private IModulesService _modulesService;
        private ICoursesService _coursesService;

        public StudentEnrollmentsViewModel(IAuthService authService, IUsersService usersService, IModulesService modulesService, ICoursesService coursesService)
        {
            _authService = authService;
            _usersService = usersService;
            _modulesService = modulesService;
            _coursesService = coursesService;

            selectedUser = _authService.authorisedUser;
            allModuleRuns = _modulesService.GetAllModuleRuns().Result.Select(x => x.ToIdentifier()).ToList();
            allCourses = _coursesService.GetAllCourses().Result.Select(x => x.ToIdentifier()).ToList();
        }

        //Get module runs the user is not enrolled on
        public List<Identifier> GetAvailableModules
        {
            get
            {
                var returnVal = allModuleRuns.Where(p => !selectedUser.Enrollments.Any(x => x.Id.Equals(p.Id))).ToList();
                return returnVal;
            }
        }

        //Get module runs the user is enrolled on
        public List<Identifier> GetCurrentModules
        {
            get
            {
                return new List<Identifier>(selectedUser.Enrollments);
            }
        }

        public void EnrollUserToModule(Identifier module)
        {
            selectedUser.Enrollments.Add(module);
            _usersService.UpdateUser(selectedUser);
        }

        public void UnenrollUserFromModule(Identifier module)
        {
            selectedUser.Enrollments.Remove(module);
            _usersService.UpdateUser(selectedUser);
        }

        public void ChangeUserCourse(Identifier course)
        {
            selectedUser.Course = course;
            _usersService.UpdateUser(selectedUser);
        }
    }
}
