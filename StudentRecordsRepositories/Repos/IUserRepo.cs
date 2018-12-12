using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    public interface IUserRepo : IRepo<User>
    {
        List<User> GetUsersFromCourse(Course course);
        Task<IEnumerable<User>> GetAllStudents();
        Task<IEnumerable<User>> GetAllLecturers();
        List<User> GetLecturerStudents(User lecturer);
    }
}
