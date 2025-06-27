using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pzt_adoNetCore.Pages.Rapor
{
	public class IndexModel : PageModel
    {
        public int OgrenciSayisi { get; set; }
        public int KursSayisi { get; set; }
        public int EgitmenSayisi { get; set; }


        public Dictionary<string, int> EgitmenAlanDagilimi { get; set; } = new();
        public Dictionary<string, int> KursSureDagilimi { get; set; } = new();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=localhost; Database=Egitim; User Id=SA; Password=reallyStrongPwd123; TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Userlar", connection);
                    OgrenciSayisi = (int)cmd1.ExecuteScalar();

                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Courses", connection);
                    KursSayisi = (int)cmd2.ExecuteScalar();

                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Instructor", connection);
                    EgitmenSayisi = (int)cmd3.ExecuteScalar();

                    //Eğitmen Alan Dağılımı
                    SqlCommand cmd4 = new SqlCommand("SELECT alan, COUNT(*) FROM Instructor GROUP BY alan", connection);
                    SqlDataReader reader1 = cmd4.ExecuteReader();
                    while (reader1.Read())
                    {
                        string alan = reader1.GetString(0);
                        int sayi = reader1.GetInt32(1);
                        EgitmenAlanDagilimi.Add(alan, sayi);
                    }
                    reader1.Close();

                    //Kurs Süre Dağılımı
                    SqlCommand cmd5 = new SqlCommand("SELECT time, COUNT(*) FROM Courses GROUP BY time", connection);
                    SqlDataReader reader2 = cmd5.ExecuteReader();
                    while (reader2.Read())
                    {
                        string time = reader2.GetString(0);
                        int sayi = reader2.GetInt32(1);
                        KursSureDagilimi.Add(time, sayi);
                    }
                    reader2.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
    }
}
