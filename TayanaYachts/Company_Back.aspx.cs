using CKEditor.NET;
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
    public partial class Company_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Userinformation.companyadmin == false)
            {
                AdminOnly.Visible = false;

                Tip.Text = "You don't have permission to browse this page!";
            }
            if (!IsPostBack)
            {
                CkfinderSetPath();
                ShowTopContent();
                ShowVerticalImg();
                ShowHorizontalImg();
                Master.Page.Title = "Company";
            }
            
        }


        private void CkfinderSetPath()
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKEditorControl);
        }


        private void ShowTopContent()
        {
            //取得 上半部 資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT AboutUsHTML,CertificateContent FROM Company WHERE Id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                CKEditorControl.Text = HttpUtility.HtmlDecode(reader["AboutUsHTML"].ToString());
                CertificateTextBox.Text = reader["CertificateContent"].ToString();
            }
            connection.Close();
        }


        private void ShowVerticalImg()
        {//顯示直圖
            VerticalImgRadioButtonList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateVerticalImg FROM Company WHERE Id=1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string image = reader["CertificateVerticalImg"].ToString();

                if (!String.IsNullOrEmpty(image))
                {
                    string[] images = image.Split(',');

                    foreach (var item in images)
                    {
                        ListItem listItem = new ListItem($"<img src='/images/{item}' style='margin:10px' width='100px'/>", item);
                        VerticalImgRadioButtonList.Items.Add(listItem);
                    }

                }
            }
            connection.Close();
        }


        private void ShowHorizontalImg()
        {//顯示橫圖
            HorizontalImgRadioButtonList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CertificateHorizontalImg FROM Company WHERE Id=1";
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string image = reader["CertificateHorizontalImg"].ToString();

                if (!String.IsNullOrEmpty(image))
                {
                    string[] images = image.Split(',');

                    foreach (var item in images)
                    {
                        ListItem listItem = new ListItem($"<img src='/images/{item}' style='margin:10px' height='110px'/>", item);
                        HorizontalImgRadioButtonList.Items.Add(listItem);
                    }

                }
            }
            connection.Close();
        }


        protected void AboutUsBtn_Click(object sender, EventArgs e)
        {
            //更新 About Us 頁面資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "UPDATE Company SET AboutUsHTML = @AboutUsHTML WHERE id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@AboutUsHTML", HttpUtility.HtmlEncode(CKEditorControl.Text));
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            AboutUsTip.Text = "*Upload Success! - " + DateTime.Now.ToString("G");
        }

        protected void CertificateBtn_Click(object sender, EventArgs e)
        {
            //更新 Certificate 頁面資料
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "UPDATE Company SET CertificateContent = @CertificateContent WHERE id = 1";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CertificateContent", CertificateTextBox.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            CertificateTip.Text = " * Upload Success! - <br/>" + DateTime.Now.ToString("G");
        }


        //---------------------------------------------------------------------------------上傳圖片----------------
        protected void VerticalImgButton_Click(object sender, EventArgs e)
        {
            //上傳直圖
            string certificateimage = "";
            VerticalImgTip.Text = "";

            if (VerticalImgFileUpload.HasFile)
            {
                int fileSize = VerticalImgFileUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1000 * 100)
                {
                    //撈出原本的圖片csv格式
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT CertificateVerticalImg FROM Company WHERE ID=1";
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        certificateimage = reader["CertificateVerticalImg"].ToString();
                    }
                    connection.Close();



                    //設定存檔路徑
                    string SavePath = Server.MapPath("~/images/");
                    string FinalFileName;


                    //處理每個上傳檔案
                    foreach (HttpPostedFile postedFile in VerticalImgFileUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);

                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                        {
                            VerticalImgTip.Text += postedFile.FileName + " 非圖檔<br/>";
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

                            //把圖片以","隔開加進格式,寫入資料庫時需去頭
                            certificateimage += "," + FinalFileName;
                        }
                    }


                    //寫入資料庫
                    string sql2 = "Update Company set CertificateVerticalImg=@CertificateVerticalImg WHERE ID=1";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@CertificateVerticalImg", certificateimage.TrimStart(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();

                    ShowVerticalImg();
                }
                else
                {
                    VerticalImgTip.Text = "The maximum upload size is 100MB!";
                }
            }
            else
            {
                VerticalImgTip.Text = "There's no file";
            }

        }

        protected void HorizontalImgBtn_Click(object sender, EventArgs e)
        {
            //上傳橫圖
            string certificateimage = "";
            HorizontalImgTip.Text = "";

            if (HorizontalImgFileUpload.HasFile)
            {
                int fileSize = HorizontalImgFileUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1000 * 100)
                {
                    //撈出原本的圖片csv格式
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT CertificateHorizontalImg FROM Company WHERE ID=1";
                    SqlCommand command = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        certificateimage = reader["CertificateHorizontalImg"].ToString();
                    }
                    connection.Close();



                    //設定存檔路徑
                    string SavePath = Server.MapPath("~/images/");
                    string FinalFileName;


                    //處理每個上傳檔案
                    foreach (HttpPostedFile postedFile in HorizontalImgFileUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);

                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                        {
                            HorizontalImgTip.Text += postedFile.FileName + " 非圖檔<br/>";
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

                            //把圖片以","隔開加進格式,最初寫入資料庫時需去頭
                            certificateimage += "," + FinalFileName;
                        }
                    }


                    //寫入資料庫
                    string sql2 = "Update Company set CertificateHorizontalImg=@CertificateHorizontalImg WHERE ID=1";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@CertificateHorizontalImg", certificateimage.TrimStart(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();

                    ShowHorizontalImg();
                }
                else
                {
                    HorizontalImgTip.Text = "The maximum upload size is 100MB!";
                }
            }
            else
            {
                HorizontalImgTip.Text = "There's no file";
            }
        }

        //----------------------------------------------------------------------------------------刪除圖片--------
        protected void VerticalDelBtn_Click(object sender, EventArgs e)
        {
            //刪除直圖
            if (VerticalImgRadioButtonList.SelectedValue.ToString() != "")
            {
                string delimage = VerticalImgRadioButtonList.SelectedValue;
                string saveimages = "";

                //實際刪除圖檔
                File.Delete(Server.MapPath("~/images/") + delimage);

                //更新資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT CertificateVerticalImg FROM Company WHERE ID=1";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string image = reader["CertificateVerticalImg"].ToString();

                    try
                    {
                        //去掉刪除的圖片重組陣列
                        string[] images = image.Split(',');
                        images = Array.FindAll(images, val => val != delimage).ToArray();


                        foreach (var item in images)
                        {
                            saveimages += item + ",";
                        }
                    }
                    catch
                    {
                        //沒圖了
                        saveimages = "";
                    }
                }

                connection.Close();




                if (saveimages != "") //記得去尾","
                {
                    string sql2 = "Update Company set CertificateVerticalImg =@CertificateVerticalImg WHERE ID=1";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@CertificateVerticalImg", saveimages.TrimEnd(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string sql2 = "Update Company set CertificateVerticalImg =NULL WHERE ID=1";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }


                ShowVerticalImg();
                VerticalDelBtn.Visible = false;
            }
        }


        protected void HorizontalDelBtn_Click(object sender, EventArgs e)
        {
            //刪除橫圖
            if (HorizontalImgRadioButtonList.SelectedValue.ToString() != "")
            {
                string delimage = HorizontalImgRadioButtonList.SelectedValue;
                string saveimages = "";

                //實際刪除圖檔
                File.Delete(Server.MapPath("~/images/") + delimage);

                //更新資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT CertificateHorizontalImg FROM Company WHERE ID=1";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string image = reader["CertificateHorizontalImg"].ToString();

                    try
                    {
                        //去掉刪除的圖片重組陣列
                        string[] images = image.Split(',');
                        images = Array.FindAll(images, val => val != delimage).ToArray();


                        foreach (var item in images)
                        {
                            saveimages += item + ",";
                        }
                    }
                    catch
                    {
                        //沒圖了
                        saveimages = "";
                    }
                }

                connection.Close();




                if (saveimages != "") //記得去尾","
                {
                    string sql2 = "Update Company set CertificateHorizontalImg =@CertificateHorizontalImg WHERE ID=1";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@CertificateHorizontalImg", saveimages.TrimEnd(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string sql2 = "Update Company set CertificateHorizontalImg =NULL WHERE ID=1";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }


                ShowHorizontalImg();
                HorizontalDelBtn.Visible = false;
            }
        }





        protected void VerticalImgRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerticalDelBtn.Visible = true;
        }

        protected void HorizontalImgRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            HorizontalDelBtn.Visible = true;
        }
    }
}