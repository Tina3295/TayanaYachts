using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace TayanaYachts
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterAccount_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(InputEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                if (InputPassword.Text == RepeatPassword.Text)
                {
                    SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                    //檢查帳號重複
                    SqlCommand command = new SqlCommand($"select * from Users where (Email=@Email)", connection);
                    command.Parameters.AddWithValue("@Email", InputEmail.Text);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        connection.Close();
                        Tip.Text = "Account already registered！";
                    }
                    else
                    {
                        connection.Close();

                        string sql2 = "INSERT INTO Users(Email,Password,Salt,FirstName,LastName) VALUES(@Email,@Password,@Salt,@FirstName,@LastName)";
                        SqlCommand command2 = new SqlCommand(sql2, connection);


                        string password = InputPassword.Text;
                        var salt = CreateSalt();
                        string saltStr = Convert.ToBase64String(salt); //將 byte 改回字串存回資料表
                        var hash = HashPassword(password, salt);
                        string hashPassword = Convert.ToBase64String(hash);

                        

                        command2.Parameters.AddWithValue("@Email", InputEmail.Text);
                        command2.Parameters.AddWithValue("@Password", hashPassword);
                        command2.Parameters.AddWithValue("@Salt", saltStr);
                        command2.Parameters.AddWithValue("@FirstName", FirstName.Text);
                        command2.Parameters.AddWithValue("@LastName", LastName.Text);
                        
                        connection.Open();
                        command2.ExecuteNonQuery();
                        connection.Close();

                        //跳轉登入頁
                        //Response.Redirect("Login.aspx");
                        Response.Write("<script>alert('Register success!Login to continue.');location.href='Login.aspx';</script>");

                    }

                    
                }
                else
                {
                    Tip.Text = "The password doesn't match the confirmation password.";
                }
            }
            else
            {
                Tip.Text = "Email format error！";
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