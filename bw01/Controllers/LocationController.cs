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
            MarkerList markers = GetMarkersObjects();
           

            return Json(markers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the markers. This data could be filled with whatever you 
        /// set from your DB.
        /// </summary>
        /// <returns></returns>
        public static MarkerList GetMarkersObjects()
        {
                LocationViewModel lv = new LocationViewModel();
                lv.Mode = "List";
                lv.HandleRequest();

            List<Marker> _markers = new List<Marker>();


            foreach (Location item in lv.locations)
            {
                _markers.Add(new Marker()
                {
                    lat = item.Latitude.ToString(),
                    lng = item.Latitude.ToString(),
                    html = item.Phone.ToString(),
                    label = item.Id.ToString()
                });

            }



            //Marker marker = new Marker
            //{
            //    html = "Some stuff to display in the<br>First Info Window",
            //    lat = "51.4109278",
            //    lng = "-0.2091921",
            //    label = "Marker One"
            //};

            //Marker marker2 = new Marker
            //{
            //    html = "Some stuff to display in the<br>Second Info Window",
            //    lat = "55.084",
            //    lng = "-1.82",
            //    label = "Marker Two"
            //};

            //Marker marker3 = new Marker
            //{
            //    html = "Some stuff to display in the<br>Third Info Window",
            //    lat = "53.08",
            //    lng = "-1.35",
            //    label = "Marker Three"
            //};


            //return new MarkerList { markers = new List<Marker> { marker, marker2, marker3 } };
            return new MarkerList { markers=_markers};
        }


    }
}