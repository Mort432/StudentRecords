using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsViewModels.ViewModels
{
    public class LoginViewModel
    {
        public List<User> AvailableUsers { get; set; }

        private IUsersService _usersService;
        private IAuthService _authService;

        public LoginViewModel(IUsersService usersService, IAuthService authService)
        {
            _usersService = usersService;
            _authService = authService;

            AvailableUsers = _usersService.GetAllUsers().Result.ToList();
        }

        public void Login(User user)
        {
            _authService.Login(user);
        }
    }
}
