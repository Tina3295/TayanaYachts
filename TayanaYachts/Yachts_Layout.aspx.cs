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
    public partial class Yachts_Layout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowLayout();
        }
        protected void ShowLayout()
        {
            string guid = Session["guid"].ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT LayoutImg FROM Yachts WHERE guid = @guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {
                string layoutImg = reader["LayoutImg"].ToString();


                if (!string.IsNullOrEmpty(layoutImg))
                {
                    try 
                    {
                        string[] images = layoutImg.Split(',');
                        foreach(string image in images)
                        {
                            LayoutImg.Text += "<li><img src = '/images/" + image + "' alt = '&quot;&quot;' width='670px'></ li >";
                        }
                    }
                    catch
                    {
                        LayoutImg.Text += "<li><img src = '/images/" + layoutImg + "' alt = '&quot;&quot;' width='670px'></ li >";
                    }
                }
                
            }
            connection.Close();
        }
    }
}
