using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class RegattaCrewManager
    {

        public RegattaCrewManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();

        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }



        public List<int?> Get(Regatta Entity)
        {
            int RegattaID = Entity.Id;
            using (boxerdb db = new boxerdb())
            {
                var query = (from c in db.RegattaCrews
                             where c.RegattaId == RegattaID
                             select c.CrewMemberId );
                return query.ToList();
            }
        }


        public List<CrewMemberSelect> GetAllCrew()
        {
            using (boxerdb db = new boxerdb())
            {
                var query = (from c in db.CrewMembers
                             select new CrewMemberSelect() {Id=c.Id, Name=c.CrewName });
                return query.ToList();
            }
        }





        public Regatta Find(int RegattaID)
        {
            Regatta ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.Regattas.Find(RegattaID);
            }
            return ret;

        }

        public bool Validate(Regatta entity)
        {
            ValidationErrors.Clear();

            if (!string.IsNullOrEmpty(entity.RegattaName))
            {
                if (entity.RegattaName.ToLower() == entity.RegattaName)
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("Regatta Name", "Regatta Name cannot be all lower case"));
                }

            }
            return (ValidationErrors.Count == 0);

        }

       

        public Boolean Update(Regatta entity)
        {
            bool ret = false;
            if (Validate(entity))
            {
                try
                {
                    using (boxerdb db = new boxerdb())
                    {
                        db.Regattas.Attach(entity);
                        var modifiedRegatta = db.Entry(entity);
                        modifiedRegatta.Property(e => e.RegattaName).IsModified = true;
                        modifiedRegatta.Property(e => e.RegattaYear).IsModified = true;
                        modifiedRegatta.Property(e => e.Result).IsModified = true;
                        modifiedRegatta.Property(e => e.StartDate).IsModified = true;
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

        public Boolean Insert(Regatta entity)
        {
            bool ret = false;
            try
            {
                ret = Validate(entity);
                if (ret)
                {
                    using (boxerdb db = new boxerdb())
                    {
                        Regatta NewRegatta = new Regatta()
                        {
                            RegattaName = entity.RegattaName,
                            RegattaYear = entity.RegattaYear,
                            Result = entity.Result,
                            StartDate = entity.StartDate
                        };

                        db.Regattas.Add(NewRegatta);
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


        public bool Delete(Regatta entity)
        {
            bool ret = false;
            using (boxerdb db = new boxerdb())
            {
                db.Regattas.Attach(entity);
                db.Regattas.Remove(entity);
                db.SaveChanges();
                ret = true;
            }
            return ret;

        }
    }
}
