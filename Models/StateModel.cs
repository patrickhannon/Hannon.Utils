using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hannon.Utils

{
    [Serializable]
    public class StateModel
    {
        public string Description { get; set; }
        public string Abbreviation { get; set; }

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;
            StateModel s = (StateModel) obj;
            return (Abbreviation == s.Abbreviation) && (Description == s.Description);
        }

        public override int GetHashCode()
        {
            return Description.GetHashCode() ^ Abbreviation.GetHashCode();
        }
    }
}
