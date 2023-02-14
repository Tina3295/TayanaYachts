using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class AdministratorRights : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Userinformation.admin == "General")
            {
                AdministratorOnly.Visible = false;

                tip.Text = "You don't have permission to browse this page!";
            }
            if (!IsPostBack)
            {
                Show();
                Master.Page.Title = "Administrator Rights";
            }



            AdminChange();
            Show();
        }

        private void Show()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Users where Permission='General'";
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
            connection.Close();
        }


        protected void AdminChange()
        {
            if (Request["id"] != null)
            {

                if (Request["admin"] == "True")
                {
                    string config = WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString;
                    SqlConnection connection = new SqlConnection(config);
                    SqlCommand command = new SqlCommand($"update Users set "+Request["system"]+"=@admin where UserID=@UserID", connection);

                    command.Parameters.AddWithValue("@admin", "False"); 
                    command.Parameters.AddWithValue("@UserID", Request["id"]);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string config = WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString;
                    SqlConnection connection = new SqlConnection(config);
                    SqlCommand command = new SqlCommand($"update Users set " + Request["system"] + "=@admin where UserID=@UserID", connection);

                    command.Parameters.AddWithValue("@admin", "True");

                    command.Parameters.AddWithValue("@UserID", Request["id"]);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }


            }
        }
    }
}