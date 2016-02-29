using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class CrewManager
    {

        public CrewManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();


        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }





        public List<CrewMember> Get(CrewMember Entity)
        {
            List<CrewMember> ret = new List<CrewMember>();
            using (boxerdb db = new boxerdb())
            {
                ret = db.CrewMembers.OrderBy(x=>x.Id).ToList<CrewMember>();
            }

            if (!string.IsNullOrEmpty(Entity.CrewName))
            {
                ret = ret.FindAll(p => p.CrewName.ToLower().StartsWith(Entity.CrewName));
            }


            return ret;
        }

        public CrewMember Find(int CrewID)
        { 
            CrewMember ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.CrewMembers.Find(CrewID);
            }
            return ret;

        }

        public bool Validate(CrewMember entity)
        {
            ValidationErrors.Clear();

            if (!string.IsNullOrEmpty(entity.CrewName))
            {
                if (entity.CrewName.ToLower() == entity.CrewName)
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("Crew Name", "Crew member name cannot be all lower case"));
                }

            }
            return (ValidationErrors.Count == 0);

        }


        public Boolean Update(CrewMember entity)
        {
            bool ret = false;
            if (Validate(entity))
            {
                try
                {
                    using (boxerdb db = new boxerdb())
                    {
                        db.CrewMembers.Attach(entity);
                        var modifiedRegatta = db.Entry(entity);
                        modifiedRegatta.Property(e => e.CrewName).IsModified = true;
                        modifiedRegatta.Property(e => e.CrewPhone).IsModified = true;
                        modifiedRegatta.Property(e => e.CrewEmail).IsModified = true;
                       
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

        public Boolean Insert(CrewMember entity)
        {
            bool ret = false;
            try
            {
                ret = Validate(entity);
                if (ret)
                {
                    using (boxerdb db = new boxerdb())
                    {
                        CrewMember newCrew = new CrewMember()
                        {
                            CrewName = entity.CrewName,
                            CrewEmail = entity.CrewEmail,
                            CrewPhone = entity.CrewPhone
                        };

                        db.CrewMembers.Add(newCrew);
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


        public bool Delete(CrewMember entity)
        {
            bool ret = false;
            using (boxerdb db = new boxerdb())
            {
                db.CrewMembers.Attach(entity);
                db.CrewMembers.Remove(entity);
                db.SaveChanges();
                ret = true;
            }
            return ret;

        }
    }
}
