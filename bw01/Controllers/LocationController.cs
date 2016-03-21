using System;
using System.Web.Mvc;
using DataRepository;
using DataRepository.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace bw01.Controllers
{
    public class LocationController : Controller
    {

        private LocationViewModel lcm=new LocationViewModel(); 
        // GET: Location
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            lcm.Mode="List";
            lcm.HandleRequest();
            
            return View(lcm);
        }


        public ActionResult show()
        {
            lcm.Mode = "List";
            lcm.HandleRequest();

            return View(lcm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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


        public ActionResult GetMarkers()
        {
           // MarkerList markers = GetMarkersObjects();
            lcm.Mode = "List";
            lcm.SearchEntity.Phone = "gray";
            lcm.HandleRequest();

            foreach (var location in lcm.locations)
            {
                lcm.markers.Add(new Marker()
                {
                   latitude=location.Latitude,
                   longitude=location.Longitude,
                   phone=location.Phone,
                   ID=location.Id
                });
            }


            return Json(lcm.markers, JsonRequestBehavior.AllowGet);
        }

     

    }
}