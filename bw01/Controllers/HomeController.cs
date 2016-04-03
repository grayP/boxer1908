using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataRepository.Models;
    
    namespace bw01.Controllers
{

    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            HeadingViewModel hvm = new HeadingViewModel();
            hvm.HandleRequest();
            return View(hvm.Headings);
        }


    }
}