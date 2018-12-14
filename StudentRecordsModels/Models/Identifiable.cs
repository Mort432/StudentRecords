using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    // Represents something that can be identified in a DB.
    public class Identifiable
    {
        public object Id { get; set; }

        public Identifier ToIdentifier()
        {
            return new Identifier(this);
        }
    }
}
