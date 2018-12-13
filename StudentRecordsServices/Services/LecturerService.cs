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

        public List<User> GetLecturerStudents(User lecturer)
        {
            return _userRepo.GetLecturerStudents(lecturer);
        }

        public int GetGraduatedStudents(Identifier courseIdentifier)
        {
            Course course = _courseRepo.SelectById(courseIdentifier.Id).Result;
            return _userRepo.CountGraduatedCourseUsers(course);
        }

        public List<Result> GetLecturerResults(User lecturer)
        {
            List<ModuleRun> lecturerModules = _moduleRunRepo.GetLecturerModuleRuns(lecturer);
            return _resultRepo.GetModuleRunsResults(lecturerModules);
        }
    }
}
