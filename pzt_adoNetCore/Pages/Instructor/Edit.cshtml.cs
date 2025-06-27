using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pzt_adoNetCore.Pages.Instructor
{
	public class EditModel : PageModel
    {
        public Egitmen egitmenbilgi = new Egitmen();

        public void OnGet()
        {
            string ID = Request.Query["id"];


            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "select * from Instructor where insID=@insID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@insID", ID);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            egitmenbilgi.insID = "" + reader.GetInt32(0);
                            egitmenbilgi.fullName = reader.GetString(1);
                            egitmenbilgi.email = reader.GetString(2);
                            egitmenbilgi.alan = reader.GetString(3);
                            egitmenbilgi.telefon = "" + reader.GetInt32(4);
                            egitmenbilgi.userRole = reader.GetString(5);
                            egitmenbilgi.sifre = reader.GetString(6);

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
            egitmenbilgi.insID = Request.Form["ID"];
            egitmenbilgi.fullName = Request.Form["fullName"].ToString();
            egitmenbilgi.email = Request.Form["email"].ToString();
            egitmenbilgi.alan = Request.Form["alan"].ToString();
            egitmenbilgi.telefon = Request.Form["telefon"];
            egitmenbilgi.userRole = Request.Form["userRole"].ToString();
            egitmenbilgi.sifre = Request.Form["sifre"].ToString();

            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "update Instructor set fullName=@fullName, email=@email, alan=@alan, telefon=@telefon, userRole=@userRole,sifre=@sifre where insID=@insID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@fullName", egitmenbilgi.fullName);
                        command.Parameters.AddWithValue("@email", egitmenbilgi.email);
                        command.Parameters.AddWithValue("@alan", egitmenbilgi.alan);
                        command.Parameters.AddWithValue("@telefon", egitmenbilgi.telefon);
                        command.Parameters.AddWithValue("@userRole", egitmenbilgi.userRole);
                        command.Parameters.AddWithValue("@sifre", egitmenbilgi.sifre);
                        command.Parameters.AddWithValue("@insID", egitmenbilgi.insID);

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {

            }

            Response.Redirect("/Instructor/Index");
        }
    }
}
