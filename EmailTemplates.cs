using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hannon.Util
{
    public static class EmailTemplates
    {
        #region GuestEmail
        public static string GuestEmail = @"<!DOCTYPE html>" +
    "<html lang='en' xmlns='http://www.w3.org/1999/xhtml'>" +
    "<head>" +
        "<meta charset='utf-8' />" +
        "<title>Mark's Guest Login</title>" +
        "<style>" +
            ".bolder{" +
            "	color: red;" +
            "	font: italic bold 12px/30px Georgia, serif;" +
            "}" +
            ".note{" +
            "	font: italic bold 11px/30px Georgia, serif;" +
            "}" +
            ".noteSmall{" +
            "	font: italic 11px/30px Georgia, serif;" +
            "}" +
        "</style>" +
    "</head>" +
    "<body>" +
            "<table  style='width: 100%; border:1px; color=black;'>" +
                "<tr>" +
                    "<th>" +
                        "<img src='https://www.markspp.com/Content/images/MarksCyanLogo.png'>" +
                        "</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th></th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th></th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th></th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th>Hi #FullName, Welcome to Mark's</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th class='bolder'>You will be viewing list prices only!  To view your discounted pricing structure please complete the full registration process.</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th>" +
                            "<a href='https://#Url/Account/Register'>Click here to complete the full registration process.</a>" +
                        "</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th class='note'>If you select this option, you will be directed to registering for an online account</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th>" +
                            "&nbsp;" +
                        "</th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th><a href='https://#Url/Account/GuestValidate?guestId=#SessionId'>Click here to continue shopping as a guest</a></th>" +
                    "</tr>" +
                    "<tr>" +
                        "<th class='noteSmall'>This link is available for 24 hours.</th>" +
                    "</tr>";
        #endregion
        #region WebCartReminder

        public static string WebCartReminder = @"
            <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>" +
            "<head>" +
            "<meta charset='utf-8' />" +
            "<title>Marks email cart reminder</title>" +
            "<style>" +
            ".body {" +
            "	font-size: 19px;" +
            "}" +
            ".bolder {" +
            "	font-size: 14px;" +
            "	font-family: Verdana, Helvetica, Sans-Serif;" +
            "}" +
            ".tr, td {" +
            "	font-size: 14px;" +
            "	font-family: Verdana, Helvetica, Sans-Serif;" +
            "}" +
            ".tdPart {" +
            "	max-width: 200px;" +
            "}" +
            "" +
            ".note {" +
            "	font: italic bold 11px/30px Georgia, serif;" +
            "}" +
            ".header {" +
            "	background-color: #0092CF;" +
            "	color: white;" +
            "	padding: 4px;" +
            "}" +
            "</style>" +
            "</head>" +
            "<body>" +
            "<table style='border:1px; color=black;'>" +
            "<tr>" +
            "<th></th> " +
            "</tr>" +
            "<tr>" +
            "<th><img src='https://www.markspp.com/Content/images/MarksCyanLogo.png'></th>" +
            "</tr>" +
            "<tr>" +
            "<th>Hi #FullName</th>" +
            "</tr>" +
            "<tr>" +
            "<th class='bolder'>This is a friendly reminder your shopping cart currently has items in it.</th>" +
            "</tr>" +
            "<tr>" +
            "<th class='bolder'>Please login to review.</th>" +
            "</tr>" +
            "<th><a href='https://#Url'>Click here to view your cart.</a></th>" +
            "</tr>" +
            "</table>";
        #endregion

        public static string CartItemHeader = "<table style='border:1px; color=black;'><tr class='header'><th></th><th>Part #</th><th>Description</th><th>Qty</th></tr>"; //"<th>Price</th><th>Ext. Price</th></tr>";
        public static string Footer = @"</table></body></html>";
    }
}
