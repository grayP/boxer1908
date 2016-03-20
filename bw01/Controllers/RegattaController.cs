using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository.Models;

namespace bw01.Controllers
{
    public class RegattaController : Controller
    {
        // GET: Regatta
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            RegattaViewModel rvm = new RegattaViewModel();
             rvm.HandleRequest();
            
            return View(rvm);
        }

        public ActionResult show()
        {
            RegattaViewModel rvm = new RegattaViewModel();
            rvm.HandleRequest();

            return View(rvm);
        }

        [HttpPost]
        public ActionResult Index(RegattaViewModel rvm)
        {
            rvm.IsValid = ModelState.IsValid;
            rvm.HandleRequest();

            if (rvm.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string,string>  item in rvm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

            }

            return View(rvm);
        }

    }
}