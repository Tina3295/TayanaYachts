using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowBanner();
                ShowNews();
            }
        }


        protected void ShowBanner()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder bannerHtml = new StringBuilder();
            while (reader.Read())
            {
                string img = reader["FirstBannerImg"].ToString();
                string[] model = reader["YachtModel"].ToString().Split(' ');
                string isNewDesign = reader["NewDesign"].ToString();
                string isNewBuilding = reader["NewBuilding"].ToString();

                string tag = "";
                string displayTag = "0";
                if (isNewDesign.Equals("True"))
                {
                    displayTag = "1";
                    tag = "images/new02.png";
                }
                else if (isNewBuilding.Equals("True"))
                {
                    displayTag = "1";
                    tag = "images/new01.png";
                }


                bannerHtml.Append($"<li class='info' style='border-radius: 5px;height: 424px;width: 978px;'>" +
                    $"<a href='' target='_blank'><img src='images/{img}' style='width: 100%;height: 424px;border-radius: 5px'/></a>" +
                    $"<div class='wordtitle'>{model[0]} <span>{model[1]}</span><br /><p>SPECIFICATION SHEET</p></div>" +
                    $"<div class='new' style='display: none;overflow: hidden;border-radius:10px;'>" +
                    $"<img src='{tag}' alt='new' /></div><input type='hidden' value='{displayTag}' /></li>");
            }
            connection.Close();


            Banner.Text = bannerHtml.ToString();
            BannerNum.Text = bannerHtml.ToString();
        }








        protected void ShowNews()
        {
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            

            //計算設定的時間範圍內新聞數量
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT Top 3 *,convert(varchar, ReleaseDate, 111) as Date FROM News WHERE ReleaseDate<= @nowDate order by IsTop desc,ReleaseDate";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nowDate", nowDate);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            NewsRepeater.DataSource = reader;
            NewsRepeater.DataBind();
            connection.Close();



























        }

    }
}