using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TayanaYachts
{
    public partial class dealers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LeftMenu();
            }
           
            ShowCountry();
            ShowDealer();
        }

        public void LeftMenu()
        {
            string config = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(config);

            string sql = "SELECT Country,CountryID FROM DealerCountry";
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CountryLeft.Text += "<li><a href=Dealers.aspx?id=" + reader["CountryID"].ToString() + ">" + reader["Country"].ToString()+ "</a></li>";
            }
            connection.Close();
        }

        private void ShowCountry()
        {
            string id= Request["id"];

            if(Request["id"]!=null)
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT CountryID,Country FROM DealerCountry where CountryID=@CountryID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CountryID", Convert.ToInt32(id));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(); 

                if (reader.Read())
                {
                    CountryName.Text = reader["Country"].ToString();
                    CountryNameRight.Text = reader["Country"].ToString();
                    id= reader["CountryID"].ToString();
                }
                connection.Close();
            }
            else //沒點項目顯示第一筆國家
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
                string sql = "SELECT CountryID,Country FROM DealerCountry";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    CountryName.Text = reader["Country"].ToString();
                    CountryNameRight.Text = reader["Country"].ToString();
                    id = reader["CountryID"].ToString();
                }
                connection.Close();
            }
            Session["id"] = id;
        }



        private void ShowDealer()
        {
           
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["TayanaYachtsConnectionString"].ConnectionString);
            string sql = "SELECT * FROM Dealer where CountryID =" + Session["id"];
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                string area = reader["Area"].ToString();
                string imageurl ="/images/"+reader["DealerImgPath"].ToString();
                string name = reader["Name"].ToString();
                string contact = reader["Contact"].ToString();
                string address = reader["Address"].ToString();
                string tel = reader["TEL"].ToString();
                string fax = reader["Fax"].ToString();
                string email = reader["Email"].ToString();
                string link = reader["Link"].ToString();


                DealerInfo.Text += "<li> <div class=\"list02\"><ul><li class=\"list02li\"><div><p><img width='100%' src=\"" + imageurl + "\"></p></div></li><li>";
                DealerInfo.Text += "<span>" + area + "</span><br>";
                if (!string.IsNullOrEmpty(name))
                {
                    DealerInfo.Text += name + "<br>";
                }
                if (!string.IsNullOrEmpty(contact))
                {
                    DealerInfo.Text += "Contact：" + contact + "<br>";
                }
                if (!string.IsNullOrEmpty(address))
                {
                    DealerInfo.Text += "Address：" + address + "<br>";
                }
                if (!string.IsNullOrEmpty(tel))
                {
                    DealerInfo.Text += "TEL：" + tel + "<br>";
                }
                if (!string.IsNullOrEmpty(fax))
                {
                    DealerInfo.Text += "Fax：" + fax + "<br>";
                }
                if (!string.IsNullOrEmpty(email))
                {
                    DealerInfo.Text += "Email：" + email + "<br>";
                }
                if (!string.IsNullOrEmpty(link))
                {
                    DealerInfo.Text += "Link：<a href=\"" + link + "\"target=\"_blank\">"+link+ "</a>";
                }
                DealerInfo.Text += "</li></ul></div></li>";

            }
            connection.Close();
        }
    }
}