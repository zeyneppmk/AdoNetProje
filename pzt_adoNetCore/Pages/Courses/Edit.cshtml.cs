using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pzt_adoNetCore.Pages.Instructor;

namespace pzt_adoNetCore.Pages.Courses
{
	public class EditModel : PageModel
    {
        public Kurslar kursbilgi = new Kurslar();

        public void OnGet()
        {
            string ID = Request.Query["id"];


            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string sql = "select * from Courses where coursId=@courseId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@courseId", ID);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            kursbilgi.coursId = "" + reader.GetInt32(0);
                            kursbilgi.title = reader.GetString(1);
                            kursbilgi.time = reader.GetString(2);
                            kursbilgi.description = reader.GetString(3);
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
            kursbilgi.coursId = Request.Form["ID"];
            kursbilgi.title = Request.Form["title"].ToString();
            kursbilgi.time = Request.Form["time"].ToString();
            kursbilgi.description = Request.Form["description"].ToString();

            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "update Courses set title=@title, time=@time, description=@description where coursId=@coursId";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", kursbilgi.title);
                        command.Parameters.AddWithValue("@time", kursbilgi.time);
                        command.Parameters.AddWithValue("@description", kursbilgi.description);
                        command.Parameters.AddWithValue("@coursID", kursbilgi.coursId);
                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {

            }

            Response.Redirect("/Courses/Index");
        }
    }
}
