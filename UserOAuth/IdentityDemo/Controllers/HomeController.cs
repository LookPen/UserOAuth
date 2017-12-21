using IdentityDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (MSwartzContext context = new MSwartzContext())
            {
                var users = context.Users.Include(u => u.Claims)
                            .Include(u => u.Logins)
                            .Include(u => u.Roles)
                            .ToList();

                if (users.Count > 0)
                { }
            }
            return View();
        }
    }
}