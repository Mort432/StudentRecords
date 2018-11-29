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
        public User authorisedUser { get; set; }

        public void Login(User user)
        {
            authorisedUser = user;
        }

        public void Logout()
        {
            authorisedUser = null;
        }
    }
}
