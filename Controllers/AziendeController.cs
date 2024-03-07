using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using back_end_s6_l01_02_03_04.Models;


namespace back_end_s6_l01_02_03_04.Controllers
{        // GET: Aziende
        public class AziendeController : Controller
        {
            public ActionResult Index()
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewBag.ErrorMessage = "Devi effettuare il login per registrare un Azienda.";
                    return View("Error");
                }
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Index(Aziende azienda)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
                var conn = new SqlConnection(connectionString);
                if (ModelState.IsValid)
                {
                    try
                    {
                        conn.Open();
                        var command = new SqlCommand("INSERT INTO Aziende (PartitaIVA, RagioneSociale, Indirizzo, Email) VALUES (@PartitaIVA, @RagioneSociale, @Indirizzo, @Email)", conn);
                        command.Parameters.AddWithValue("@PartitaIVA", azienda.PartitaIVA);
                        command.Parameters.AddWithValue("@RagioneSociale", azienda.RagioneSociale);
                        command.Parameters.AddWithValue("@Indirizzo", azienda.Indirizzo);
                        command.Parameters.AddWithValue("@Email", azienda.Email);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
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
