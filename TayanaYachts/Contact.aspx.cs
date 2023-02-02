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
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CountryList();
            
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
                    //此處可加入"我不是機器人驗證"成功後要做的事

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
            string sql = "SELECT CountryID,Country FROM DealerCountry";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListItem listItem = new ListItem();
                listItem.Text = reader["Country"].ToString();
                listItem.Value = reader["CountryID"].ToString();
                CountryDropDownList.Items.Add(listItem);
            }
            connection.Close();

            //CountryDropDownList.Items.Insert(0, "--Choose City--");
            CountryDropDownList.Items.Insert(0, new ListItem("--Choose City--", "0"));
        }
    }
}