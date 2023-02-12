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
    public partial class News_Detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowNews();
            }
        }



        private void ShowNews()
        {
            string guid = Request.QueryString["id"];
            //如果沒有網址傳值就導回新聞列表頁
            if (String.IsNullOrEmpty(guid))
            {
                Response.Redirect("News.aspx");
            }


            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM News WHERE guid = @guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid.Trim());
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                NewsTitle.Text = reader["NewsTitle"].ToString();

                NewsContent.Text = HttpUtility.HtmlDecode(reader["NewsContentHtml"].ToString());

                string newsImage= reader["NewsImage"].ToString();
                if (!string.IsNullOrEmpty(newsImage))
                {
                    if (newsImage.Contains(','))
                    {
                        string[] images = newsImage.Split(',');
                        foreach(string image in images)
                        {
                            NewsImages.Text+= "<p><img alt='Image' src='images/"+ image + "' style='width: 700px;' /></p>";
                        }
                    }
                    else
                    {
                        NewsImages.Text += "<p><img alt='Image' src='images/" + newsImage + "' style='width: 700px;' /></p>";
                    }

                }

               
            }
            connection.Close();

            
        }
    }
}