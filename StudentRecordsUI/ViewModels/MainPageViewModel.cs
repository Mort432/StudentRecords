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
                        Content = "Students",
                        Glyph = char.ConvertFromUtf32(0xE77B).ToString(),
                        ViewType = typeof(Students)
                    });
                    break;
                case UserRole.Lecturer:
                    break;
                case UserRole.Admin:
                    break;
            }

            return menuItems;
        }
    }
}
