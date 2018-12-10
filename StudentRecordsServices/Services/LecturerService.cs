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

        public LecturerService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public List<User> GetLecturerStudents(User lecturer)
        {
            var courseStudents = _userRepo.Select(x => (x.Course != null) && lecturer.Course.Id.Equals(x.Course.Id) && x.Role == UserRole.Student);
            var moduleStudents = _userRepo.Select(x => lecturer.Enrollments.Any(z => z.Id.Equals(x.Id)));
            var myStudents = courseStudents.Result.ToList().Union(moduleStudents.Result.ToList()).ToList();

            return myStudents;
        }
    }
}
