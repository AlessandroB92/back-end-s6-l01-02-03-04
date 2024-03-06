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
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
            var conn = new SqlConnection(connectionString);
            if (ModelState.IsValid) //per rendere i controlli dei model funzionanti
            {
                try
                {
                    conn.Open();
                    var command = new SqlCommand("SELECT * FROM Admin WHERE Username = @username AND Password = @password", conn);
                    command.Parameters.AddWithValue("@username", admin.Username);
                    command.Parameters.AddWithValue("@password", admin.Password);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        FormsAuthentication.SetAuthCookie(reader["AdminID"].ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                    else //se il reader non ha rows, la select è andata a vuoto e quindi il database non riconosce l'utente
                         //quindi ridireziona su errore perchè hai sbagliato qualche dato
                    {
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logged");
                }
                finally
                {
                    conn.Close();
                }
            }
            return View();

        }

        [Authorize]

        public ActionResult Logged()
        {
            var AdminID = HttpContext.User.Identity.Name;
            ViewBag.AdminID = AdminID;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // sloggare l'utente
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");

        }
    }
}