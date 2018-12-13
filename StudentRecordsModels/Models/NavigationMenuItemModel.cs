using System;

namespace StudentRecordsModels.Models
{
    public class NavigationMenuItemModel
    {
        public string Content { get; set; }
        public string Glyph { get; set; }
        public Type ViewType { get; set; }
        public bool IsEnabled { get; set; } = true;

        public override string ToString() => Content;
    }
}
