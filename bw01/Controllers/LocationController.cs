using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bw01.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Decimal Lat, Decimal Long)
        {

            try
            {
                Console.WriteLine("Lat=" + Lat);
                Console.WriteLine("Long=" + Long);
             return View();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return View();
            }
        }
    }
}