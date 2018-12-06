using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    public class NavigationMenuItemModel
    {
        public string Content { get; set; }
        public string Glyph { get; set; }
        public Type ViewType { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
