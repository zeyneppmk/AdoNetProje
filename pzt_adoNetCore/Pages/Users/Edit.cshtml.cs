using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pzt_adoNetCore.Pages.Users
{
	public class EditModel : PageModel
    {
        public User userbilgi = new User();
        public void OnGet()
        {
            string ID = Request.Query["ID"];


            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "select * from Userlar where ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            userbilgi.ID = "" + reader.GetInt32(0);
                            userbilgi.fullName = reader.GetString(1);
                            userbilgi.email = reader.GetString(2);
                            userbilgi.telefon = "" + reader.GetInt32(3);
                            userbilgi.userRole = reader.GetString(4);
                            userbilgi.sifre = reader.GetString(5);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OnPost()
        {
            userbilgi.ID = Request.Form["ID"];
            userbilgi.fullName = Request.Form["fullName"].ToString();
            userbilgi.email = Request.Form["email"].ToString();
            userbilgi.telefon = Request.Form["telefon"].ToString();
            userbilgi.userRole = Request.Form["userRole"].ToString();
            userbilgi.sifre = Request.Form["sifre"].ToString();


            try
            {
                string connectionString = "Server=localhost; Database=Egitim User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "update Userlar set fullName=@fullName, email=@email, telefon=@telefon, userRole=@userRole, sifre=@sifre where ID=@ID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fullName", userbilgi.fullName);
                        command.Parameters.AddWithValue("@email", userbilgi.email);
                        command.Parameters.AddWithValue("@telefon", userbilgi.telefon);
                        command.Parameters.AddWithValue("@userRole", userbilgi.userRole);
                        command.Parameters.AddWithValue("@sifre", userbilgi.sifre);
                        command.Parameters.AddWithValue("@ID", userbilgi.ID);
                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {

            }

            Response.Redirect("/Users/Index");
        }
    }
}
