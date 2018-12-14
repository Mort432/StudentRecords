using StudentRecordsModels.Models;
using StudentRecordsModels.Models.Enums;
using StudentRecordsServices.Services;
using StudentRecordsWPFUI.Pages;
using System.Collections.Generic;

namespace StudentRecordsWPFUI.ViewModels
{
    public class MainPageViewModel
    {
        public IEnumerable<NavigationMenuItemModel> MenuItems => GetMenuItems();

        //Inject dependencies
        private IAuthService _authService;

        public MainPageViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        //Set up the menu items, choosing based on the authorised user's role.
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
                    menuItems.Add(new NavigationMenuItemModel()
                    {
                        Content = "Analytics",
                        Glyph = char.ConvertFromUtf32(0xE9D2).ToString(),
                        ViewType = typeof(LecturerStudentAnalytics)
                    });
                    break;
                case UserRole.Admin:
                    break;
            }

            menuItems.Add(new NavigationMenuItemModel()
            {
                Content = "Log Out",
                Glyph = char.ConvertFromUtf32(0xE748).ToString(),
                ViewType = typeof(Login)
            });

            return menuItems;
        }

        public void LogOut()
        {
            _authService.Logout();
        }
    }
}
