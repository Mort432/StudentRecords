using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using StudentRecordsServices.Services;
using StudentRecordsUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsUI.ViewModels
{
    class MainPageViewModel
    {
        public IEnumerable<NavigationMenuItemModel> MenuItems { get; set; }

        private IAuthService _authService;

        public MainPageViewModel(IAuthService authService)
        {
            _authService = authService;

            MenuItems = GetMenuItems();
        }

        public IEnumerable<NavigationMenuItemModel> GetMenuItems()
        {
            User currentUser = _authService.authorisedUser;
            List<NavigationMenuItemModel> menuItems = new List<NavigationMenuItemModel>();

            menuItems.Add(new NavigationMenuItemModel()
            {
                Content = "Home",
                Glyph = char.ConvertFromUtf32(0xE80F).ToString(),
                ViewType = typeof(Home)
            });

            switch (currentUser.Role)
            {
                case UserRole.Student:
                    menuItems.Add(new NavigationMenuItemModel()
                    {
                        Content = "Student Profile",
                        Glyph = char.ConvertFromUtf32(0xE77B).ToString(),
                        ViewType = typeof(StudentProfile)
                    });
                    menuItems.Add(new NavigationMenuItemModel()
                    {
                        Content = "Student Enrollments",
                        Glyph = char.ConvertFromUtf32(0xE70F).ToString(),
                        ViewType = typeof(StudentEnrollments)
                    });
                    break;
                case UserRole.Lecturer:
                    menuItems.Add(new NavigationMenuItemModel()
                    {
                        Content = "Lecturer Profile",
                        Glyph = char.ConvertFromUtf32(0xE77B).ToString(),
                        ViewType = typeof(LecturerProfile)
                    });
                    menuItems.Add(new NavigationMenuItemModel()
                    {
                        Content = "Manage Students",
                        Glyph = char.ConvertFromUtf32(0xE70F).ToString(),
                        ViewType = typeof(LecturerStudentManagement)
                    });
                    break;
                case UserRole.Admin:
                    break;
            }

            return menuItems;
        }
    }
}
