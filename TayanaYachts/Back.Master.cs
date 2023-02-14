using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Back : System.Web.UI.MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            //清除Cache，避免登出後按上一頁還會顯示Cache頁面
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();


            //權限關門判斷 (Cookie)
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login.aspx"); //導回登入頁
            }
            else
            {
                Userinformation.PermissionCheck();

                //管理權限顯示區塊
                if (Userinformation.admin == "General")
                {
                    AdministratorOnly.Visible = false;

                    if (!Userinformation.yachtsadmin)
                    {
                        YachtsHide.Visible = false;
                        YachtsContentHide.Visible = false;
                    }
                    if (!Userinformation.newsadmin)
                    {
                        NewsHide.Visible = false;
                    }
                    if (!Userinformation.companyadmin)
                    {
                        CompanyHide.Visible = false;
                    }
                    if (!Userinformation.dealersadmin)
                    {
                        DealersHide.Visible = false;
                    }

                }
                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Name.Text = Userinformation.name;
            }
        }


        protected void Logout_Click(object sender, EventArgs e)
        {

            if (Session != null)
            {
                Session.Abandon();
                Session.RemoveAll();
            }

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            authenticationCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(authenticationCookie);

            // 執行登出
            FormsAuthentication.SignOut();

            // 轉向到你登出後要到的頁面
           
            Response.Redirect("Login.aspx");
        }
    }
}