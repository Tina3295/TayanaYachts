using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Back : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Name.Text = Userinformation.name;

            //    if (Userinformation.permission == "3")
            //    {
            //        SystemHide.Visible = false;
            //    }


            //    Userinformation.Admin();
            //    if (Userinformation.videoadmin == false)
            //    {
            //        VideoHide.Visible = false;
            //    }
            //    if (Userinformation.albumadmin == false)
            //    {
            //        AlbumHide.Visible = false;
            //    }
            //    if (Userinformation.faqadmin == false)
            //    {
            //        FAQHide.Visible = false;
            //    }
            //    if (Userinformation.linkadmin == false)
            //    {
            //        LinkHide.Visible = false;
            //    }



            //    if (Userinformation.permission != "1")
            //    {
            //        AdministratorOnly.Visible = false;
            //    }
            //}

        }




        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}