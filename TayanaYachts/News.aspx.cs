using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                LoadNewsList();

            }
        }



        private void LoadNewsList()
        {
            //1.建立判斷網址是否有傳值,預設第一頁
            int page = 1; 
            if (!String.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }

            //2.設定控制項參數: 一頁幾筆資料、作用頁面完整網頁名稱
            Pagination.limit = 3;
            Pagination.targetPage = "News.aspx";

            //3.建立計算分頁資料顯示邏輯 (每一頁是從第幾筆開始到第幾筆結束)
            //計算每個分頁的第幾筆到第幾筆
            var floor = (page - 1) * Pagination.limit + 1; //每頁的第一筆
            var ceiling = page * Pagination.limit; //每頁的最末筆

            //4.算出總資料數
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT COUNT(NewID) FROM News WHERE ReleaseDate <= @nowDate";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nowDate", DateTime.Now.ToString("yyyy-MM-dd")); //只秀當天及之前的資料
            connection.Open();
            
            Pagination.totalItems = Convert.ToInt32(command.ExecuteScalar());//用 ExecuteScalar() 來算數量
            connection.Close();


            //5.渲染分頁控制項
            Pagination.showPageControls();

            //6.將原始資料表的 SQL 語法使用 CTE 暫存表改寫，並使用 ROW_NUMBER() 函式製作資料項流水號 rowindex
            // SQL 用 CTE 暫存表 + ROW_NUMBER 去生出我的流水號 rowindex 後以流水號為條件來查詢暫存表
            // 排序先用 isTop 後用 ReleaseDate 產生置頂效果
            string sql2 = $"WITH temp AS (SELECT ROW_NUMBER() OVER (ORDER BY IsTop DESC, ReleaseDate DESC) AS rowindex, * FROM News WHERE ReleaseDate <= @nowDate) SELECT * FROM temp WHERE rowindex >= {floor} AND rowindex <= {ceiling}";
            SqlCommand command2 = new SqlCommand(sql2, connection);
            command2.Parameters.AddWithValue("@nowDate", DateTime.Now.ToString("yyyy-MM-dd"));

            //10.取得每頁的新聞列表資料製作成 HTML 內容
            connection.Open();
            StringBuilder newListHtml = new StringBuilder();
            SqlDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {
                string newid = reader["NewID"].ToString();
                string releaseDate = DateTime.Parse(reader["ReleaseDate"].ToString()).ToString("yyyy/M/d");
                string newsTitle = reader["NewsTitle"].ToString();
                string summary = reader["Summary"].ToString();
                string thumbnail = reader["Thumbnail"].ToString();
                string guid = reader["Guid"].ToString();
                string IsTop = reader["IsTop"].ToString();
                string displayStr = "none";
                if (IsTop.Equals("True"))
                {
                    displayStr = "inline-block";
                }
                newListHtml.Append($"<li><div class='list01'><ul><li><div '>" +
                    $"<img src='images/new_top01.png' alt='&quot;&quot;' style='display: {displayStr};position: absolute;z-index: 5;'/>" +
                    $"<div style='border: 1px solid #CCCCCC;'><p>" +
                    $"<img id='thumbnail_Image{newid}' src='/Images/{thumbnail}' style='z-index: 1;'  />" +
                    $"</p></div></li><li><span>{releaseDate}</span><br />" +
                    $"<a href='News_Detail.aspx?id={guid}'>{newsTitle}</a></li><br />" +
                    $"<li>{summary} </li></ul></div></li>");
            }
            connection.Close();

            //渲染新聞列表
            NewsList.Text = newListHtml.ToString();
        }


    }
}