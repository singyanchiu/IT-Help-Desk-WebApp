using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ProjectManagementApp.Utils
{
    public class TextTools
    {
        public static string EmailToName(string email)
        {
            MailAddress addr = new MailAddress(email);
            string username = addr.User;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(username);
        }
    }
}