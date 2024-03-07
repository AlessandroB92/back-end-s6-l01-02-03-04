using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using back_end_s6_l01_02_03_04.Models;

namespace back_end_s6_l01_02_03_04.Controllers
{
    public class ClientiPrivatiController : Controller
    {
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "Devi effettuare il login per registrare un cliente.";
                return View("Error");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ClientiPrivati cliente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
            var conn = new SqlConnection(connectionString);
            if (ModelState.IsValid)
            {
                try
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO ClientiPrivati (CodiceFiscale, Nome, Cognome, Indirizzo, Email) VALUES (@CodiceFiscale, @Nome, @Cognome, @Indirizzo, @Email)", conn);
                    command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                    command.Parameters.AddWithValue("@Indirizzo", cliente.Indirizzo);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Errore nella compilazione dei campi.";
                    return View("Error");
                }
                finally { conn.Close(); }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}