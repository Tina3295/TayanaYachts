using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class News_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.SelectedDate = Calendar1.TodaysDate;
                LoadYears();
                ShowNewsTitle();
                if (NewsTitleRadioBtnList.Items.Count > 0)
                {
                    NewsTitlePanel.Visible = true;
                }
                else
                {
                    NewsTitlePanel.Visible = false;
                }
            }



            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl);
        }

        protected void AddTitle_Click(object sender, EventArgs e)
        {
            if (AddNewsTitle.Text != "")
            {
                //隨機識別碼+秒
                string guid = Guid.NewGuid().ToString().Trim() + DateTime.Now.ToString("ff");
                string SelDate = Calendar1.SelectedDate.ToString("yyyy-M-dd");

                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "INSERT INTO News (ReleaseDate, NewsTitle, Guid, IsTop) VALUES (@ReleaseDate, @NewsTitle, @Guid, @IsTop)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ReleaseDate", SelDate);
                command.Parameters.AddWithValue("@NewsTitle", AddNewsTitle.Text);
                command.Parameters.AddWithValue("@Guid", guid);
                command.Parameters.AddWithValue("@IsTop", AddIsTop.Checked.ToString());
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                //渲染畫面
                NewsTitleRadioBtnList.Items.Clear();
                ShowNewsTitle();
                NewsTitlePanel.Visible = true;
                NewsDetailPanel.Visible = false;
                NewsAttachmentPanel.Visible = false;

                AddNewsTitle.Text = "";
                AddTitleTip.Text = "";
            }
            else
            {
                AddTitleTip.Text = "Please enter Title";
            }
        }



        private void ShowNewsTitle()
        {
            NewsTitleRadioBtnList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM News WHERE ReleaseDate = @ReleaseDate ORDER BY IsTop DESC,NewID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ReleaseDate", Calendar1.SelectedDate.ToString("yyyy-M-dd"));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                bool istop = Convert.ToBoolean(reader["isTop"]);
                if (istop)
                {
                    listItem.Text = reader["NewsTitle"].ToString() + "(Top)";
                }
                else
                {
                    listItem.Text = reader["NewsTitle"].ToString();
                }

                listItem.Value = reader["NewID"].ToString();
                NewsTitleRadioBtnList.Items.Add(listItem);
            }
            connection.Close();

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            NewsTitleRadioBtnList.Items.Clear();
            ShowNewsTitle();
            if (NewsTitleRadioBtnList.Items.Count > 0)
            {
                NewsTitlePanel.Visible = true;
            }
            else
            {
                NewsTitlePanel.Visible = false;
            }


            NewsAttachmentPanel.Visible = false;
            NewsDetailPanel.Visible = false;

            TipClear();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            //取得新聞日期
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = $"SELECT ReleaseDate FROM News";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DateTime newsTime = DateTime.Parse(reader["ReleaseDate"].ToString());
                //修改有新聞的日期外觀
                if (e.Day.Date.Date == newsTime && e.Day.Date.Date != Calendar1.SelectedDate)
                {
                    //e.Cell.BorderWidth = Unit.Pixel(1); //外框線粗細
                    //e.Cell.BorderColor = Color.BlueViolet; //外框線顏色
                    e.Cell.Font.Underline = true; //有無下地線
                    e.Cell.Font.Bold = true; //是否為粗體
                    e.Cell.ForeColor = Color.DodgerBlue; //外觀色彩
                }
            }
            connection.Close();
        }




        private void LoadYears()
        {
            for (int i = 1960; i <= 2100; i++)
            {
                Years.Items.Add(i.ToString());
                CanlendarYear.Items.Add(i.ToString());
                //Years.Items.FindByValue(System.DateTime.Now.Year.ToString());
            }
            string nowYear = System.DateTime.Now.Year.ToString();
            CanlendarYear.Items.FindByValue(nowYear).Selected = true;
            //cb_Years.Items.Insert(0, new ListEditItem(strYear, Convert.ToString(0)));
            //cb_Years.SelectedIndex = 0;

            for (int i = 1; i <= 12; i++)
            {
                Months.Items.Add(i.ToString());
            }
            FillDays();
        }
        public void FillDays()
        {
            Days.Items.Clear();

            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(Years.SelectedValue), Convert.ToInt32(Months.SelectedValue));
            for (int i = 1; i <= noofdays; i++)
            {
                Days.Items.Add(i.ToString());
            }
            //ddlDay.Items.FindByValue(System.DateTime.Now.Day.ToString()).Selected = true;// Set current date as selected
        }

        //protected void Years_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillDays();
        //}
        protected void Months_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDays();
        }

        protected void CanlendarYear_SelectedIndexChanged(object sender, EventArgs e)
        {//讓日曆直接跳到所選年份    
            int year = Convert.ToInt32(CanlendarYear.SelectedItem.Text);
            int month = Convert.ToInt32(Calendar1.SelectedDate.Month);
            int day = Convert.ToInt32(Calendar1.SelectedDate.Day);


            Calendar1.VisibleDate = new DateTime(year, month, day);
            Calendar1.SelectedDate = new DateTime(year, month, day);


            NewsDetailPanel.Visible = false;
            NewsAttachmentPanel.Visible = false;
            NewsTitleRadioBtnList.Items.Clear();
            ShowNewsTitle();
            if (NewsTitleRadioBtnList.Items.Count > 0)
            {
                NewsTitlePanel.Visible = true;
            }
            else
            {
                NewsTitlePanel.Visible = false;
            }

            TipClear();
        }

        protected void NewsTitleRadioBtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //顯示新聞詳細資訊
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

            string sql = "SELECT * FROM News where NewID = @NewID";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Years.SelectedValue = Convert.ToDateTime(reader["ReleaseDate"]).Year.ToString();
                Months.SelectedValue = Convert.ToDateTime(reader["ReleaseDate"]).Month.ToString();
                Days.SelectedValue = Convert.ToDateTime(reader["ReleaseDate"]).Day.ToString();
                Thumbnailimg.ImageUrl = "/images/" + reader["Thumbnail"].ToString();
                NewsTitle.Text = reader["NewsTitle"].ToString();
                IsTop.Checked = Convert.ToBoolean(reader["IsTop"]);
                Summary.Text = reader["Summary"].ToString();
                CKEditorControl.Text = Server.HtmlDecode(reader["NewsContentHtml"].ToString());


                InitDate.Text = reader["InitDate"].ToString();
            }
            connection.Close();

            NewsImageData();
            ShowAttachment();

            NewsDetailPanel.Visible = true;
            NewsAttachmentPanel.Visible = true;

            TipClear();
        }

        protected void ThumbnailUploadBtn_Click(object sender, EventArgs e)
        {
            if (ThumbnailUpload.HasFile)
            {
                //設定存檔路徑，記得加最後"\"
                string SavePath = Server.MapPath("~/images/");
                string FinalFileName;
                string FileName = ThumbnailUpload.FileName;
                //檢查副檔名
                int x = FileName.LastIndexOf('.');
                string a = FileName.Remove(0, x);
                if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                {
                    ThumbnailUploadTip.Text = "type can't be accepted!";
                }
                else
                {
                    //刪舊圖檔
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql2 = "SELECT Thumbnail FROM News WHERE NewID= @NewID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
                    connection.Open();
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                        string delFileName = reader2["Thumbnail"].ToString();
                        //有舊圖才執行刪除
                        if (!String.IsNullOrEmpty(delFileName))
                        {
                            File.Delete(SavePath + delFileName);
                        }
                    }
                    connection.Close();



                    //檢查檔名衝突
                    string PathToCheck = SavePath + FileName;
                    string TempFileName = "";

                    if (File.Exists(PathToCheck))
                    {
                        int count = 2;
                        while (File.Exists(PathToCheck))
                        {
                            string[] FileNameSpilt = FileName.Split('.');
                            TempFileName = FileNameSpilt[0] + "_" + count.ToString() + a;
                            PathToCheck = SavePath + TempFileName;
                            count++;
                        }
                    }

                    ThumbnailUpload.SaveAs(PathToCheck);

                    if (TempFileName != "")
                    {
                        FinalFileName = TempFileName;
                    }
                    else
                    {
                        FinalFileName = FileName;
                    }




                    string sql = "update News set Thumbnail=@Thumbnail where NewID = @NewID";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Thumbnail", FinalFileName);
                    command.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    Thumbnailimg.ImageUrl = "/images/" + FinalFileName;
                    ThumbnailUploadTip.Text = "";
                }
            }
            else
            {
                ThumbnailUploadTip.Text = "There's no file to upload!";
            }
        }









        //以下多圖
        private void NewsImageData()
        {//顯示出多圖
            NewsImageRadioButtonList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT NewsImage FROM News WHERE NewID= @NewID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string newsimage = reader["NewsImage"].ToString();

                if (!String.IsNullOrEmpty(newsimage))
                {
                    string[] newsimages = newsimage.Split(',');

                    foreach (var item in newsimages)
                    {
                        ListItem listItem = new ListItem($"<img src='/images/{item}' style='margin:10px' width='230px'/>", item);
                        NewsImageRadioButtonList.Items.Add(listItem);
                    }

                }
            }
            connection.Close();
        }

        protected void DelNewsImage_Click(object sender, EventArgs e)
        {//刪除圖片

            if (NewsImageRadioButtonList.SelectedValue.ToString() != "")
            {

                string delimage = NewsImageRadioButtonList.SelectedValue;
                string selecttitle = NewsTitleRadioBtnList.SelectedValue;

                string savenewsimages = "";

                //實際刪除圖檔
                File.Delete(Server.MapPath("~/images/") + delimage);

                //更新資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT NewsImage FROM News WHERE NewID= @NewID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@NewID", selecttitle);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string newsimage = reader["NewsImage"].ToString();

                    try
                    {
                        string[] newsimages = newsimage.Split(',');

                        newsimages = Array.FindAll(newsimages, val => val != delimage).ToArray();



                        foreach (var item in newsimages)
                        {
                            savenewsimages += item + ",";
                        }
                    }
                    catch
                    {
                        savenewsimages = "";
                    }
                }

                connection.Close();




                if (savenewsimages != "")
                {
                    string sql2 = "Update News set NewsImage =@NewsImage WHERE NewID= @NewID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@NewID", selecttitle);
                    command2.Parameters.AddWithValue("@NewsImage", savenewsimages.TrimEnd(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string sql2 = "Update News set NewsImage =NULL WHERE NewID= @NewID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@NewID", selecttitle);

                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }


                NewsImageData();
                DelNewsImage.Visible = false;
            }

        }



        protected void NewsImageBtn_Click(object sender, EventArgs e)
        {//上傳多圖
            string newsimage = "";
            NewsImageTip.Text = "";

            if (NewsImageUpload.HasFile)
            {
                int fileSize = NewsImageUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1000 * 100)
                {
                    //撈出原本的圖片csv格式
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT NewsImage FROM News WHERE NewID= @NewID";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        newsimage = reader["NewsImage"].ToString();
                    }
                    connection.Close();



                    //設定存檔路徑
                    string SavePath = Server.MapPath("~/images/");
                    string FinalFileName;


                    //處理每個上傳檔案
                    foreach (HttpPostedFile postedFile in NewsImageUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);

                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                        {
                            NewsImageTip.Text += postedFile.FileName + " 非圖檔<br/>";
                        }
                        else
                        {
                            //檢查檔名衝突
                            string PathToCheck = SavePath + FileName;
                            string TempFileName = "";

                            if (File.Exists(PathToCheck))
                            {
                                int count = 2;
                                while (File.Exists(PathToCheck))
                                {
                                    string[] FileNameSpilt = FileName.Split('.');
                                    TempFileName = FileNameSpilt[0] + "_" + count.ToString() + a;
                                    PathToCheck = SavePath + TempFileName;
                                    count++;
                                }
                            }

                            postedFile.SaveAs(PathToCheck);

                            if (TempFileName != "")
                            {
                                FinalFileName = TempFileName;
                            }
                            else
                            {
                                FinalFileName = FileName;
                            }

                            //把圖片以","隔開加進格式
                            newsimage += "," + FinalFileName;
                        }
                    }


                    //寫入資料庫
                    string sql2 = "Update News set NewsImage =@NewsImage WHERE NewID= @NewID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
                    command2.Parameters.AddWithValue("@NewsImage", newsimage.TrimStart(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();

                    NewsImageData();
                }
                else
                {
                    NewsImageTip.Text = "The maximum upload size is 100MB!";
                }
            }
            else
            {
                NewsImageTip.Text = "There's no file";
            }
        }



        //更新新聞資訊
        protected void UpdateNews_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "update News set ReleaseDate=@ReleaseDate,NewsTitle=@NewsTitle,Summary=@Summary,IsTop=@IsTop,NewsContentHtml=@NewsContentHtml where NewID = @NewID";

            SqlCommand command = new SqlCommand(sql, connection);

            DateTime releasedate = new DateTime(Convert.ToInt32(Years.SelectedItem.Text), Convert.ToInt32(Months.SelectedItem.Text), Convert.ToInt32(Days.SelectedItem.Text));
            command.Parameters.AddWithValue("@ReleaseDate", releasedate);
            command.Parameters.AddWithValue("@NewsTitle", NewsTitle.Text.ToString());
            command.Parameters.AddWithValue("@Summary", Summary.Text.ToString());
            command.Parameters.AddWithValue("@IsTop", IsTop.Checked);
            command.Parameters.AddWithValue("@NewsContentHtml", HttpUtility.HtmlEncode(CKEditorControl.Text).ToString());

            command.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();



            //讓日曆跳到改的日期
            Calendar1.SelectedDates.Clear();
            Calendar1.SelectedDate = releasedate;
            Calendar1.VisibleDate = releasedate;
            ShowNewsTitle();
            //DealerRadioButtonList.SelectedItem.Text = Area.Text.ToString();
        }

        //刪除整則新聞
        protected void DeleteNews_Click(object sender, EventArgs e)
        {
            //刪附件(實際檔案、
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AttachmentName FROM NewsAttachment WHERE NewID = @NewID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string delFileName = reader["AttachmentName"].ToString();
                //有舊圖才執行刪除
                if (!String.IsNullOrEmpty(delFileName))
                {
                    File.Delete(Server.MapPath("~/attachment/") + delFileName);
                }
            }
            connection.Close();


            //資料庫資料)
            SqlCommand command2 = new SqlCommand($"DELETE  FROM NewsAttachment WHERE NewID = @NewID", connection);
            command2.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            command2.ExecuteNonQuery();
            connection.Close();


            //刪實際縮圖
            string sql3 = "SELECT Thumbnail FROM News WHERE NewID = @NewID";
            SqlCommand command3 = new SqlCommand(sql3, connection);
            command3.Parameters.AddWithValue("NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader3 = command3.ExecuteReader();
            if (reader3.Read())
            {
                string delFileName = reader3["Thumbnail"].ToString();
                //有舊圖才執行刪除
                if (!String.IsNullOrEmpty(delFileName))
                {
                    File.Delete(Server.MapPath("~/images/") + delFileName);
                }
            }
            connection.Close();





            //刪多圖
            string sql4 = "SELECT NewsImage FROM News WHERE NewID = @NewID";
            SqlCommand command4 = new SqlCommand(sql4, connection);
            command4.Parameters.AddWithValue("NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader4 = command4.ExecuteReader();
            if (reader4.Read())
            {
                string delFileName = reader4["NewsImage"].ToString();
                //有舊圖才執行刪除
                if (!String.IsNullOrEmpty(delFileName))
                {
                    if (delFileName.Contains(","))
                    {
                        string[] imgnames = delFileName.Split(',');
                        foreach (string imgname in imgnames)
                        {
                            File.Delete(Server.MapPath("~/images/") + imgname);
                        }
                    }
                    else
                    {
                        File.Delete(Server.MapPath("~/images/") + delFileName);
                    }
                }
            }
            connection.Close();







            SqlCommand command5 = new SqlCommand($"DELETE  FROM News WHERE (NewID = @NewID)", connection);
            command5.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            command5.ExecuteNonQuery();
            connection.Close();


            ShowNewsTitle();
            if (NewsTitleRadioBtnList.Items.Count > 0)
            {
                NewsTitlePanel.Visible = true;
            }
            else
            {
                NewsTitlePanel.Visible = false;
            }
            NewsDetailPanel.Visible = false;
            NewsAttachmentPanel.Visible = false;
        }



        //以下附件相關
        protected void AttachmentBtn_Click(object sender, EventArgs e)
        {
            if (AttachmentFileUpload.HasFile)
            {
                string SavePath = Server.MapPath("~/attachment/");
                string FinalFileName;

                int fileSize = AttachmentFileUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1000 * 100)
                {

                    foreach (HttpPostedFile postedFile in AttachmentFileUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);
                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg" && a != ".doc" && a != ".docx"
                            && a != ".xls" && a != ".xlsx" && a != ".ppt" && a != ".pptx" && a != ".pdf" && a != ".bmp"
                            && a != ".txt" && a != ".zip" && a != ".rar" && a != ".bmp")
                        {
                            AttachmentTip.Text = postedFile.FileName + "type can't be accepted";
                        }
                        else
                        {
                            //檢查檔名衝突
                            string PathToCheck = SavePath + FileName;
                            string TempFileName = "";

                            if (File.Exists(PathToCheck))
                            {
                                int count = 2;
                                while (File.Exists(PathToCheck))
                                {
                                    string[] FileNameSpilt = FileName.Split('.');
                                    TempFileName = FileNameSpilt[0] + "_" + count.ToString() + a;
                                    PathToCheck = SavePath + TempFileName;
                                    count++;
                                }
                            }

                            postedFile.SaveAs(PathToCheck);

                            if (TempFileName != "")
                            {
                                FinalFileName = TempFileName;
                            }
                            else
                            {
                                FinalFileName = FileName;
                            }

                            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                            string sql = "INSERT INTO NewsAttachment(AttachmentName,NewID) VALUES(@AttachmentName,@NewID)";
                            SqlCommand command = new SqlCommand(sql, connection);

                            command.Parameters.AddWithValue("@AttachmentName", FinalFileName);
                            command.Parameters.AddWithValue("@NewID", NewsTitleRadioBtnList.SelectedValue);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();




                        }
                    }
                    ShowAttachment();
                    AttachmentTip.Text = "";

                }
                else
                {
                    AttachmentTip.Text = "The maximum upload size is 100MB!";
                }

            }
            else
            {
                AttachmentTip.Text = "There's no file";
            }
        }

        protected void ShowAttachment()
        {
            AttachmentRadioButtonList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AttachmentID,AttachmentName FROM NewsAttachment WHERE NewID = @NewID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("NewID", NewsTitleRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem($"<a href='attachment/{reader["AttachmentName"]}' download>{reader["AttachmentName"]}</a>", reader["AttachmentID"].ToString());
                AttachmentRadioButtonList.Items.Add(listItem);


                //ListItem listItem = new ListItem();
                //bool istop = Convert.ToBoolean(reader["isTop"]);
                //if (istop)
                //{
                //    listItem.Text = reader["NewsTitle"].ToString() + "(Top)";
                //}
                //else
                //{
                //    listItem.Text = reader["NewsTitle"].ToString();
                //}

                //listItem.Value = reader["AttachmentID"].ToString();
                //NewsTitleRadioBtnList.Items.Add(listItem);
            }
            connection.Close();
        }




        protected void DelAttachmentBtn_Click(object sender, EventArgs e)
        {
            //刪除實際檔案
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AttachmentName FROM NewsAttachment WHERE AttachmentID = @AttachmentID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("AttachmentID", AttachmentRadioButtonList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                File.Delete(Server.MapPath("~/attachment/") + reader["AttachmentName"].ToString());
            }
            connection.Close();


            //刪除資料庫資料
            SqlCommand command2 = new SqlCommand($"DELETE  FROM NewsAttachment WHERE (AttachmentID = @AttachmentID)", connection);
            command2.Parameters.AddWithValue("@AttachmentID", AttachmentRadioButtonList.SelectedValue);
            connection.Open();
            command2.ExecuteNonQuery();
            connection.Close();




            ShowAttachment();
            DelAttachmentBtn.Visible = false;
        }

        protected void AttachmentRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DelAttachmentBtn.Visible = true;
        }

        protected void NewsImageRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DelNewsImage.Visible = true;
        }





        protected void TipClear()
        {
            AddTitleTip.Text = "";
            AttachmentTip.Text = "";
            ThumbnailUploadTip.Text = "";
            NewsImageTip.Text = "";
        }
    }
}
