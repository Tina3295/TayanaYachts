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
    public partial class Tayana_Gallery : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetGuid();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowGallery();
                LoadLeftMenu();
                LoadTopMenu();
            }
        }
        private void GetGuid()
        {
            string guid = Request.QueryString["id"];
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT TOP 1 Guid FROM Yachts order by YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //預設第一筆遊艇型號的 GUID
                if (String.IsNullOrEmpty(guid))
                {
                    guid = reader["Guid"].ToString().Trim();
                }
            }
            connection.Close();

            Session["Guid"] = guid;
        }





        private void ShowGallery()
        {
            string guid = Session["guid"].ToString();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT BannerImg FROM Yachts WHERE Guid = @Guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[1] { new DataColumn("ImageUrl") });


            if (reader.Read())
            {
                string[] bannerimgs = reader["BannerImg"].ToString().Split(',');

                foreach (var item in bannerimgs)
                {
                    dataTable.Rows.Add($"/Images/{item}");
                }
            }
            connection.Close();

            RepeaterImg.DataSource = dataTable;
            RepeaterImg.DataBind();
        }


        private void LoadLeftMenu()
        {
            string urlPath = System.IO.Path.GetFileName(Request.PhysicalPath);

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts order by YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder leftMenuHtml = new StringBuilder();
            while (reader.Read())
            {
                string yachtModel = reader["YachtModel"].ToString();
                string isNewDesign = reader["NewDesign"].ToString();
                string isNewBuilding = reader["NewBuilding"].ToString();
                string guid = reader["Guid"].ToString();
                string isNew = "";

                if (isNewDesign.Equals("True"))
                {
                    isNew = "(New Design)";
                }
                else if (isNewBuilding.Equals("True"))
                {
                    isNew = "(New Building)";
                }
                leftMenuHtml.Append($"<li><a href='{urlPath}?id={guid}'>{yachtModel} {isNew}</a></li>");
            }
            connection.Close();

            ModelMenu.Text = leftMenuHtml.ToString();
        }



        private void LoadTopMenu()
        {
            string guid = Session["guid"].ToString();
            string id = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts WHERE guid = @guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder topMenuHtml= new StringBuilder();

            if (reader.Read())
            {
                id = reader["YachtID"].ToString();
                string yachtModel = reader["YachtModel"].ToString();
                

                //黑色分類連結列表
                topMenuHtml.Append($"<li><a class='menu_yli01' href='Yachts_OverView.aspx?id={guid}' >OverView</a></li>");
                topMenuHtml.Append($"<li><a class='menu_yli02' href='Yachts_Layout.aspx?id={guid}' >Layout & deck plan</a></li>");
                topMenuHtml.Append($"<li><a class='menu_yli03' href='Yachts_Specification.aspx?id={guid}' >Specification</a></li>");
                if (!String.IsNullOrEmpty(reader["VideoUrl"].ToString()))
                {
                    topMenuHtml.Append($"<li style='margin-right: 5px'><a class='menu_yli04' href='Yachts_Video.aspx?id={guid}' >Video</a></li>");
                }

                //渲染畫面     
                LittleTitle.Text = yachtModel;//右上小連結        
                ModelName.Text = yachtModel;
                Menu.Text = topMenuHtml.ToString();//渲染型號內容上方分類連結列表

            }
            connection.Close();
        }

    }
}