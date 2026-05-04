using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

namespace ServerServiceAuto
{
    public class Masina
    {
        public int Id { get; set; }
        public string NumarInmatriculare { get; set; }
        public string Marca { get; set; }
        public string Problema { get; set; }
    }

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService1 : System.Web.Services.WebService
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ServiceAutoDB.mdf;Integrated Security=True";

        // --- 1. ADĂUGARE ---
        [WebMethod(Description = "Adaugă o mașină nouă în baza de date")]
        public string AdaugaMasina(string numar, string marca, string problema)
        {
            try
            {
                using (SqlConnection conexiune = new SqlConnection(connectionString))
                {
                    conexiune.Open();
                    string query = "INSERT INTO Masini (NumarInmatriculare, Marca, Problema) VALUES (@numar, @marca, @problema)";

                    using (SqlCommand comanda = new SqlCommand(query, conexiune))
                    {
                        comanda.Parameters.AddWithValue("@numar", numar);
                        comanda.Parameters.AddWithValue("@marca", marca);
                        comanda.Parameters.AddWithValue("@problema", problema);

                        comanda.ExecuteNonQuery();
                    }
                }
                return "Mașină înregistrată cu succes în service!";
            }
            catch (Exception ex)
            {
                return "Eroare la adăugare: " + ex.Message;
            }
        }

        // --- 2. AFIȘARE ---
        [WebMethod(Description = "Returnează lista tuturor mașinilor din service")]
        public List<Masina> ObtineMasini()
        {
            List<Masina> listaMasini = new List<Masina>();

            using (SqlConnection conexiune = new SqlConnection(connectionString))
            {
                conexiune.Open();
                string query = "SELECT * FROM Masini"; // Luăm tot din tabel

                using (SqlCommand comanda = new SqlCommand(query, conexiune))
                {
                    using (SqlDataReader reader = comanda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaMasini.Add(new Masina
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NumarInmatriculare = reader["NumarInmatriculare"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Problema = reader["Problema"].ToString()
                            });
                        }
                    }
                }
            }
            return listaMasini;
        }

        // --- 3. ȘTERGERE ---
        [WebMethod(Description = "Șterge o mașină din baza de date folosind ID-ul")]
        public string StergeMasina(int id)
        {
            try
            {
                using (SqlConnection conexiune = new SqlConnection(connectionString))
                {
                    conexiune.Open();
                    string query = "DELETE FROM Masini WHERE Id = @id";
                    using (SqlCommand comanda = new SqlCommand(query, conexiune))
                    {
                        comanda.Parameters.AddWithValue("@id", id);
                        int randuriAfectate = comanda.ExecuteNonQuery();

                        if (randuriAfectate > 0) return "Mașina a fost ștearsă din sistem!";
                        else return "Nu s-a găsit nicio mașină cu acest ID.";
                    }
                }
            }
            catch (Exception ex) { return "Eroare: " + ex.Message; }
        }

        // --- 4. MODIFICARE ---
        [WebMethod(Description = "Modifică datele unei mașini existente")]
        public string ModificaMasina(int id, string numar, string marca, string problema)
        {
            try
            {
                using (SqlConnection conexiune = new SqlConnection(connectionString))
                {
                    conexiune.Open();
                    string query = "UPDATE Masini SET NumarInmatriculare = @numar, Marca = @marca, Problema = @problema WHERE Id = @id";
                    using (SqlCommand comanda = new SqlCommand(query, conexiune))
                    {
                        comanda.Parameters.AddWithValue("@id", id);
                        comanda.Parameters.AddWithValue("@numar", numar);
                        comanda.Parameters.AddWithValue("@marca", marca);
                        comanda.Parameters.AddWithValue("@problema", problema);

                        int randuriAfectate = comanda.ExecuteNonQuery();
                        if (randuriAfectate > 0) return "Datele mașinii au fost actualizate!";
                        else return "Nu s-a găsit mașina pentru modificare.";
                    }
                }
            }
            catch (Exception ex) { return "Eroare: " + ex.Message; }
        }

        // --- 5. CĂUTARE / FILTRARE ---
        [WebMethod(Description = "Caută mașini după numărul de înmatriculare")]
        public List<Masina> CautaMasina(string numarCautat)
        {
            List<Masina> lista = new List<Masina>();
            using (SqlConnection conexiune = new SqlConnection(connectionString))
            {
                conexiune.Open();
                string query = "SELECT * FROM Masini WHERE NumarInmatriculare LIKE @numar";
                using (SqlCommand comanda = new SqlCommand(query, conexiune))
                {
                    comanda.Parameters.AddWithValue("@numar", "%" + numarCautat + "%");
                    using (SqlDataReader reader = comanda.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Masina
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NumarInmatriculare = reader["NumarInmatriculare"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Problema = reader["Problema"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}