using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Hannon.Utils;

namespace Hannon.Util
{ 
    [Serializable]
    public static class XMLParser
    {
        private static XmlDocument _doc = new XmlDocument();
        public static string GetNodeValue(string xml, string xmlPath)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("xml", xml);
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("xmlPath", xmlPath);
            var cardType = string.Empty;
            try
            {
                _doc.LoadXml(xml);
                //Get the specific value from the XML path attribute
                //<Fieldid="TRXFIELD_D18">
                //<Name>CCType</Name>
                //<Value>Visa</Value>
                //</Field>
                cardType = _doc.SelectSingleNode("/Transactionpost/RequestData/Transaction/Fields/@TRXFIELD_D18").Value;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cardType;
        }
    }
}
