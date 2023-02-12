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
    public partial class Dealers_Back : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList();
                Dealer();
                DealerInfoHide.Visible = false;
            }
            Show();





        }

        private void Show()
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

            string sql = "SELECT * ,(SELECT COUNT(*) FROM Dealer where CountryID = DealerCountry.CountryID)as 區域數 FROM DealerCountry";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
            connection.Close();
        }

        protected void DropDownList()
        {
            //國家下拉選單
            CountryDropDownList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT CountryID,Country FROM DealerCountry";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            CountryDropDownList.DataSource = reader;
            CountryDropDownList.DataBind();
            connection.Close();

            //CountryDropDownList.Items.Insert(0, "--Choose City--");
            CountryDropDownList.Items.Insert(0, new ListItem("--Choose City--", "0"));
        }

        protected void AddCountry_Click(object sender, EventArgs e)
        {
            //新增國家
            if (AddCountryTextBox.Text != "")
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

                string sql = "INSERT INTO DealerCountry (Country) VALUES (@Country)";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Country", AddCountryTextBox.Text);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                Show();
                DropDownList();
                AddCountryTextBox.Text = "";
                AddCountryTip.Text = "";
            }
            else
            {
                AddCountryTip.Visible = true;
                AddCountryTip.Text = "Please enter country!";
            }
        }

        private void Dealer()
        {//顯示所選國家之地區代理商
            if (CountryDropDownList.SelectedItem.Value != "0")
            {
                DealerRadioButtonList.Items.Clear();

                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT Area,DealerID FROM Dealer WHERE CountryID = @CountryID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CountryID", CountryDropDownList.SelectedItem.Value);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string area = reader["Area"].ToString();
                    string dealerid = reader["DealerID"].ToString();
                    ListItem listItem = new ListItem();
                    listItem.Text = area;
                    listItem.Value = dealerid;
                    DealerRadioButtonList.Items.Add(listItem);
                }
                connection.Close();
                DealerInfoHide.Visible = false;
            }
        }



        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();//取得點擊這列的id

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

            SqlCommand command = new SqlCommand($"DELETE  FROM DealerCountry WHERE  (CountryID = {id});DELETE FROM Dealer WHERE CountryID = {id}", connection);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Show();
            DropDownList();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            Show();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Show();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox tb = (TextBox)GridView1.Rows[e.RowIndex].FindControl("CountryEdit");

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "update DealerCountry set Country=@Country where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Country", tb.Text.ToString());
            command.Parameters.AddWithValue("@CountryID", GridView1.DataKeys[e.RowIndex].Value.ToString());

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            GridView1.EditIndex = -1;
            Show();
            DropDownList();
        }






        protected void AddArea_Click(object sender, EventArgs e)
        {
            //新增地區代理商
            if (AddAreaTextBox.Text != "")
            {
                if (CountryDropDownList.SelectedItem.Value != "0")
                {
                    string selectcountry = CountryDropDownList.SelectedItem.Value;
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

                    string sql = "INSERT INTO Dealer (CountryID,Area) VALUES (@CountryID,@Area)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@CountryID", Convert.ToInt32(selectcountry));
                    command.Parameters.AddWithValue("@Area", AddAreaTextBox.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    Show();
                    Dealer();



                    //預選
                    string sql2 = "SELECT DealerID FROM Dealer where Area = @Area";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@Area", AddAreaTextBox.Text);
                    connection.Open();
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                       DealerRadioButtonList.SelectedValue = reader2["DealerID"].ToString();
                    }
                    connection.Close();

                    ShowDealerInfo();
                    AddAreaTextBox.Text = "";
                }
                else
                {
                    AddAreaTip.Visible = true;
                    AddAreaTip.Text = "Please choose country of area!";
                }
            }
            else
            {
                AddAreaTip.Visible = true;
                AddAreaTip.Text = "Please enter area!";
            }
        }


        protected void DealerRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {   
            ShowDealerInfo();
        }





        protected void ShowDealerInfo()
        {
            //顯示代理商詳細資訊
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

            string sql = "SELECT * FROM Dealer where DealerID = @DealerID";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Agentimg.ImageUrl = "/images/" + reader["DealerImgPath"].ToString();
                Area.Text = reader["Area"].ToString();
                Name.Text = reader["Name"].ToString();
                Contact.Text = reader["Contact"].ToString();
                Address.Text = reader["Address"].ToString();
                TEL.Text = reader["TEL"].ToString();
                Fax.Text = reader["Fax"].ToString();
                Email.Text = reader["Email"].ToString();
                Link.Text = reader["Link"].ToString();
                InitDate.Text = reader["InitDate"].ToString();
            }
            connection.Close();

            Country.Text = CountryDropDownList.SelectedItem.Text;
            DealerInfoHide.Visible = true;

            TipHide();

        }

        //protected void DealerRadio()
        //{   //顯示代理商詳細資訊
        //    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

        //    string sql = "SELECT * FROM Dealer where DealerID = @DealerID";

        //    SqlCommand command = new SqlCommand(sql, connection);
        //    command.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {

        //        Area.Text = reader["Area"].ToString();

        //        Response.Write(DealerRadioButtonList.SelectedValue);

        //    }
        //    connection.Close();



        //}

        protected void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {//隨著選擇地區變換顯示詳細資訊
            Dealer();
            TipHide();
        }

        protected void DeleteAgent_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

            //實際刪除代理商圖片
            string sql = "SELECT DealerImgPath FROM Dealer where DealerID = @DealerID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string img= reader["DealerImgPath"].ToString();
                if(img!= "NoImage.jpg")
                {
                    File.Delete(Server.MapPath("~/images/") + img);
                }
            }
            connection.Close();





            //刪除代理商資料
            SqlCommand command2 = new SqlCommand($"DELETE  FROM Dealer WHERE (DealerID = @DealerID)", connection);
            command2.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);
            connection.Open();
            command2.ExecuteNonQuery();
            connection.Close();

            Show();
            Dealer();
            DealerInfoHide.Visible = false;
        }

        protected void UpdateAgent_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "update Dealer set Area=@Area,Name=@Name,Contact=@Contact,Address=@Address,TEL=@TEL,Fax=@Fax,Email=@Email,Link=@Link where DealerID = @DealerID";

            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Area", Area.Text.ToString());
            command.Parameters.AddWithValue("@Name", Name.Text.ToString());
            command.Parameters.AddWithValue("@Contact", Contact.Text.ToString());
            command.Parameters.AddWithValue("@Address", Address.Text.ToString());
            command.Parameters.AddWithValue("@TEL", TEL.Text.ToString());
            command.Parameters.AddWithValue("@Fax", Fax.Text.ToString());
            command.Parameters.AddWithValue("@Email", Email.Text.ToString());
            command.Parameters.AddWithValue("@Link", Link.Text.ToString());
            command.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            DealerRadioButtonList.SelectedItem.Text = Area.Text.ToString();
        }



        protected void AgentimgUploadBtn_Click(object sender, EventArgs e)
        {

            if (AgentimgUpload.HasFile)
            {

                string SavePath = Server.MapPath("~/images/");
                string FinalFileName;
                string FileName = AgentimgUpload.FileName;
                //檢查副檔名
                int x = FileName.LastIndexOf('.');
                string a = FileName.Remove(0, x);
                if (a != ".jpg" && a != ".png" && a != ".gif" && a != ".jpeg")
                {
                    Response.Write("<Script language='JavaScript'>alert('此檔案非圖檔！');</Script>");
                }
                else
                {
                    //刪舊圖檔
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql2 = "SELECT DealerImgPath FROM Dealer WHERE DealerID= @DealerID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);
                    connection.Open();
                    SqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                        string delFileName = reader2["DealerImgPath"].ToString();
                        //有舊圖且非預設圖才執行刪除
                        if (!String.IsNullOrEmpty(delFileName)&& delFileName!= "NoImage.jpg")
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

                    AgentimgUpload.SaveAs(PathToCheck);

                    if (TempFileName != "")
                    {
                        FinalFileName = TempFileName;
                    }
                    else
                    {
                        FinalFileName = FileName;
                    }




                    string sql = "update Dealer set DealerImgPath=@DealerImgPath where DealerID = @DealerID";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@DealerImgPath", FinalFileName);
                    command.Parameters.AddWithValue("@DealerID", DealerRadioButtonList.SelectedValue);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    Agentimg.ImageUrl = "/images/" + FinalFileName;
                }
            }
            else
            {
                AgentimgUploadTip.Visible = true;
                AgentimgUploadTip.Text = "There's no file to upload!";
            }
        }





        protected void TipHide()
        {
            AgentimgUploadTip.Visible = false;
            AddCountryTip.Visible = false;
            AddAreaTip.Visible = false;
        }
    }
}