using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;
using StudentRecordsRepositories.Repos;

namespace StudentRecordsServices.Services
{
    public class CoursesService : ICoursesService
    {
        ICourseRepo _courseRepo;

        public CoursesService(ICourseRepo courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _courseRepo.SelectAll();
        }

        public async Task<Course> GetCourseById(object id)
        {
            return await _courseRepo.SelectById(id);
        }
    }
}
