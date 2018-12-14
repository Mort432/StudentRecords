using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos
{
    //Declares the functionality a User repo must implement.
    public interface IUserRepo : IRepo<User>
    {
        //This gets all the users on argument course.
        List<User> GetUsersFromCourse(Course course);
        //Fetch all students.
        Task<IEnumerable<User>> GetAllStudents();
        //Fetch all lecturers.
        Task<IEnumerable<User>> GetAllLecturers();
        //Fetches every student under the authority of a lecturer.
        //This includes students in the lecturer's course, and students in lecturer's modules.
        List<User> GetLecturerStudents(User lecturer);
        //Counts the users who have graduated in a given course.
        int CountGraduatedCourseUsers(Course course);
    }
}
