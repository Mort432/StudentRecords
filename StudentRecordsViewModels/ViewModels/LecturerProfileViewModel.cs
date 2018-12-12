using StudentRecordsModels.Models;
using StudentRecordsServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsViewModels.ViewModels
{
    public class LecturerProfileViewModel
    {
        public User selectedLecturer;

        private IAuthService _authService;

        public LecturerProfileViewModel(IAuthService authService)
        {
            _authService = authService;

            selectedLecturer = _authService.authorisedUser;
        }
    }
}
