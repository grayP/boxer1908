using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class RegattaManager
    {

        public RegattaManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();

            //  List<tblRegatta> _regattas = new List<tblRegatta>();

        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }





        public List<tblRegatta> Get(tblRegatta Entity)
        {
            List<tblRegatta> ret = new List<tblRegatta>();
            using (boxerdb db = new boxerdb())
            {
                ret = db.tblRegattas.OrderBy(x=>x.StartDate).ToList<tblRegatta>();
            }

            if (!string.IsNullOrEmpty(Entity.RegattaName))
            {
                ret = ret.FindAll(p => p.RegattaName.ToLower().StartsWith(Entity.RegattaName));
            }


            return ret;
        }

        public tblRegatta Find(int RegattaID)
        {
            tblRegatta ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.tblRegattas.Find(RegattaID);
            }
            return ret;

        }

        public bool Validate(tblRegatta entity)
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


        public Boolean Update(tblRegatta entity)
        {
            bool ret = false;
            if (Validate(entity))
            {
                try
                {
                    using (boxerdb db = new boxerdb())
                    {
                        db.tblRegattas.Attach(entity);
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

        public Boolean Insert(tblRegatta entity)
        {
            bool ret = false;
            try
            {
                ret = Validate(entity);
                if (ret)
                {
                    using (boxerdb db = new boxerdb())
                    {
                        tblRegatta NewRegatta = new tblRegatta()
                        {
                            RegattaName = entity.RegattaName,
                            RegattaYear = entity.RegattaYear,
                            Result = entity.Result,
                            StartDate = entity.StartDate
                        };

                        db.tblRegattas.Add(NewRegatta);
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

    }
}
