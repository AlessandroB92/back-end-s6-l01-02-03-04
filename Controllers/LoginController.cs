using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Azure.Identity;
using back_end_s6_l01_02_03_04.Models;

namespace back_end_s6_l01_02_03_04.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logged");
            }
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var command = new SqlCommand("SELECT * FROM Admin WHERE Username = @username AND Password = @password", conn);
                        command.Parameters.AddWithValue("@username", admin.Username);
                        command.Parameters.AddWithValue("@password", admin.Password);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                FormsAuthentication.SetAuthCookie(reader["AdminID"].ToString(), true);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                // Imposta un messaggio di errore e reindirizza alla pagina di login
                                TempData["ErrorMessage"] = "Credenziali errate. Riprova.";
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Gestisci l'errore in modo appropriato, ad esempio, registralo o mostra un messaggio all'utente
                    ModelState.AddModelError("", "Si è verificato un errore durante il login.");
                    return View(admin);
                }
            }
            return View(admin);
        }

        [Authorize]
        public ActionResult Logged()
        {
            var adminID = HttpContext.User.Identity.Name;
            ViewBag.AdminID = adminID;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}