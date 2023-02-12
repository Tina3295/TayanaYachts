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
    public partial class Yachts_OverView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadContent();
        }

        protected void LoadContent()
        {
            string guid = Session["guid"].ToString();
            string id = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts WHERE guid = @guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            

            if (reader.Read())
            {
                id = reader["YachtID"].ToString();
                string yachtModel = reader["YachtModel"].ToString();
                OverviewContent.Text = HttpUtility.HtmlDecode(reader["OverviewContent"].ToString());
                OverviewDimensions.Text = HttpUtility.HtmlDecode(reader["OverviewDimensionsHtml"].ToString());
   
                string[] num = yachtModel.Split(' ');
                ModelNum.Text = num[1];                  //渲染dimensions num
            }
            connection.Close();






            //取得附件資料
            string sql2 = "SELECT * FROM YachtsAttachment WHERE YachtID = @YachtID";
            SqlCommand command2 = new SqlCommand(sql2, connection);
            command2.Parameters.AddWithValue("@YachtID", id);
            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                Attachment.Text += "<li><a id='ctl00_ContentPlaceHolder1_RepFile_ctl01_HyperLink1' href='attachment/" + reader2["AttachmentName"].ToString() + "' download>" + reader2["AttachmentName"].ToString() + "</a></li>";
            }
            connection.Close();

        }
    }
}