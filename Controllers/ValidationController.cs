using GlossaryOfTerms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace GlossaryOfTerms.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class ValidationController : Controller
    {
        public JsonResult IsTermUnique(Glossary glossary)
        {
            return IsUnique(glossary.ID, glossary.Term, glossary.Definition)
                ? Json(true, JsonRequestBehavior.AllowGet)
                : Json(false, JsonRequestBehavior.AllowGet);
        }

        public bool IsUnique(int id, string term, string def)
        {
            using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
            {
                Glossary glossary;
                if (id > 0)
                {
                    // Edit
                    glossary = dc.Glossaries.Where(a => (a.ID == id)).FirstOrDefault();

                    if (glossary.Term == term && glossary.Definition != def)
                    {
                        //Definition Only Update
                        return true;
                    }
                    else if (glossary.Term == term && glossary.Definition == def)
                    {
                        //Update whileout Changes
                        return true;
                    }
                    else
                    {
                        //Updating Term
                        return IsUnique(term);
                    }
                }
                else
                {
                    //Adding New Term 
                    return IsUnique(term);
                }

            }
        }

        public bool IsUnique(string term)
        {
            using (CSE_DatabaseEntities dc = new CSE_DatabaseEntities())
            {
                var glossary = dc.Glossaries.Where(a => a.Term == term).FirstOrDefault();
                if (glossary == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}