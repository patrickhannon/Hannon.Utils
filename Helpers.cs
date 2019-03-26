using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Hannon.Utils.Models;
using Hannon.Util.Model;
using Hannon.Utils;
using Hannon.Utils.Models;

namespace Hannon.Util
{
    public static class Helpers
    {
        public static string Marks_email_domain = "@hannon.com";
        public static readonly int MAX_STRING_LENGTH = 220;
        public static readonly int MARK_NO_MIN_LEN = 5;
        public static readonly int MAX_DESC_LEN = 101;
        public static string DelimiterForSEO = ", ";
        public static string Underscore = "_";
        public static string CountryCodeUSA = "USA";
        public static int ISO_CountryCode = 3;
        public static string Country = "United States";

        public static Dictionary<int, string> PayfabricErrorCodes = new Dictionary<int, string>()
        {
            {1691, "Declined"},{1000, "Failure"}
        };

        public enum ZipCodeType
        {
            Standard, Plus4, Known
        };

        /// <summary>
        /// Returns a number of days past since goLive
        /// </summary>
        /// <returns>int</returns>
        public static int GetGoLiveDaysPast()
        {
            var now = DateTime.Now;
            var goLive = new DateTime(2014, 04, 02);
            var totalDaysPast = now - goLive;
            return Math.Abs(totalDaysPast.Days);
        }

        public static int GetZipCodeType(string zipToCheck)
        {
            var zipToCheckStd = "76017";
            var zipToCheckPlus4 = "76017-4198";
            var stdZip = @"\d{5}$";
            var stdZipPlus4 = @"\d{5}-\d{4}$";
            //^\d{5}-\d{4}
            if (Regex.IsMatch(zipToCheck, stdZip))
            {
                return (int)ZipCodeType.Standard;
                //Debug.WriteLine(string.Format("This is a std zip code:{0}", zipToCheckStd));
            }
            else if (Regex.IsMatch(zipToCheck, stdZipPlus4))
            {
                return (int)ZipCodeType.Plus4;
                //Debug.WriteLine(string.Format("This is a zip plus 4 zip code:{0}", zipToCheckPlus4));
            }
            return (int)ZipCodeType.Known;
        }

        /// <summary>
        /// Truncates a string to the defined length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns>string length to max length</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string FormatCustomerNumber(string customerNumber, string zip)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("customerNumber", customerNumber);
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("zip", zip);

            if (Helpers.GetZipCodeType(zip) == (int)Helpers.ZipCodeType.Standard)
            {
                 return string.Format("{0}-{1}A", customerNumber, zip);
            }
            else if (Helpers.GetZipCodeType(zip) == (int)Helpers.ZipCodeType.Plus4)
            {
                //only get the first 5 digits for zipCode
                var zipCode = zip.Substring(0, 5);
                return string.Format("{0}-{1}A", customerNumber, zip);
            }
            //Worst case, make sure the customer number is created
            return customerNumber;
        }

        //Currency
        public static string UsCurrency = "USD";
        public static string PayfabricTransactionType = "Book";
        public static string ImagePathWeb = @"/Content/Images/Catalog/fingers/";
        private static char[] delimiters = {'\\'};
        

