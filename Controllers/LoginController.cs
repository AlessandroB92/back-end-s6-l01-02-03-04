using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using back_end_s6_l01_02_03_04.Models;

namespace back_end_s6_l01_02_03_04.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Add");
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            string connString = ConfigurationManager.ConnectionStrings["BRTDbContext"].ToString();
            var conn = new SqlConnection(connString);
            conn.Open();
            var command = new SqlCommand("SELECT * FROM Admin WHERE Username = @username AND Password = @password", conn);
            command.Parameters.AddWithValue("@username", admin.Username);
            command.Parameters.AddWithValue("@password", admin.Password);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                FormsAuthentication.SetAuthCookie(reader["AdminID"].ToString(), true);
                return RedirectToAction("ClientePrivato", "Login"); ; // TODO: alla pagina di pannello
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Prova()
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

            return RedirectToAction("Index", "Home");

        }
    }
}