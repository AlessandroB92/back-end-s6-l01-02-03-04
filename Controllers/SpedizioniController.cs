using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using back_end_s6_l01_02_03_04.Models;

namespace back_end_s6_l01_02_03_04.Controllers
{
    public class SpedizioniController : Controller
    {
        public ActionResult Index()
        {
            List<ClientiPrivati> clienti = new List<ClientiPrivati>();
            List<Aziende> aziende = new List<Aziende>();
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "Devi effettuare il login per registrare una Spedizione.";
                return View("Error");
            }
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string queryCliente = "SELECT ClientePrivatoID, Cognome, Nome FROM ClientiPrivati";
                    SqlCommand commandCliente = new SqlCommand(queryCliente, conn);
                    SqlDataReader readerCliente = commandCliente.ExecuteReader();

                    while (readerCliente.Read())
                    {
                        ClientiPrivati cliente = new ClientiPrivati
                        {
                            ClientePrivatoID = readerCliente.GetInt32(0),
                            Cognome = readerCliente.GetString(1),
                            Nome = readerCliente.GetString(2),
                        };

                        clienti.Add(cliente);
                    }
                    readerCliente.Close();

                    string queryAziende = "SELECT AziendaID, RagioneSociale FROM Aziende";
                    SqlCommand commandTipiViolazione = new SqlCommand(queryAziende, conn);
                    SqlDataReader readerTipiViolazione = commandTipiViolazione.ExecuteReader();

                    while (readerTipiViolazione.Read())
                    {
                        Aziende azienda = new Aziende
                        {
                            AziendaID = readerTipiViolazione.GetInt32(0),
                            RagioneSociale = readerTipiViolazione.GetString(1)
                        };

                        aziende.Add(azienda);
                    }
                    readerTipiViolazione.Close();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

            ViewBag.ClientiPrivati = new SelectList(clienti, "ClientePrivatoID", "Nome");
            ViewBag.Aziende = new SelectList(aziende, "AziendaID", "RagioneSociale");
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Spedizione spedizione)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var command = new SqlCommand("INSERT INTO Spedizioni (ClienteID, AziendaID, NumeroSpedizione, DataSpedizione, Peso, CittaDestinataria, IndirizzoDestinatario, NominativoDestinatario, CostoSpedizione, DataConsegnaPrevista, StatoSpedizione) VALUES (@ClienteID, @AziendaID, @NumeroSpedizione, @DataSpedizione, @Peso, @CittaDestinataria, @IndirizzoDestinatario, @NominativoDestinatario, @CostoSpedizione, @DataConsegnaPrevista, @StatoSpedizione)", conn);
                        command.Parameters.AddWithValue("@ClienteID", spedizione.ClienteID);
                        command.Parameters.AddWithValue("@AziendaID", spedizione.AziendaID);
                        command.Parameters.AddWithValue("@NumeroSpedizione", spedizione.NumeroSpedizione);
                        command.Parameters.AddWithValue("@DataSpedizione", spedizione.DataSpedizione);
                        command.Parameters.AddWithValue("@Peso", spedizione.Peso);
                        command.Parameters.AddWithValue("@CittaDestinataria", spedizione.CittaDestinataria);
                        command.Parameters.AddWithValue("@IndirizzoDestinatario", spedizione.IndirizzoDestinatario);
                        command.Parameters.AddWithValue("@NominativoDestinatario", spedizione.NominativoDestinatario);
                        command.Parameters.AddWithValue("@CostoSpedizione", spedizione.CostoSpedizione);
                        command.Parameters.AddWithValue("@DataConsegnaPrevista", spedizione.DataConsegnaPrevista);
                        command.Parameters.AddWithValue("@StatoSpedizione", spedizione.StatoSpedizione);
                        command.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception)
                {
                    ViewBag.ErrorMessage = "Si è verificato un errore durante l'aggiunta della spedizione.";
                    return View(spedizione);
                }
            }

            return View();
        }
    }
}
