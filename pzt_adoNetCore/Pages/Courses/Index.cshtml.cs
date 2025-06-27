using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pzt_adoNetCore.Pages.Users;

namespace pzt_adoNetCore.Pages.Courses
{
    public class IndexModel : PageModel
    {
        public List<Kurslar> listele = new List<Kurslar>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Courses";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    {
                        while (reader.Read())
                        {
                            Kurslar x = new Kurslar();
                            x.coursId = "" + reader.GetInt32(0);
                            x.title = reader.GetString(1);
                            x.time = reader.GetString(2);
                            x.description = reader.GetString(3);

                            listele.Add(x);
                        }
                        reader.Close();

                        string searchTerm = Request.Query["arama"].ToString();
                        string sure = Request.Query["sure"].ToString();
                        string sirala = Request.Query["sirala"].ToString();

                        // Debug log (konsolda test için)
                        Console.WriteLine($"arama={searchTerm}, sure={sure}, sirala={sirala}");

                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            listele = listele.Where(x => !string.IsNullOrEmpty(x.title) && x.title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        }

                        if (!string.IsNullOrEmpty(sure))
                        {
                            listele = listele.Where(x => string.Equals(x.time?.Trim(), sure?.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        }

                        if (!string.IsNullOrEmpty(sirala))
                        {
                            if (sirala == "asc")
                                listele = listele.OrderBy(x => x.title).ToList();
                            else if (sirala == "desc")
                                listele = listele.OrderByDescending(x => x.title).ToList();
                        }


                    }

                }

            }

            catch (Exception ex)
            {
                // Hata loglama yapılabilir
                Console.WriteLine("Hata: " + ex.Message);
            }
             
        }

    }
}