        private static IEnumerable<SelectListItem> _states = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "Texas", Value = "TX"}
        };

        private static IEnumerable<SelectListItem> _countries = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "United States", Value = "US"}
        };

        public enum VendorPriority
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public enum TransactionType
        {
            Sale,
            Book,
        }

        public static string GetCardTypeAbbreviated(string longName)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("longName", longName);
            var trim = longName.Trim();
            switch (trim)
            {
                case "Visa":
                    return "VISA";
                case "Discover":
                    return "DISC";
                case "AmericanExpress":
                    return "AMEX";
                case "MasterCard":
                    return "M/C";
                default:
                    return "VISA";
            }
        }

        /// <summary>
        /// Gets the environment name based on the connection string, then cached in Application State.
        /// </summary>
        //public static string EnvironmentName
        //{
        //    get
        //    {
        //        if (System.Web.HttpContext.Current.Application["EnvironmentName"] == null)
        //        {
        //            var environmentName = "UNKNOWN";
        //            var connectionString = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("OrderEntry").ConnectionStringWithoutCredentials;

        //            if (Regex.IsMatch(connectionString, "SQLPROD", RegexOptions.IgnoreCase))
        //                environmentName = "PRODUCTION";
        //            else if (Regex.IsMatch(connectionString, "SQLDEV", RegexOptions.IgnoreCase))
        //                environmentName = "DEV/TEST";

        //            System.Web.HttpContext.Current.Application["EnvironmentName"] = environmentName;
        //        }

        //        return (string)System.Web.HttpContext.Current.Application["EnvironmentName"];
        //    }
        //} // CurrentAssemblyInfo


        public static Dictionary<string, CountryCodesModel> CountryCodes = new Dictionary<string, CountryCodesModel>()
        {
            {"USA", new CountryCodesModel("US", "USD")}
        };


        private static HashSet<ReplacementModel> _replacementValues = new HashSet<ReplacementModel>()
        {
            new ReplacementModel("&", " and "),
            new ReplacementModel("@", " at "),
            new ReplacementModel("#", " number "),
            new ReplacementModel("%", " percent "),
            new ReplacementModel("?", " question "),
            new ReplacementModel("'", " "),
            new ReplacementModel("=", " equal "),
            new ReplacementModel(";", " ")
        };


        public static List<StateModel> GetNonContiguousUsStates()
        {
            var nonContiguousUsStates = new List<StateModel>();

            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "PR", Description = "PUERTO RICO" });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "CN", Description = "CANADA" });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "MX", Description = "MEXICO" });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "VI", Description = "VIRGIN ISLANDS" });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "TT", Description = "TRUST TERRITORIES" });

            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "CM", Description = "NORTHERN MARIANA IS." });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "HI", Description = "HAWAII" });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "GU", Description = "GUAM" });
            nonContiguousUsStates.Add(new StateModel() { Abbreviation = "CZ", Description = "CANAL ZONE" });

            return nonContiguousUsStates;
        }
        
        public static readonly CultureInfo UnitedStates = CultureInfo.GetCultureInfo("en-US");

        /// <summary>
        /// Returns a default SelectListItem
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns>returns a default SelectListItem</returns>
        public static SelectListItem GetDefaultItem(string text, string value)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("text", text);
            ArgumentValidator.ThrowOnNull("value", value);

            return new SelectListItem()
            {
                Text = text,
                Value = value
            };
        }

        public static string GetUserName(string ADUsername, string delimiter)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("ADUsername", ADUsername);
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("delimiter", delimiter);

            string[] stringSeparators = new string[] {delimiter};
            var result = ADUsername.Split(stringSeparators, StringSplitOptions.None);
            return result[1];
        }

        public static bool ResponseStatus(HttpStatusCode status)
        {
            switch (status)
            {
                case HttpStatusCode.OK:
                    return true;
                case HttpStatusCode.PreconditionFailed:
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Used to replace characters in a string
        /// </summary>
        /// <param name="value">string to use</param>
        /// <param name="replaceWithReadableForm">Used to replace include a replacement value if set to true</param>
        /// <returns>updated string</returns>
        public static ResponseMessage RemoveUnwantedCharacters(string value,
            bool replaceWithReadableForm)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("value", value);
            var response = new ResponseMessage();

            if (!VerifyUnwantedCharacters(value))
            {
                return new ResponseMessage()
                {
                    Message = value,
                    Status = false
                };
            }

            foreach (var replacementValue in _replacementValues)
            {
                //replace if requested with readable form
                if (replaceWithReadableForm)
                {
                    if (value.Contains(replacementValue.Name))
                    {
                        value = value.Replace(replacementValue.Name, replacementValue.Value);
                        response.Message = value;
                        response.Status = true;
                    }
                }
                else
                {
                    //otherwise just remove the given string
                    if (value.Contains(replacementValue.Name))
                    {
                        value = value.Replace(replacementValue.Name, "");
                        response.Message = value;
                        response.Status = true;
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Returns whether the char is a digit char.
        /// Taken from inside the char.IsDigit method.
        /// </summary>
        public static bool IsCharDigit(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }

        private static bool VerifyUnwantedCharacters(string value)
        {
            ArgumentValidator.ThrowOnNullEmptyOrWhitespace("value", value);
            foreach (var replacementValue in _replacementValues)
            {
                if (value.Contains(replacementValue.Name))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<SelectListItem> GetStates()
        {
            return _states;
        }

        public static IEnumerable<SelectListItem> GetCountries()
        {
            return _countries;
        }

        /// <summary>
        /// Splits a string based on a specific delimiter
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        /// <param name="delimiter"></param>
        public static void TrySplit(string data, out string user, char delimiter)
        {
            //If the string does not contain the delimiter just return
            if (data.ToLower().Contains('\\'))
            {
                var value = data.Split(delimiter);
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    user = value[1];
                }
                else
                {
                    user = string.Empty;
                }
            }
            else
            {
                user = data;
            }
        }

        /// <summary>
        /// Format required: MMYY to match Credit Card expiration dates in GP
        /// </summary>
        /// <param name="dateToCheck"></param>
        /// <returns></returns>
        public static bool InvalidOrDateInPast(string dateToCheck)
        {
            //format: MMYY
            DateTime date;
            int mm;
            int yy;
            var month = dateToCheck.Substring(0, 2);
            var year = dateToCheck.Substring(2, 2);
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(month) || string.IsNullOrEmpty(year))
            {
                return true;
            }

            //make the year 4 digits
            var fullYear = "20" + year;
            if (Int32.TryParse(month, out mm) && Int32.TryParse(fullYear, out yy))
            {
                //create a new date, then do a compare
                date = new DateTime(yy, mm, 1);
                if (date < now)
                {
                    return true;
                }
            }
            return false;
        }

        public static string FilePathToFileUrl(string filePath)
        {
            StringBuilder uri = new StringBuilder();
            foreach (char v in filePath)
            {
                if ((v >= 'a' && v <= 'z') || (v >= 'A' && v <= 'Z') || (v >= '0' && v <= '9') ||
                    v == '+' || v == '/' || v == ':' || v == '.' || v == '-' || v == '_' || v == '~' ||
                    v > '\xFF')
                {
                    uri.Append(v);
                }
                else if (v == Path.DirectorySeparatorChar || v == Path.AltDirectorySeparatorChar)
                {
                    uri.Append('/');
                }
                else
                {
                    uri.Append(String.Format("%{0:X2}", (int) v));
                }
            }
            if (uri.Length >= 2 && uri[0] == '/' && uri[1] == '/') // UNC path
                uri.Insert(0, "file:");
            else
                uri.Insert(0, "file:///");
            return uri.ToString();
        }

        public static string ParseString(string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > maxLength)
                {
                    return value.Substring(0, maxLength);
                }
            }
            return value;
        }

        public static string RemoveUnwantedChars(string[] charsToRemove, string value)
        {
            foreach (var c in charsToRemove)
            {
                value = value.Replace(c, "");
            }
            return value;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            char[] buffer = new char[str.Length];
            int idx = 0;

            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z') || (c == '.') || (c == '_'))
                {
                    buffer[idx] = c;
                    idx++;
                }
            }

            return new string(buffer, 0, idx);
        }
        
        public static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                Regex rx = new Regex(
                    @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(emailaddress);
            }
            catch (FormatException)
            {
                return false;
            }
        }
        /// <summary>
        /// Strips out anything that is not a number
        /// </summary>
        /// <param name="input">value to strip</param>
        /// <returns>empty string if null, empty or whitespace, Or numerical string only</returns>
        public static string GetNumbers(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        /// <summary>
        /// Makes a comparison between a list and an actual value
        /// If it exists in the list then return true
        /// </summary>
        /// <param name="list">list to compare against</param>
        /// <param name="value">actual value to compare</param>
        /// <returns>bool:true/false</returns>
        public static bool IsIncluded(string list, string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ||
                        string.IsNullOrEmpty(list) || string.IsNullOrWhiteSpace(list))
            {
                return false;
            }
            List<string> values = list.Split(',').ToList();
            if (values.Contains(value))
            {
                return true;
            }
            return false;
        }
    }
}
