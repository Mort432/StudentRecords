using System;
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
        //Inject dependancies
        private IUserRepo _userRepo;
        private ICourseRepo _courseRepo;
        private IModuleRunRepo _moduleRunRepo;
        private IResultRepo _resultRepo;

        public LecturerService(IUserRepo userRepo, ICourseRepo courseRepo, IResultRepo resultRepo, IModuleRunRepo moduleRunRepo)
        {
            _userRepo = userRepo;
            _courseRepo = courseRepo;
            _resultRepo = resultRepo;
            _moduleRunRepo = moduleRunRepo;
        }

        //Get a lecturer's students.
        //This includes any students in modules they teach, and any students in their course if they lead one.
        public List<User> GetLecturerStudents(User lecturer)
        {
            return _userRepo.GetLecturerStudents(lecturer);
        }

        //Get a count of students graduated on a course
        public int GetGraduatedStudents(Identifier courseIdentifier)
        {
            Course course = _courseRepo.SelectById(courseIdentifier.Id).Result;
            return _userRepo.CountGraduatedCourseUsers(course);
        }

        //Get all the results from every module a lecturer teaches.
        public List<Result> GetLecturerResults(User lecturer)
        {
            List<ModuleRun> lecturerModules = _moduleRunRepo.GetLecturerModuleRuns(lecturer);
            return _resultRepo.GetModuleRunsResults(lecturerModules);
        }
    }
}
