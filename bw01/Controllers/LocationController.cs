using System;
using System.Web.Mvc;
using DataRepository.Models;

namespace bw01.Controllers
{
    public class LocationController : Controller
    {

        private LocationViewModel lcm; 
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
        [HttpPost]
        public ActionResult Add(Decimal Lat, Decimal Long, DateTime ReadingDateTime, Single heading, Single speed)
        {

            try
            {
                lcm = new LocationViewModel();
                lcm.Mode = "add";
                lcm.Entity.Latitude = Lat;
                lcm.Entity.Longitude = Long;
                lcm.Entity.ReadingDateTime = ReadingDateTime;


                Console.WriteLine("Lat=" + Lat);
                Console.WriteLine("Long=" + Long);


                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return View();
            }
        }
    }
}