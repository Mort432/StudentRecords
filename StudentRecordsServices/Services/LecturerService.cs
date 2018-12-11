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

        public LecturerService(IUserRepo userRepo, ICourseRepo courseRepo)
        {
            _userRepo = userRepo;
            _courseRepo = courseRepo;
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            var courseStudents = _userRepo.Select(x => (x.Course != null) && lecturer.Course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student);
            var moduleStudents = _userRepo.Select(x => lecturer.Enrollments.Any(z => z.Id.Equals(x.Id)));
            var myStudents = courseStudents.Result.ToList().Union(moduleStudents.Result.ToList()).ToList();

            return myStudents;
        }

        public int GetGraduatedStudents(Identifier courseIdentifier)
        {
            Course course = _courseRepo.SelectById(courseIdentifier.Id).Result;
            List<User> users = _userRepo.GetUsersFromCourse(course);
            return users.Count();
        }
    }
}
