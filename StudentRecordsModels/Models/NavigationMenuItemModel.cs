using System;

namespace StudentRecordsModels.Models
{
    // This represents the item model for the nav bar on the left.
    public class NavigationMenuItemModel
    {
        // The text in the nav bar
        public string Content { get; set; }
        // The symbol in the nav bar (not supported in WPF)
        public string Glyph { get; set; }
        // Refers to the view that will be loaded when clicked.
        public Type ViewType { get; set; }
        public bool IsEnabled { get; set; } = true;

        public override string ToString() => Content;
    }
}
