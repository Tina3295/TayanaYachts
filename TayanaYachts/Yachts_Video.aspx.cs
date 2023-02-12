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
    public partial class Yachts_Video : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ShowVideo();
            }
        }

        protected void ShowVideo()
        {
            string guid = Session["guid"].ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT VideoUrl FROM Yachts WHERE guid = @guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            //DataTable dataTable = new DataTable();
            //dataTable.Columns.AddRange(new DataColumn[1] { new DataColumn("VideoUrl") });




            string videoID;

            if (reader.Read())
            {
                string video = reader["VideoUrl"].ToString();

                if(!string.IsNullOrEmpty(video))
                {
                    if(video.Contains("="))   //斯斯有兩種，youtube網址也有兩種
                    {
                        int x = video.LastIndexOf('=');
                        videoID = video.Remove(0, x + 1);
                    }
                    else
                    {
                        int x = video.LastIndexOf('/');
                        videoID = video.Remove(0, x + 1);
                    }



                    //dataTable.Rows.Add($"https://www.youtube.com/embed/"+videoID);
                    //Page.DataSource = dataTable;
                    //RepeaterImg.DataBind();




                    VideoPlay.Text = "<iframe style='display: flex' width='100%' height='400' src='https://www.youtube.com/embed/" + videoID + "' title='YouTube video player' frameborder='0' allow='accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share' allowfullscreen></iframe>";
                    

                }
                else   //如果沒影片導回overview
                {
                    Response.Redirect($"Yachts_OverView.aspx?id={guid}");
                }
            }
            connection.Close();
        }
    }
}