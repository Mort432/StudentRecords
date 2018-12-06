using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    public interface ICoursesService
    {
        Task<Course> GetCourseById(object id);

        Task<IEnumerable<Course>> GetAllCourses();
    }
}
