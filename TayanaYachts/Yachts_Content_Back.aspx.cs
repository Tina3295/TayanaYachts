using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Yachts_Content_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ModelList();
            }
            



            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl);
            FileBrowser fileBrowser2 = new FileBrowser();
            fileBrowser2.BasePath = "/ckfinder";
            fileBrowser2.SetupCKEditor(CKEditorControl2);
            FileBrowser fileBrowser3 = new FileBrowser();
            fileBrowser2.BasePath = "/ckfinder";
            fileBrowser2.SetupCKEditor(OverviewCKEditorControl);
        }




        protected void ModelList()
        {
            //Model下拉選單
            ModelDropDownList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT YachtID,YachtModel FROM Yachts order by YachtModel";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                listItem.Text = reader["YachtModel"].ToString();
                listItem.Value = reader["YachtID"].ToString();
                ModelDropDownList.Items.Add(listItem);
            }
            connection.Close();

            ModelDropDownList.Items.Insert(0, new ListItem("--Choose Model--", "0"));


        }

        protected void ModelDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModelDropDownList.SelectedValue != "0")
            {
                ShowTopData();
                ShowAttachment();
                LayoutData();
                ShowSpecification();
                DownloadsPanel.Visible = true;
                ContentPanel.Visible = true;
                DownPanel.Visible = true;
                //ShowDimensionsImg();
                //TipHide();
            }
            else
            {
                DownloadsPanel.Visible = false;
                ContentPanel.Visible = false;
                DownPanel.Visible = false;
            }
        }



        protected void ShowTopData()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Yachts where YachtID=@YachtID";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                bool isnewdesign = Convert.ToBoolean(reader["NewDesign"].ToString());
                bool isnewbuilding = Convert.ToBoolean(reader["NewBuilding"].ToString());

                if(isnewdesign && isnewbuilding)
                {
                    Newbuilding.Visible = true;
                    Newdesign.Visible = true;
                }
                else if(isnewdesign)
                {
                    Newbuilding.Visible = false;
                    Newdesign.Visible = true;
                }
                else if(isnewbuilding)
                {
                    Newbuilding.Visible = true;
                    Newdesign.Visible = false;
                }
                else
                {
                    Newbuilding.Visible = false;
                    Newdesign.Visible = false;
                }

                OverviewCKEditorControl.Text = Server.HtmlDecode(reader["OverviewContent"].ToString());

                CKEditorControl.Text = Server.HtmlDecode(reader["OverviewDimensionsHtml"].ToString());
            }
            connection.Close();

            //TipHide();
        }




        protected void OverviewContentBtn_Click(object sender, EventArgs e)
        {
            //更新OverviewContent內容
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "update Yachts set OverviewContent=@OverviewContent,OverviewDimensionsHtml=@OverviewDimensionsHtml where YachtID = @YachtID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
            command.Parameters.AddWithValue("@OverviewDimensionsHtml", HttpUtility.HtmlEncode(CKEditorControl.Text).ToString());
            command.Parameters.AddWithValue("@OverviewContent", HttpUtility.HtmlEncode(OverviewCKEditorControl.Text).ToString());


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //OverviewContentBtnTip.Text = "*Upload Success! - " + DateTime.Now.ToString("G");
        }







        //protected void ShowDimensionsImg()
        //{
        //    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
        //    string sql = "SELECT OverviewDimensionsImg FROM Yachts where YachtID=@YachtID";

        //    SqlCommand command = new SqlCommand(sql, connection);
        //    command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        DimensionsImg.ImageUrl = "/images/" + reader["OverviewDimensionsImg"].ToString();
        //        DimensionsImgName.Text= reader["OverviewDimensionsImg"].ToString();
        //    }
        //    connection.Close();
        //}





        //-------------------------------------------------video/附件-----------------------------------------------------------------
        protected void ShowAttachment()
        {
            //videourl
            VideoTextBox.Text = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT VideoUrl FROM Yachts WHERE YachtID = @YachtID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("YachtID", ModelDropDownList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if(!string.IsNullOrWhiteSpace(reader["VideoUrl"].ToString()))
                {
                    VideoTextBox.Text = reader["VideoUrl"].ToString();
                }
            }
            connection.Close();




            AttachmentRadioButtonList.Items.Clear();

            
            string sql2 = "SELECT AttachmentID,AttachmentName FROM YachtsAttachment WHERE YachtID = @YachtID";
            SqlCommand command2 = new SqlCommand(sql2, connection);
            command2.Parameters.AddWithValue("YachtID", ModelDropDownList.SelectedValue);
            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                ListItem listItem = new ListItem($"<a href='attachment/{reader2["AttachmentName"]}' download>{reader2["AttachmentName"]}</a>", reader2["AttachmentID"].ToString());
                AttachmentRadioButtonList.Items.Add(listItem);
            }
            connection.Close();
        }




        protected void AttachmentBtn_Click(object sender, EventArgs e)
        {
            //video
            if (string.IsNullOrWhiteSpace(VideoTextBox.Text))
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "update Yachts set VideoUrl=NULL where (YachtID=@YachtID)";
                SqlCommand command = new SqlCommand(sql, connection);

                
                command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "update Yachts set VideoUrl=@VideoUrl where (YachtID=@YachtID)";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@VideoUrl", VideoTextBox.Text);
                command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }




            if (AttachmentFileUpload.HasFile)
            {
                string SavePath = Server.MapPath("~/attachment/");
                string FinalFileName;

                int fileSize = AttachmentFileUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1024 * 50)
                {

                    foreach (HttpPostedFile postedFile in AttachmentFileUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);
                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg" && a != ".doc" && a != ".docx"
                            && a != ".xls" && a != ".xlsx" && a != ".ppt" && a != ".pptx" && a != ".pdf" && a != ".bmp"
                            && a != ".txt" && a != ".zip" && a != ".rar")
                        {
                            AttachmentTip.Text = postedFile.FileName + " type can't be accepted";
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
                            string sql = "INSERT INTO YachtsAttachment(AttachmentName,YachtID) VALUES(@AttachmentName,@YachtID)";
                            SqlCommand command = new SqlCommand(sql, connection);

                            command.Parameters.AddWithValue("@AttachmentName", FinalFileName);
                            command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);

                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();




                        }
                    }
                    ShowAttachment();
                    //AttachmentTip.Text = "";

                }
                else
                {
                    AttachmentTip.Text = "The maximum upload size is 50MB!";
                }

            }
            else
            {
                //AttachmentTip.Text = "There's no file";
            }
        }


        protected void DelAttachmentBtn_Click(object sender, EventArgs e)
        {
            //刪除實際檔案
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AttachmentName FROM YachtsAttachment WHERE AttachmentID = @AttachmentID";
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
            SqlCommand command2 = new SqlCommand($"DELETE  FROM YachtsAttachment WHERE (AttachmentID = @AttachmentID)", connection);
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




        protected void TipHide()
        {
            OverviewContentBtnTip.Visible = false;
            SpecificationTip.Visible = false;
            
        }


        //-----------------------------------------以下多圖-----------------------------------------------------
        private void LayoutData()
        {//顯示出多圖
            LayoutRadioButtonList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT LayoutImg FROM Yachts WHERE YachtID= @YachtID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string image = reader["LayoutImg"].ToString();

                if (!String.IsNullOrEmpty(image))
                {
                    string[] images = image.Split(',');

                    foreach (var item in images)
                    {
                        ListItem listItem = new ListItem($"<img src='/images/{item}' style='margin:10px' width='170px'/>", item);
                        LayoutRadioButtonList.Items.Add(listItem);
                    }

                }
            }
            connection.Close();
        }


        protected void LayoutUploadBtn_Click(object sender, EventArgs e)
        {
            //上傳多圖
            string layoutimage = "";
            LayoutUploadTip.Text = "";

            if (LauoutFileUpload.HasFile)
            {
                int fileSize = LauoutFileUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1000 * 100)
                {
                    //撈出原本的圖片csv格式
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT LayoutImg FROM Yachts WHERE YachtID= @YachtID";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        layoutimage = reader["LayoutImg"].ToString();
                    }
                    connection.Close();



                    //設定存檔路徑
                    string SavePath = Server.MapPath("~/images/");
                    string FinalFileName;


                    //處理每個上傳檔案
                    foreach (HttpPostedFile postedFile in LauoutFileUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);

                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                        {
                            LayoutUploadTip.Text += postedFile.FileName + " 非圖檔<br/>";
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
                            layoutimage += "," + FinalFileName;
                        }
                    }


                    //寫入資料庫
                    string sql2 = "Update Yachts set LayoutImg =@LayoutImg WHERE YachtID= @YachtID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
                    command2.Parameters.AddWithValue("@LayoutImg", layoutimage.TrimStart(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();

                    LayoutData();
                }
                else
                {
                    LayoutUploadTip.Text = "The maximum upload size is 100MB!";
                }
            }
            else
            {
                LayoutUploadTip.Text = "There's no file";
            }
        }

        protected void LayoutRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DelLayoutBtn.Visible = true;
        }

        protected void DelLayoutBtn_Click(object sender, EventArgs e)
        {
            if (LayoutRadioButtonList.SelectedValue.ToString() != "")
            {

                string delimage = LayoutRadioButtonList.SelectedValue;
                string selectmodel = ModelDropDownList.SelectedValue;

                string saveimages = "";

                //實際刪除圖檔
                File.Delete(Server.MapPath("~/images/") + delimage);

                //更新資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT LayoutImg FROM Yachts WHERE YachtID= @YachtID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@YachtID", selectmodel);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string image = reader["LayoutImg"].ToString();

                    try
                    {
                        string[] images = image.Split(',');

                        images = Array.FindAll(images, val => val != delimage).ToArray();



                        foreach (var item in images)
                        {
                            saveimages += item + ",";
                        }
                    }
                    catch
                    {
                        saveimages = "";
                    }
                }

                connection.Close();




                if (saveimages != "")
                {
                    string sql2 = "Update Yachts set LayoutImg=@LayoutImg WHERE YachtID= @YachtID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtID", selectmodel);
                    command2.Parameters.AddWithValue("@LayoutImg", saveimages.TrimEnd(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string sql2 = "Update Yachts set LayoutImg=NULL WHERE YachtID= @YachtID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtID", selectmodel);

                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }


                LayoutData();
                DelLayoutBtn.Visible = false;
            }
        }

        protected void SpecificationContentBtn_Click(object sender, EventArgs e)
        {
            //更新Specification內容
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "update Yachts set SpecificationHtml=@SpecificationHtml where YachtID = @YachtID";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@YachtID", ModelDropDownList.SelectedValue);
            command.Parameters.AddWithValue("@SpecificationHtml", HttpUtility.HtmlEncode(CKEditorControl2.Text).ToString());


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            //SpecificationTip.Text = "*Upload Success! - " + DateTime.Now.ToString("G");
        }

        protected void ShowSpecification()
        {
            CKEditorControl2.Text = "";
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT SpecificationHtml FROM Yachts WHERE YachtID = @YachtID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("YachtID", ModelDropDownList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!string.IsNullOrWhiteSpace(reader["SpecificationHtml"].ToString()))
                {
                    CKEditorControl2.Text = reader["SpecificationHtml"].ToString();
                }
            }
            connection.Close();

        }
    }
}