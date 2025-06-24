using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace pzt_adoNetCore.Pages.Users
{
	public class IndexModel : PageModel
    {
        public List<User> listele = new List<User>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost;Database=Egitim;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from Userlar";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User islem = new User();
                        islem.ID = "" + reader.GetInt32(0);
                        islem.fullName = reader.GetString(1);
                        islem.email = reader.GetString(2);
                        islem.telefon =""+ reader.GetInt32(3);
                        islem.userRole = reader.GetString(4);
                        islem.sifre = reader.GetString(4);

                        listele.Add(islem);
                    }
                }
            }
            catch (Exception ex)
            {
            
            }
        }
    }
}
