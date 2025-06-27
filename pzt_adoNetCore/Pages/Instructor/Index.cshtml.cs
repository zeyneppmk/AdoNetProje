using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pzt_adoNetCore.Pages.Users;

namespace pzt_adoNetCore.Pages.Instructor
{
	public class IndexModel : PageModel
    {
        public List<Egitmen> listele = new List<Egitmen>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost;Database=Egitim;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from Instructor";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Egitmen islem = new Egitmen();
                        islem.insID = "" + reader.GetInt32(0);
                        islem.fullName = reader.GetString(1);
                        islem.email = reader.GetString(2);
                        islem.alan = reader.GetString(3);
                        islem.telefon = "" + reader.GetInt32(4);
                        islem.userRole = reader.GetString(5);
                        islem.sifre = reader.GetString(6);
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
