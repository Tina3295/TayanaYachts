using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using System.Text.RegularExpressions;

namespace TayanaYachts
{
    public partial class UserManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Userinformation.admin == "General")
            {
                AdministratorOnly.Visible = false;

                AdminTip.Text = "You don't have permission to browse this page!";
            }
            Master.Page.Title = "User Management";
            Show();
        }

        private void Show()
        {

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Users order by Permission desc,UserID";
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
            connection.Close();

        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();//取得點擊這列的id

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);

            SqlCommand command = new SqlCommand($"DELETE  FROM Users WHERE  (UserID = {id}) ", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Response.Redirect(Request.Url.ToString());
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
            DropDownList UserIdentityEdit = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("UserIdentityEdit");

            //Label EmailNoEdit = (Label)GridView1.Rows[e.RowIndex].FindControl("EmailNoEdit");
            TextBox FirstNameEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("FirstNameEdit");
            TextBox LastNameEdit = (TextBox)GridView1.Rows[e.RowIndex].FindControl("LastNameEdit");

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);



            //任何人為Top或Middle,所有權限開1
            if (UserIdentityEdit.SelectedItem.Value == "Top" || UserIdentityEdit.SelectedItem.Value == "Middle")
            {
                string sql = "update Users set FirstName=@FirstName,LastName=@LastName,Permission=@Permission,Yachts=1,News=1,Company=1,Dealers=1 where UserID = @UserID";
                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@FirstName", FirstNameEdit.Text.ToString());
                command.Parameters.AddWithValue("@LastName", LastNameEdit.Text.ToString());
                command.Parameters.AddWithValue("@Permission", UserIdentityEdit.SelectedItem.Value.ToString());
                command.Parameters.AddWithValue("@UserID", GridView1.DataKeys[e.RowIndex].Value.ToString());

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else //變成General權限歸0,如果原本是General權限不動
            {
                //先看原本權限是什麼
                string permission = "";

                string sql = "SELECT Permission FROM Users where UserID = @UserID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserID", GridView1.DataKeys[e.RowIndex].Value.ToString());
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    permission = reader["Permission"].ToString();
                }
                connection.Close();


                if (permission == "Top" || permission == "Middle")
                {
                    string sql2 = "update Users set FirstName=@FirstName,LastName=@LastName,Permission=@Permission,Yachts=0,News=0,Company=0,Dealers=0 where UserID = @UserID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);

                    command2.Parameters.AddWithValue("@FirstName", FirstNameEdit.Text.ToString());
                    command2.Parameters.AddWithValue("@LastName", LastNameEdit.Text.ToString());
                    command2.Parameters.AddWithValue("@Permission", UserIdentityEdit.SelectedItem.Value.ToString());
                    command2.Parameters.AddWithValue("@UserID", GridView1.DataKeys[e.RowIndex].Value.ToString());

                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    string sql2 = "update Users set FirstName=@FirstName,LastName=@LastName where UserID = @UserID";
                    SqlCommand command2 = new SqlCommand(sql2, connection);

                    command2.Parameters.AddWithValue("@FirstName", FirstNameEdit.Text.ToString());
                    command2.Parameters.AddWithValue("@LastName", LastNameEdit.Text.ToString());
                    command2.Parameters.AddWithValue("@UserID", GridView1.DataKeys[e.RowIndex].Value.ToString());

                    connection.Open();
                    command2.ExecuteNonQuery();
                    connection.Close();
                }
            }
            GridView1.EditIndex = -1;
            Show();

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)   //編輯時身分預選
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList UserIdentityEdit = (DropDownList)e.Row.FindControl("UserIdentityEdit");

                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    string sql = "SELECT Permission FROM Users where UserID=@UserID";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("UserID", GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        UserIdentityEdit.SelectedValue = reader["Permission"].ToString();
                    }
                    connection.Close();

                }
            }
        }


        protected void GridView1_DataBound(object sender, EventArgs e)//如果是TOP不能改top//如果是MIddle不能改tOp middle
        {
            //GridView1.Rows[0].Cells[7].Controls.Clear();
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            int count;


            if (Userinformation.admin == "Top")
            {
                string sql = "SELECT Count(UserID) as Num FROM Users where Permission='Top'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    count = Convert.ToInt32(reader["Num"].ToString());
                    for (int i = 0; i < count; i++)
                    {
                        GridView1.Rows[i].Cells[7].Controls.Clear();
                    }
                }
                connection.Close();


            }
            else
            {
                string sql = "SELECT Count(UserID) as Num FROM Users where Permission='Top' or Permission='Middle'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    count = Convert.ToInt32(reader["Num"].ToString());
                    Response.Write(count);
                    for (int i = 0; i < count; i++)
                    {
                        GridView1.Rows[i].Cells[7].Controls.Clear();
                        GridView1.Rows[i].Cells[6].Controls.Clear();
                    }
                }
                connection.Close();


            }





        }

        protected void RegisterAccount_Click(object sender, EventArgs e)
        {
            //不能空白
            if (!string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                //檢查email格式
                if (Regex.IsMatch(Email.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {

                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    //檢查帳號重複
                    SqlCommand command = new SqlCommand($"select * from Users where (Email=@Email)", connection);
                    command.Parameters.AddWithValue("@Email", Email.Text);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        connection.Close();
                        Tip.Text = "Account already had！";
                    }
                    else
                    {
                        connection.Close();

                        string sql2 = "INSERT INTO Users(Email,Password,Salt,FirstName,LastName) VALUES(@Email,@Password,@Salt,@FirstName,@LastName)";
                        SqlCommand command2 = new SqlCommand(sql2, connection);


                        string password = Password.Text;
                        var salt = CreateSalt();
                        string saltStr = Convert.ToBase64String(salt); //將 byte 改回字串存回資料表
                        var hash = HashPassword(password, salt);
                        string hashPassword = Convert.ToBase64String(hash);



                        command2.Parameters.AddWithValue("@Email", Email.Text);
                        command2.Parameters.AddWithValue("@Password", hashPassword);
                        command2.Parameters.AddWithValue("@Salt", saltStr);
                        command2.Parameters.AddWithValue("@FirstName", FirstName.Text);
                        command2.Parameters.AddWithValue("@LastName", LastName.Text);

                        connection.Open();
                        command2.ExecuteNonQuery();
                        connection.Close();

                        Show();

                        Email.Text = "";
                        Password.Text = "";
                        FirstName.Text = "";
                        LastName.Text = "";
                        Tip.Text = "";
                        //跳轉登入頁
                        //Response.Redirect("Login.aspx");
                        //Response.Write("<script>alert('Register success!Login to continue.');location.href='Login.aspx';</script>");

                    }
                }
                else
                {
                    Tip.Text = "Email format error！";
                }
            }
            else
            {
                Tip.Text = "Email and password is required ！";
            }
        }






        // Argon2 加密
        //產生 Salt 功能
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }
        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 2; // 迭代運算次數
            argon2.MemorySize = 1024 * 512; // 512 MB

            return argon2.GetBytes(16);
        }
    }
}