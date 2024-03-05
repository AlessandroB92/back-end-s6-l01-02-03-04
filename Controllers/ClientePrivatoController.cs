using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using back_end_s6_l01_02_03_04.Models;

namespace back_end_s6_l01_02_03_04.Controllers
{
    public class ClientePrivatoController : Controller
    {
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ClientePrivato cliente)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO ClientiPrivati (CodiceFiscale, Nome, Cognome, Indirizzo, Email) VALUES (@Nome, @Cognome, @Email, @Indirizzo, @CodiceFiscale)", conn);
                    command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    command.Parameters.AddWithValue("@Indirizzo", cliente.Indirizzo);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.ExecuteNonQuery();
                }

                return RedirectToAction("Index", "Home"); // Redirect alla home page dopo aver aggiunto il cliente
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Si è verificato un errore durante l'aggiunta del cliente.";
                return View(cliente); // Ritorna alla vista con il modello in caso di errore
            }
        }
    }
}