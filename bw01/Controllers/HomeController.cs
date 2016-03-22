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
      
        public ActionResult Index()
        {
            HeadingViewModel hvm = new HeadingViewModel();
            hvm.HandleRequest();
            return View(hvm.Headings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}