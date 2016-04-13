using DataRepository.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace bw01.Controllers
{
    public class CrewController : Controller
    {
        // GET: Regatta
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            CrewMemberViewModel cvm = new CrewMemberViewModel();
            cvm.HandleRequest();
            return View(cvm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        [ChildActionOnly]
        public ActionResult ShowSearchForm(CrewMemberViewModel cvm)
        {
            if (Roles.IsUserInRole("Admin"))
            {
                return PartialView("~/Views/Crew/partial/_CrewMenu.cshtml",cvm);
            }
            return null;
        }

        [ChildActionOnly]
        public ActionResult ShowTableData(CrewMemberViewModel cvm)
        {
            if (Roles.IsUserInRole("Admin"))
            {
                return PartialView("~/Views/Crew/partial/_CrewData.cshtml", cvm);
            }
            return PartialView("~/Views/Crew/partial/_CrewDataOpen.cshtml", cvm); ;
        }

    }
}
