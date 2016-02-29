using DataRepository.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bw01.Controllers
{
    public class CrewController : Controller
    {
        // GET: Regatta
        public ActionResult Index()
        {
            CrewMemberViewModel cvm = new CrewMemberViewModel();
            cvm.HandleRequest();

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Index(CrewMemberViewModel cvm)
        {
            cvm.IsValid = ModelState.IsValid;
            cvm.HandleRequest();

            if (cvm.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in cvm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

            }

            return View(cvm);
        }


       
    }
}
