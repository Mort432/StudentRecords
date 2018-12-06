using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsModels.Models
{
    class Identifier : IEquatable<Identifier>
    {
        public object Id;
        public string Value;

        public Identifier(Identifiable identifiable)
        {
            Id = identifiable.Id;
            Value = identifiable.ToString();
        }

        public override bool Equals(object obj) => Equals(obj as Identifier);
        public bool Equals(Identifier other) => other != null && EqualityComparer<object>.Default.Equals(Id, other.Id) && Value == other.Value;

        public override string ToString() => Value;

        public override int GetHashCode()
        {
            var hashCode = 409228620;
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }
    }
}
