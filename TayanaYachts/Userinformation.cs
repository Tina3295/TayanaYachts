using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace TayanaYachts
{
    public class Userinformation
    {
        //登入user資訊
        public static string email;
        public static string name;

        public static string admin;
        public static bool yachtsadmin;
        public static bool newsadmin;
        public static bool companyadmin;
        public static bool dealersadmin;


        public static void PermissionCheck()
        {

            string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            string[] ticketUserDataArr = ticketUserData.Split(';');
            bool haveRight = HttpContext.Current.User.Identity.IsAuthenticated;
            if (haveRight)
            {
                //canView = true;
                admin=ticketUserDataArr[0];
                email = ticketUserDataArr[1];
                name = ticketUserDataArr[2] + ticketUserDataArr[3];
                yachtsadmin = Convert.ToBoolean(ticketUserDataArr[4]);
                newsadmin = Convert.ToBoolean(ticketUserDataArr[5]);
                companyadmin = Convert.ToBoolean(ticketUserDataArr[6]);
                dealersadmin = Convert.ToBoolean(ticketUserDataArr[7]);
            }





        }
    }
}