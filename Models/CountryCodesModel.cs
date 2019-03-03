using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hannon.Utils;

namespace Hannon.Util
{
    [Serializable]
    public class CountryCodesModel
    {
        public CountryCodesModel(string countryCode, string currency)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("currency",currency);
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("countryCode",countryCode);
            Currency = currency;
            CountryCode = countryCode;
        }
        public string Currency { get; set; }
        public string CountryCode { get; set; }
    }
}
