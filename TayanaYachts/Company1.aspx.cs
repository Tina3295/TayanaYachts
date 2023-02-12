using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Company1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowCertificateContent();
                ShowVerticalImg();
                ShowHorizontalImg();
            }
        }


        protected void ShowCertificateContent()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateContent FROM Company WHERE id =1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                CertificateHTML.Text = HttpUtility.HtmlDecode(reader["CertificateContent"].ToString());
            }
            connection.Close();
        }


        protected void ShowVerticalImg()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateVerticalImg FROM Company WHERE id =1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string img = reader["CertificateVerticalImg"].ToString();
                if (!string.IsNullOrEmpty(img))
                {
                    try 
                    {
                        string[] images = img.Split(',');
                        foreach(string image in images)
                        {
                            VerticalImg.Text += "<li style='margin-bottom:10px'><p><img src='images/" + image + "' alt='Tayana' width='200'></p></li>";
                        }
                    }
                    catch
                    {
                        VerticalImg.Text = "<li style='margin-bottom:10px'><p><img src='images/" + img + "' alt ='Tayana' width ='200'></p></li>";
                    }
                }
               
            }
            connection.Close();

        }



        protected void ShowHorizontalImg()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateHorizontalImg FROM Company WHERE id =1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string img = reader["CertificateHorizontalImg"].ToString();
                if (!string.IsNullOrEmpty(img))
                {
                    try
                    {
                        string[] images = img.Split(',');
                        foreach (string image in images)
                        {
                            HorizontalImg.Text += "<li style='margin-bottom:10px'><p><img src='images/" + image + "' alt='Tayana' width='319px' height='234px'></p></li>";
                        }
                    }
                    catch
                    {
                        HorizontalImg.Text = "<li style='margin-bottom:10px'><p><img src='images/" + img + "' alt ='Tayana' width='319px' height='234px'></p></li>";
                    }
                }
                
            }
            connection.Close();

        }






    }
}