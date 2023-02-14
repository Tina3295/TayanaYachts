using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {


            //檢查帳號是否存在
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            SqlCommand command = new SqlCommand($"select * from Users where (Email=@Email)", connection);
            command.Parameters.AddWithValue("@Email", Email.Text);
            
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count > 0) //有註冊過
            {
                //將字串轉回 byte
                byte[] hash = Convert.FromBase64String(table.Rows[0]["Password"].ToString());
                byte[] salt = Convert.FromBase64String(table.Rows[0]["Salt"].ToString());
                //驗證密碼
                bool success = VerifyHash(Password.Text, salt, hash);

                if (success)
                {
                    //宣告驗證票要夾帶的資料 (用;區隔)
                    string userData = table.Rows[0]["Permission"].ToString() + ";" + table.Rows[0]["Email"].ToString() + ";" 
                                        + table.Rows[0]["FirstName"].ToString() + ";" + table.Rows[0]["LastName"].ToString()+";"
                                        + table.Rows[0]["Yachts"].ToString() + ";" + table.Rows[0]["News"].ToString() + ";"
                                        + table.Rows[0]["Company"].ToString() + ";" + table.Rows[0]["Dealers"].ToString() + ";";
                    //設定驗證票(夾帶資料，cookie 命名) // 需額外引入using System.Web.Configuration;
                    SetAuthenTicket(userData, Email.Text);
                    
                    Response.Redirect("Index_Back.aspx");
                }
                else
                {
                    //資料庫裡找不到相同資料時，表示密碼有誤!
                    Tip.Text = "password error, login failed!";
                   
                    return;
                }
            }
            else
            {
                Tip.Text = "Account error or unregistered.";
            }

        }

        //設定驗證票
        private void SetAuthenTicket(string userData, string userId)
        {
            //如果有勾rememberme就5小時，沒有就1小時
            int time;
            if(Check.Checked)
            {
                time = 5;
            }
            else
            {
                time = 1;
            }

            //宣告一個驗證票
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(time), false, userData);
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立 Cookie
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //將 Cookie 寫入回應
            Response.Cookies.Add(authenticationCookie);
        }



        // Argon2 驗證加密密碼
        // Hash 處理加鹽的密碼功能
        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            //底下這些數字會影響運算時間，而且驗證時要用一樣的值
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // 4 核心就設成 8
            argon2.Iterations = 2; //迭代運算次數
            argon2.MemorySize = 1024 * 512; // 512MB

            return argon2.GetBytes(16);
        }
        //驗證
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash); // LINEQ
        }
    }
}