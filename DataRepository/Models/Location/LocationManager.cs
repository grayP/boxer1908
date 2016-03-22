using System;
using System.Collections.Generic;
using System.Linq;

namespace DataRepository.Models
{


    public class LocationManager
    {

        public LocationManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();

          

        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }


       


        public List<Location> Get(Location Entity)
        {
            List<Location> ret = new List<Location>();
            using (boxerdb db = new boxerdb())
            {
                ret = db.Locations.Take(Entity.Take).OrderByDescending(x=>x.ReadingDateTime).ToList<Location>();
                
            }

          

            if (!string.IsNullOrEmpty(Entity.Phone))
            {
                ret = ret.FindAll(p => p.Phone.ToLower().StartsWith(Entity.Phone));
            }


            return ret;
        }

        public Location Find(int LocationID)
        {
            Location ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.Locations.Find(LocationID);
            }
            return ret;

        }


        public Location Latest()
        {
            Location ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.Locations.OrderByDescending(x => x.ReadingDateTime).First();
            }
            return ret;

        }

        public bool Validate(Location entity)
        {
            ValidationErrors.Clear();

            if (!string.IsNullOrEmpty(entity.Phone))
            {
                //if (entity.RegattaName.ToLower() == entity.RegattaName)
                //{
                //    ValidationErrors.Add(new KeyValuePair<string, string>("Regatta Name", "Regatta Name cannot be all lower case"));
                //}

            }
            return (ValidationErrors.Count == 0);

        }


        public Boolean Update(Location entity)
        {
            bool ret = false;
            if (Validate(entity))
            {
                try
                {
                    using (boxerdb db = new boxerdb())
                    {
                        db.Locations.Attach(entity);
                        var modifiedLocation = db.Entry(entity);
                        modifiedLocation.Property(e => e.Latitude).IsModified = true;
                        modifiedLocation.Property(e => e.Longitude).IsModified = true;
                        modifiedLocation.Property(e => e.ReadingDateTime).IsModified = true;
                        modifiedLocation.Property(e => e.Phone).IsModified = true;
                        db.SaveChanges();
                        ret = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    ret = false;
                }
            }

            return ret;
        }

        public Boolean Insert(Location entity)
        {
            bool ret = false;
            try
            {
                ret = Validate(entity);
                if (ret)
                {
                    using (boxerdb db = new boxerdb())
                    {
                        Location newLocation = new Location()
                        {
                            Longitude = entity.Longitude,
                            Latitude = entity.Latitude,
                            ReadingDateTime = entity.ReadingDateTime,
                            Phone = entity.Phone
                        };

                        db.Locations.Add(newLocation);
                        db.SaveChanges();
                        ret = true;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return ret;
            }

        }


        public bool Delete(Location entity)
        {
            bool ret = false;
            using (boxerdb db = new boxerdb())
            {
                db.Locations.Attach(entity);
                db.Locations.Remove(entity);
                db.SaveChanges();
                ret = true;
            }
            return ret;

        }
    }
}
