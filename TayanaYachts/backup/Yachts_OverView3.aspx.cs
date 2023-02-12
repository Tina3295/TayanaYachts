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
    public partial class Yachts_OverView3 : System.Web.UI.Page
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
            if(!IsPostBack)
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
                //如果無網址傳值就用第一筆遊艇型號的 GUID
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
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[1] { new DataColumn("ImageUrl") });

            string guid = Session["guid"].ToString();
            //依 GUID 取得遊艇輪播圖片資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT BannerImg FROM Yachts WHERE Guid = @Guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string [] bannerimgs = reader["BannerImg"].ToString().Split(',');
                
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
            string urlPathStr = System.IO.Path.GetFileName(Request.PhysicalPath);
            //取得遊艇型號資料
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
                //依是否為新建或新設計加入標註
                if (isNewDesign.Equals("True"))
                {
                    isNew = "(New Design)";
                }
                else if (isNewBuilding.Equals("True"))
                {
                    isNew = "(New Building)";
                }
                leftMenuHtml.Append($"<li><a href='{urlPathStr}?id={guid}'>{yachtModel} {isNew}</a></li>");
            }
            connection.Close();

            ModelMenu.Text = leftMenuHtml.ToString();
        }



        private void LoadTopMenu()
        {
            string guid = Session["guid"].ToString();
            string id = "";
            //依 GUID 取得遊艇資料
            //List<RowData> saveRowList = new List<RowData>();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts WHERE guid = @guid";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@guid", guid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            StringBuilder topMenuHtmlStr = new StringBuilder();
            
            if (reader.Read())
            {
                id= reader["YachtID"].ToString();
                string yachtModel = reader["YachtModel"].ToString();
                OverviewContent.Text = HttpUtility.HtmlDecode(reader["OverviewContent"].ToString());
                OverviewDimensions.Text = HttpUtility.HtmlDecode(reader["OverviewDimensionsHtml"].ToString());

                //加入渲染型號內容上方分類連結列表
                topMenuHtmlStr.Append($"<li><a class='menu_yli01' href='Yachts_OverView.aspx?id={guid}' >OverView</a></li>");
                topMenuHtmlStr.Append($"<li><a class='menu_yli02' href='Yachts_Layout.aspx?id={guid}' >Layout & deck plan</a></li>");
                topMenuHtmlStr.Append($"<li><a class='menu_yli03' href='Yachts_Specification.aspx?id={guid}' >Specification</a></li>");
                //加入渲染型號內容上方分類連結列表 Video 分頁標籤，有存影片連結網址才渲染
                //saveRowList = JsonConvert.DeserializeObject<List<RowData>>(loadJson);
                if (!String.IsNullOrEmpty(reader["VideoUrl"].ToString()))
                {
                    topMenuHtmlStr.Append($"<li style='margin-right: 5px'><a class='menu_yli04' href='Yachts_Video.aspx?id={guid}' >Video</a></li>");
                }

                //渲染畫面     
                LittleTitle.Text = yachtModel;//右上小連結        
                ModelName.Text = yachtModel;
                Menu.Text= topMenuHtmlStr.ToString();//渲染型號內容上方分類連結列表

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
                Literal1.Text += "<li><a id='ctl00_ContentPlaceHolder1_RepFile_ctl01_HyperLink1' href='attachment/" + reader2["AttachmentName"].ToString() + " target='_blank'>" + reader2["AttachmentName"].ToString() + "</a></li>";
            }
            connection.Close();
        }
    }
}