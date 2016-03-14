using System;
using System.Web.Mvc;
using DataRepository;
using DataRepository.Models;
using Microsoft.AspNet.Identity;

namespace bw01.Controllers
{
    public class LocationController : Controller
    {

        private LocationViewModel lcm=new LocationViewModel(); 
        // GET: Location
        public ActionResult Index()
        {
            lcm.Mode="List";
            lcm.HandleRequest();
            
            return View(lcm);
        }

        [HttpPost]
         public void Add(Decimal Lat, Decimal Long, DateTime? ReadingDateTime)
        {
           try
            {

                LocationManager lm = new LocationManager();
                Location newLoc = new Location()
                {
                    Latitude = Lat,
                    Longitude = Long,
                    ReadingDateTime = (ReadingDateTime ?? DateTime.Now),
                    Phone = User.Identity.GetUserName() ?? "Guest"
                };

                lm.Insert(newLoc);
               // return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
              //  return View();
            }
        }
    }
}