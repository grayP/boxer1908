using System;
using System.Web.Mvc;
using DataRepository;
using DataRepository.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Globalization;

namespace bw01.Controllers
{
    public class LocationController : Controller
    {

        private LocationViewModel lcm = new LocationViewModel();
        // GET: Location
        [AllowAnonymous]
        public ActionResult Index()
        {
            lcm.Mode = "List";
            lcm.HandleRequest();

            return View(lcm);
        }


        public ActionResult show()
        {
            Location lastLoc = new Location();
            LocationManager lm = new LocationManager();

            lastLoc = lm.Latest();

            return View(lastLoc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public void Add(Decimal Lat, Decimal Long, DateTime? ReadingDateTime)
        {
            try
            {
                DateTime reading = ReadingDateTime ?? DateTime.Now;
               
                LocationManager lm = new LocationManager();
                Location newLoc = new Location()
                {
                    Latitude = Lat,
                    Longitude = Long,
                    ReadingDateTime = reading ,
                   // Phone = User.Identity.GetUserId() ?? "Guest"
                    Phone = reading.ToString("dd MMM HH:mm", CultureInfo.InvariantCulture)
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
            MarkerList markers = GetMarkersObjects();
            return Json(markers, JsonRequestBehavior.AllowGet);
        }


        public static MarkerList GetMarkersObjects()
        {
            LocationViewModel lv = new LocationViewModel();
            lv.Mode = "List";
            lv.SearchEntity.Take = 3;
            lv.HandleRequest();

            List<Marker> _markers = new List<Marker>();


            foreach (Location item in lv.locations)
            {
                _markers.Add(new Marker()
                {
                    lat = item.Latitude.ToString(),
                    lng = item.Longitude.ToString(),
                    html = item.Phone.ToString(),
                    label = item.Id.ToString()
                });

            }

          return new MarkerList { markers = _markers };
        }


    }
}