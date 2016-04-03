using DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository;



namespace bw01.Controllers
{
    public class RegattaCrewController : Controller
    {
        // GET: RegattaCrew
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int RegattaID, string RegattaName)
        {
            RegattaCrewViewModel rcm = new RegattaCrewViewModel();
            rcm.regatta.Id = RegattaID;
            rcm.regatta.RegattaName = RegattaName;
            rcm.LoadData();
            return View(rcm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(RegattaCrewViewModel model)
        {
            RegattaCrewManager rcm = new RegattaCrewManager();
            List<CrewMemberSelect> crew = model.AllCrew;

            foreach (CrewMemberSelect crewMember in crew)
            {
                rcm.UpdateCrewList(crewMember, model.regatta);
            }
            return RedirectToAction("Index","regatta");

        }


    }
}