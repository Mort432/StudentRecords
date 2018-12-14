using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentRecordsModels.Models;

namespace StudentRecordsServices.Services
{
    //Defines the behaviour that must be implemented by an AuthService
    public interface IAuthService
    {
        User authorisedUser { get; set; }

        void Login(User user);
        void Logout();
    }
}
