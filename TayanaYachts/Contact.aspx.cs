using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CountryList();
                ModelList();
            }


        }

        protected void Submit_Click(object sender, ImageClickEventArgs e)
        {
            if (String.IsNullOrEmpty(Recaptcha.Response))
            {
                RecaptchaTip.Text = "Captcha cannot be empty.";
            }
            else
            {
                var result = Recaptcha.Verify();
                if (result.Success)   
                {
                    //"我不是機器人驗證"成功
                    if(Regex.IsMatch(Email.Text,@"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    {
                        SendGmail();
                        Response.Write("<script>alert('Thank you for contacting us!');location.href='Contact.aspx';</script>");
                    }
                    else
                    {
                        EmailTip.Text = "Email format error!";
                    }
                }
                else
                {
                    RecaptchaTip.Text = "Error(s): ";

                    foreach (var err in result.ErrorCodes)
                    {
                        RecaptchaTip.Text = RecaptchaTip.Text + err;
                    }
                }
            }
        }


        protected void CountryList()
        {
            //國家下拉選單
            CountryDropDownList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT Country FROM DealerCountry";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                listItem.Text = reader["Country"].ToString();
                listItem.Value = reader["Country"].ToString();
                CountryDropDownList.Items.Add(listItem);
            }
            connection.Close();

            //CountryDropDownList.Items.Insert(0, "--Choose City--");
            //CountryDropDownList.Items.Insert(0, new ListItem("--Choose City--", "0"));
        }
        protected void ModelList()
        {
            //國家下拉選單
            ModelDropDownList.Items.Clear();

            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT YachtModel,NewDesign,NewBuilding FROM Yachts";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string model = reader["yachtModel"].ToString();
                string isNewDesign = reader["NewDesign"].ToString();
                string isNewBuilding = reader["NewBuilding"].ToString();

                ListItem listItem = new ListItem();
                if (isNewDesign.Equals("True"))
                {
                    listItem.Text = $"{model} (New Design)";
                    listItem.Value = $"{model} (New Design)";
                    ModelDropDownList.Items.Add(listItem);
                }
                else if (isNewBuilding.Equals("True"))
                {
                    listItem.Text = $"{model} (New Building)";
                    listItem.Value = $"{model} (New Building)";
                    ModelDropDownList.Items.Add(listItem);
                }
                else
                {
                    listItem.Text = model;
                    listItem.Value = model;
                    ModelDropDownList.Items.Add(listItem);
                }
            }
            connection.Close(); 
        }


        public void SendGmail()
        {
            //宣告使用 MimeMessage
            var message = new MimeMessage();
            //設定發信地址 ("發信人", "發信 email")
            message.From.Add(new MailboxAddress("TayanaYacht", "swps41225@gmail.com"));
            //設定收信地址 ("收信人", "收信 email")
            message.To.Add(new MailboxAddress(Name.Text.Trim(), Email.Text.Trim()));
            //寄件副本email
            //message.Cc.Add(new MailboxAddress("收信人名稱", "swps41225@gmail.com"));
            //設定優先權
            //message.Priority = MessagePriority.Normal;
            //信件標題
            message.Subject = "TayanaYacht Auto Email";
            //建立 html 郵件格式
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>Thank you for contacting us!</h1>" +
                $"<h3>Name : {Name.Text.Trim()}</h3>" +
                $"<h3>Email : {Email.Text.Trim()}</h3>" +
                $"<h3>Phone : {Phone.Text.Trim()}</h3>" +
                $"<h3>Country : {CountryDropDownList.SelectedValue}</h3>" +
                $"<h3>Type : {ModelDropDownList.SelectedValue}</h3>" +
                $"<h3>Comments : </h3>" +
                $"<p>{Comments.Text.Trim()}</p>";
            //設定郵件內容
            message.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定 false 關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("swps41225@gmail.com", "vxzyggszsjmvmmqb");
                //發信
                client.Send(message);
                //結束連線
                client.Disconnect(true);
            }
        }
    }
}