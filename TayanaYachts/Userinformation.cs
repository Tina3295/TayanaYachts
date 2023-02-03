using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TayanaYachts
{
    public class Userinformation
    {
        //登入user資訊
        public static string email;
        public static string name;
        public static string permission;


        //權限分級
        public static string Permission
        {
            get { return permission; }
            set
            {
                if (value == "Top Administrator")
                {
                    permission = "1";
                }
                else if (value == "General Administrator")
                {
                    permission = "2";
                }
                else
                {
                    permission = "3";
                }
            }
        }



        //權限賦予
        public static bool videoadmin;
        public static bool albumadmin;
        public static bool faqadmin;
        public static bool linkadmin;

        public static void Admin()
        {
            if (permission == "1")
            {
                videoadmin = true;
                albumadmin = true;
                faqadmin = true;
                linkadmin = true;
            }

            if (permission == "2")
            {
                SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["LoginConnectionString"].ConnectionString);
                string sql = "SELECT * FROM SystemRights where Email=@Email";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", email); //賦予參數值

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    videoadmin = Convert.ToBoolean(reader["Video"]);
                    albumadmin = Convert.ToBoolean(reader["Album"]);
                    faqadmin = Convert.ToBoolean(reader["FAQ"]);
                    linkadmin = Convert.ToBoolean(reader["Link"]);
                }
                connection.Close();
            }

            if (permission == "3")
            {
                videoadmin = false;
                albumadmin = false;
                faqadmin = false;
                linkadmin = false;
            }

        }
    }
}