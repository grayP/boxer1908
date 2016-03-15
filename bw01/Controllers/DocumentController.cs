using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository.Models;

namespace bw01.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Regatta
        public ActionResult Index()
        {
            DocumentViewModel dvm = new DocumentViewModel();
            
            dvm.HandleRequest();

            return View(dvm);
        }

        [HttpPost]
        public ActionResult Index(DocumentViewModel dvm)
        {
            dvm.IsValid = ModelState.IsValid;
            dvm.HandleRequest();

            if (dvm.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in dvm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

            }

            return View(dvm);
        }

    }
}