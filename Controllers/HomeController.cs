using GlossaryOfTerms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlossaryOfTerms.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGlossaries()
        {
            using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
            {
                var glossaries = dc.Glossaries.OrderBy(a => a.Term).ToList();
                return Json(new { data = glossaries }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}