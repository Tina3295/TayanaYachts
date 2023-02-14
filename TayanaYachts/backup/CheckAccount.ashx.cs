using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TayanaYachts
{
    /// <summary>
    /// CheckAccount 的摘要描述
    /// </summary>
    public class CheckAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // ashx 裡的 Request/Response 都要加上 context
            string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            string[] ticketUserDataArr = ticketUserData.Split(';');
            bool haveRight = HttpContext.Current.User.Identity.IsAuthenticated;
            //依管理權限導頁
            if (haveRight)
            {
                if (ticketUserDataArr[0].Equals("Top"))
                {
                    context.Response.Redirect("UserManagement.aspx"); 
                }
                else
                {
                    context.Response.Redirect("News_Back.aspx"); 
                }
            }
            else
            {
                context.Response.Redirect("Login.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}