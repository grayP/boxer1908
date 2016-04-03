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
        [AllowAnonymous]
        public ActionResult Index(int? RegattaID, string DocumentType)
        {

            DocumentViewModel dvm = new DocumentViewModel()
            {
                RegattaID = (RegattaID.HasValue) ?  Convert.ToInt32(RegattaID):0,
                DocumentType = (DocumentType == null) ?  "all": DocumentType
            };
            dvm.HandleRequest();

            return View(dvm);
        }
        [AllowAnonymous]
        public ActionResult show(int? RegattaID, string DocumentType)
        {

            DocumentViewModel dvm = new DocumentViewModel()
            {
                RegattaID = (RegattaID.HasValue) ? Convert.ToInt32(RegattaID) : 0,
                DocumentType = "log"
            };
            dvm.HandleRequest();

            return View(dvm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(DocumentViewModel dvm)
        {
            dvm.IsValid = ModelState.IsValid;
            dvm.Entity.DocumentText = dvm.DocumentText;
            dvm.Entity.DocumentType = dvm.DocumentType;
            dvm.Entity.DocumentAuthor = User.Identity.Name ?? "Guest";
            dvm.Entity.DocumentDateTime = DateTime.Now;

            dvm.SearchEntity.DocumentType = dvm.DocumentType;

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