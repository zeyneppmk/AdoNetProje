using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pzt_adoNetCore.Pages.Courses
{
	public class CreateModel : PageModel
    {

        public Kurslar kursBilgi = new Kurslar();
        public string ErrorMessage = "";
        public string SuccesMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            kursBilgi.title = Request.Form["title"].ToString();
            kursBilgi.time = Request.Form["time"].ToString();
            kursBilgi.description = Request.Form["description"].ToString();

            if (kursBilgi.title.Length == 0 || kursBilgi.time.Length == 0 || kursBilgi.description.Length == 0)
            {
                ErrorMessage = "Tüm Alanlara Veri Girilmelidir!";
                return;
            }
            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "insert into Courses(title,time,description)values(@title,@time,@description)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@title", kursBilgi.title);
                        command.Parameters.AddWithValue("@time", kursBilgi.time);
                        command.Parameters.AddWithValue("@description", kursBilgi.description);
                        command.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            kursBilgi.title = "";
            kursBilgi.time = "";
            kursBilgi.description = "";
            SuccesMessage = "Kayıt Basarili";
            Response.Redirect("/Courses/Index");
        }
    }
}
