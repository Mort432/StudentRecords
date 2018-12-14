using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsServices.Services
{
    public class AuthService : IAuthService
    {
        //The currently logged in user
        public User authorisedUser { get; set; }

        //Log the user in
        public void Login(User user)
        {
            authorisedUser = user;
        }

        //Log the user out
        public void Logout()
        {
            authorisedUser = null;
        }
    }
}
