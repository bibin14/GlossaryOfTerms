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


        [HttpGet]
        public ActionResult Save(int id)
        {
            using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
            {
                var glossary = dc.Glossaries.Where(a => a.ID == id).FirstOrDefault();
                return View(glossary);
            }

        }

        [HttpPost]
        public ActionResult Save(Glossary gloss)
        {
            if (ModelState.IsValid)
            {
                using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
                {
                    if (gloss.ID > 0)
                    {
                        //Edit
                        var glossary = dc.Glossaries.Where(a => a.ID == gloss.ID).FirstOrDefault();
                        if (glossary != null)
                        {
                            glossary.Term = gloss.Term;
                            glossary.Definition = gloss.Definition;
                        }
                    }
                    else
                    {
                        //Save
                        dc.Glossaries.Add(gloss);

                    }

                    dc.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
            {
                var glossary = dc.Glossaries.Where(a => a.ID == id).FirstOrDefault();
                if (glossary != null)
                {
                    return View(glossary);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteGlossary(int id)
        {
            using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
            {
                var glossary = dc.Glossaries.Where(a => a.ID == id).FirstOrDefault();
                if (glossary != null)
                {
                    dc.Glossaries.Remove(glossary);
                    dc.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }

        }
    }
}