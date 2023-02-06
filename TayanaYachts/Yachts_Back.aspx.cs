using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Yachts_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ShowModel();
            }
        }



        protected void AddModel_Click(object sender, EventArgs e)
        {
            //新增Model
            string model = AddModelTextBox.Text;
            string modelnum = AddModelNumTextBox.Text;


            if (model != "" && modelnum != "")
            {

                //比對名字重複
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand($"select YachtModel from Yachts where (YachtModel=@YachtModel)", connection);
                command.Parameters.AddWithValue("@YachtModel", model+" "+modelnum);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    connection.Close();
                    AddModelTip.Text = "There's already had same model!";
                }
                else
                {
                    connection.Close();


                    //隨機識別碼+秒
                    string guid = Guid.NewGuid().ToString().Trim() + DateTime.Now.ToString("ff");
                    string sql2 = "INSERT INTO Yachts (YachtModel, NewDesign, Guid, NewBuilding) VALUES (@YachtModel, @NewDesign, @Guid, @NewBuilding)";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtModel", model + " " + modelnum);
                    command2.Parameters.AddWithValue("@NewDesign", SetNewDesign.Checked.ToString());
                    command2.Parameters.AddWithValue("@NewBuilding", SetNewBuilding.Checked.ToString());
                    command2.Parameters.AddWithValue("@Guid", guid);
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();

                    //渲染畫面
                    ShowModel();



                    AddModelTextBox.Text = "";
                    AddModelNumTextBox.Text = "";
                    SetNewDesign.Checked = false;
                    SetNewBuilding.Checked = false;

                    //新增項選取顯示右邊
                    ModelRadioBtnList.SelectedValue = model + " " + modelnum;
                    ShowModelRightPanel();
                    BannerImagesRadioButtonList.Items.Clear();
                    AddModelTip.Text = "";
                }
            }
            else
            {
                AddModelTip.Text = "Please enter Model Name!";
            }
        }



        protected void ShowModel()
        {
            ModelRadioBtnList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT YachtModel,NewDesign,NewBuilding FROM Yachts ORDER BY NewBuilding DESC,NewDesign DESC,YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
         
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bool isnewbuilding = Convert.ToBoolean(reader["NewBuilding"].ToString());
                bool isnewdesign = Convert.ToBoolean(reader["NewDesign"].ToString());
                string model = reader["YachtModel"].ToString();


                if (isnewbuilding && isnewdesign)
                {
                    ListItem listItem = new ListItem($"{model}<img src='/images/newbuilding.jpg' style='margin-left:10px' height=15px/><img src='/images/newdesign.jpg' style='margin-left:10px' height=15px/>", model);
                    ModelRadioBtnList.Items.Add(listItem);
                }
                else if (isnewbuilding)
                {
                    ListItem listItem = new ListItem($"{model}<img src='/images/newbuilding.jpg' style='margin-left:10px' height=15px/>", model);
                    ModelRadioBtnList.Items.Add(listItem); 
                }
                else if(isnewdesign)
                {
                    ListItem listItem = new ListItem($"{model}<img src='/images/newdesign.jpg' style='margin-left:10px' height=15px/>", model);
                    ModelRadioBtnList.Items.Add(listItem); 
                }
                else
                {
                    ListItem listItem = new ListItem(model, model);
                    ModelRadioBtnList.Items.Add(listItem);
                }
            }
            connection.Close();

        }

        protected void ModelRadioBtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowModelRightPanel();
            ShowBannerImages();
            TipHide();
        }








        protected void ShowModelRightPanel()
        {
            //顯示右邊區塊及資料
            ModelUpdatePanel.Visible = true;

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT YachtModel,NewDesign,NewBuilding,BannerImg FROM Yachts where YachtModel=@YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedItem.Value);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string[] model = reader["YachtModel"].ToString().Split(' ');

                ModelName.Text = model[0];
                ModelNum.Text = model[1];
                NewDesign.Checked= Convert.ToBoolean(reader["NewDesign"].ToString());
                NewBuilding.Checked = Convert.ToBoolean(reader["NewBuilding"].ToString());
            }
            connection.Close();

        }

        protected void UpdateModelBtn_Click(object sender, EventArgs e)
        {
            string modelName = ModelName.Text;
            string modelNum = ModelNum.Text;

            if (!string.IsNullOrEmpty(modelName) && !string.IsNullOrEmpty(modelNum))
            {

                //比對名字重複
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand($"select YachtModel from Yachts where (YachtModel=@YachtModel)", connection);
                command.Parameters.AddWithValue("@YachtModel", modelName + " " + modelNum);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                //有行數代表有重複再判斷是否是自己
                if (table.Rows.Count > 0)
                {   
                    //竟是我自己，繼續執行
                    if (table.Rows[0][0].ToString() == ModelRadioBtnList.SelectedValue)
                    {
                        UpdateModel();
                    }
                    else
                    {
                        UpdateModelTip.Text = "There's already had same model!";
                    }
                }
                else
                {
                    UpdateModel();
                }





            }
            else
            {
                UpdateModelTip.Text = "Model name is required";
            }
        }



        protected void UpdateModel()
        {
            //輔助更新副程式

            string modelName = ModelName.Text;
            string modelNum = ModelNum.Text;
            //取得YachtID
            string yachtid = "";

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT YachtID FROM Yachts where YachtModel=@YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedItem.Value);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                yachtid = reader["YachtID"].ToString();
            }
            connection.Close();




            string sql3 = "Update Yachts set YachtModel=@YachtModel,NewDesign=@NewDesign,NewBuilding=@NewBuilding WHERE YachtID=" + yachtid;
            SqlCommand command3 = new SqlCommand(sql3, connection);
            command3.Parameters.AddWithValue("@YachtModel", modelName + " " + modelNum);
            command3.Parameters.AddWithValue("@NewDesign", NewDesign.Checked.ToString());
            command3.Parameters.AddWithValue("@NewBuilding", NewBuilding.Checked.ToString());
            connection.Open();
            command3.ExecuteNonQuery();
            connection.Close();

            //並預選
            ShowModel();
            ModelRadioBtnList.SelectedValue = modelName + " " + modelNum;
            UpdateModelTip.Text = "*Upload Success! - " + DateTime.Now.ToString("G");
        }










        protected void DeleteModelBtn_Click(object sender, EventArgs e)
        {

        }






        //-------------------------------------------------以下多圖-------------------------------------------------

        protected void AddImagesBtn_Click(object sender, EventArgs e)
        {
            //增加多圖
            string image = "";
            AddImagesTip.Text = "";

            if (AddImagesFileUpload.HasFile)
            {
                int fileSize = AddImagesFileUpload.PostedFile.ContentLength; //Byte
                if (fileSize < 1024 * 1024 * 10)
                {
                    //撈出原本的圖片csv格式
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT BannerImg FROM Yachts WHERE YachtModel= @YachtModel";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedValue);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        image = reader["BannerImg"].ToString();
                    }
                    connection.Close();

                    //第一次上傳先指定第一張當FirstBannerImg
                    bool haveimg=true;
                    if (string.IsNullOrEmpty(image))
                    {
                        haveimg = false;
                    }



                    //設定存檔路徑
                    string SavePath = Server.MapPath("~/images/");
                    string FinalFileName;


                    //處理每個上傳檔案
                    foreach (HttpPostedFile postedFile in AddImagesFileUpload.PostedFiles)
                    {
                        string FileName = postedFile.FileName;
                        //檢查副檔名
                        int x = FileName.LastIndexOf('.');
                        string a = FileName.Remove(0, x);

                        if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                        {
                            AddImagesTip.Text += postedFile.FileName + " 非圖檔<br/>";
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
                            image += "," + FinalFileName;
                        }
                    }


                    //寫入多圖片資料庫
                    string sql2 = "Update Yachts set BannerImg =@BannerImg WHERE YachtModel= @YachtModel";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedValue);
                    command2.Parameters.AddWithValue("@BannerImg", image.TrimStart(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();


                    //判斷原本沒有,所以將第一圖指定為FirstBannerImg
                    if (haveimg==false)
                    {                      
                        if(!string.IsNullOrEmpty(image))   //真的有上傳圖成功
                        {
                            string firstImage;

                            if (image.Contains(","))  //傳了多張圖
                            {

                                string[] images = image.TrimStart(',').Split(',');
                                firstImage = images[0];
                            }
                            else                      //傳單張
                            {
                                firstImage = image;
                            }


                            string sql3 = "Update Yachts set FirstBannerImg =@FirstBannerImg WHERE YachtModel= @YachtModel";
                            SqlCommand command3 = new SqlCommand(sql3, connection);
                            command3.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedValue);
                            command3.Parameters.AddWithValue("@FirstBannerImg", firstImage);
                            connection.Open();
                            command3.ExecuteNonQuery();
                            connection.Close();
                        }
                    }



                    //渲染畫面
                    ShowBannerImages();
                }
                else
                {
                    AddImagesTip.Text = "The maximum upload size is 10MB!";
                }
            }
            else
            {
                AddImagesTip.Text = "There's no file";
            }
        }





        private void ShowBannerImages()
        {//顯示出多圖
            BannerImagesRadioButtonList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT BannerImg,FirstBannerImg FROM Yachts WHERE YachtModel= @YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string image = reader["BannerImg"].ToString();
                string first = reader["FirstBannerImg"].ToString();

                if (!String.IsNullOrEmpty(image))
                {
                    string[] images = image.Split(',');

                    foreach (var item in images)
                    {
                        if (item == first)
                        {
                            ListItem listItem = new ListItem($"<img src='/images/{item}' class='firstbannerimage' style='margin:10px' width='115px'/>", item);
                            BannerImagesRadioButtonList.Items.Add(listItem);
                        }
                        else
                        {
                            ListItem listItem = new ListItem($"<img src='/images/{item}' style='margin:10px' width='115px'/>", item);
                            BannerImagesRadioButtonList.Items.Add(listItem);
                        }
                    }

                }
            }
            connection.Close();
            DelBannerImage.Visible = false;
            SetFirst.Visible = false;
        }


        protected void BannerImageDel_Click(object sender, EventArgs e)
        {
            //刪除圖
            if (BannerImagesRadioButtonList.SelectedValue.ToString() != "")
            {

                string delimage = BannerImagesRadioButtonList.SelectedValue;
                string selectmodel = ModelRadioBtnList.SelectedValue;

                string saveimages = "";

                //實際刪除圖檔
                File.Delete(Server.MapPath("~/images/") + delimage);

                //更新資料庫
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT BannerImg FROM Yachts WHERE YachtModel= @YachtModel";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@YachtModel", selectmodel);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string image = reader["BannerImg"].ToString();

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
                    string sql2 = "Update Yachts set BannerImg =@BannerImg WHERE YachtModel= @YachtModel";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtModel", selectmodel);
                    command2.Parameters.AddWithValue("@BannerImg", saveimages.TrimEnd(','));
                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string sql2 = "Update Yachts set BannerImg =NULL WHERE YachtModel= @YachtModel";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@YachtModel", selectmodel);

                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }


                ShowBannerImages();
                
            }
        }

        protected void BannerImagesRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DelBannerImage.Visible = true;
            SetFirst.Visible = true;
        }


        protected void SetFirst_Click(object sender, EventArgs e)
        {//指定第一張圖
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "Update Yachts set FirstBannerImg=@FirstBannerImg WHERE YachtModel=@YachtModel";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FirstBannerImg", BannerImagesRadioButtonList.SelectedValue);
            command.Parameters.AddWithValue("@YachtModel", ModelRadioBtnList.SelectedValue);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            ShowBannerImages();//重新渲染
        }













        protected void TipHide()
        {
            UpdateModelTip.Text = "";
        }

 
    }
}