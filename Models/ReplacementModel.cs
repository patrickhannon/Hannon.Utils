using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hannon.Utils;

namespace Hannon.Util.Model
{
    [Serializable]
    public class ReplacementModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ReplacementModel(string name, string value)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("name",name);
            ArgumentValidator.ThrowOnNull("value", value);
            this.Name = name;
            this.Value = value;
        }
    }
}
