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

            string id = "";

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

                id= reader["NewID"].ToString();

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





            //取得附件資料
            string sql2 = "SELECT * FROM NewsAttachment WHERE NewID = @NewID";
            SqlCommand command2 = new SqlCommand(sql2, connection);
            command2.Parameters.AddWithValue("@NewID", id);
            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                Attachment.Text += "<li><a id='ctl00_ContentPlaceHolder1_RepFile_ctl01_HyperLink1' href='attachment/" + reader2["AttachmentName"].ToString() + "' download>" + reader2["AttachmentName"].ToString() + "</a></li>";
            }
            connection.Close();

            if(!string.IsNullOrEmpty(Attachment.Text))
            {
                AttachmentPanel.Visible = true;
            }

        }
    }
}