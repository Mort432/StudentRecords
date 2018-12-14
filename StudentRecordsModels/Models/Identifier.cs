using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    // Identifiers offer the ability to express relationships and simple data without fetching the entire related object.
    // This allows us to represent two-way relationships between data in MongoDB without creating infinite BSON trees.
    public class Identifier : IEquatable<Identifier>
    {
        //The DB Id.
        public object Id;
        //Shorthand information about the inheriting object. For example, a User's full name.
        public string Value;

        public Identifier(Identifiable identifiable)
        {
            Id = identifiable.Id;
            Value = identifiable.ToString();
        }

        // Allows for comparison between Identifiers.
        public override bool Equals(object obj) => Equals(obj as Identifier);
        public bool Equals(Identifier other) => other != null && EqualityComparer<object>.Default.Equals(Id, other.Id) && Value == other.Value;

        public override string ToString() => Value;

        // Offers a hash for comparisons, needed for Equality.
        public override int GetHashCode()
        {
            var hashCode = 409228620;
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
