using ImageStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DataRepository.Models
{
    public class HeadingManager
    {

        public HeadingManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();


        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }





        public List<MainHeading> Get(MainHeading Entity)
        {
            List<MainHeading> ret = new List<MainHeading>();
            using (boxerdb db = new boxerdb())
            {
                ret = db.MainHeadings.OrderBy(x=>Guid.NewGuid()).Take(1).ToList<MainHeading>();
            }
          
            return ret;
        }

        public MainHeading Find(int HeadingID)
        {
            MainHeading ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.MainHeadings.Find(HeadingID);
            }
            return ret;

        }

        public bool Validate(MainHeading entity)
        {
            ValidationErrors.Clear();

            if (!string.IsNullOrEmpty(entity.Heading))
            {
                //if (entity.Caption.ToLower() == entity.Caption)
                //{
                //    ValidationErrors.Add(new KeyValuePair<string, string>("Caption", "Caption cannot be all lower case"));
                //}

            }
            return (ValidationErrors.Count == 0);

        }

      

        public Boolean  Update(MainHeading HeadingtoUpdate)
        {
            bool ret = false;
            if (Validate(HeadingtoUpdate))
            {
                try
                {
                    MainHeading entity = new MainHeading() { 
                        Heading = HeadingtoUpdate.Heading,
                        Public = HeadingtoUpdate.Public
                    };


                    using (boxerdb db = new boxerdb())
                    {
                        db.MainHeadings.Attach(entity);
                        var modifiedHeading = db.Entry(entity);
                        modifiedHeading.Property(e => e.Heading).IsModified = true;
                        modifiedHeading.Property(e => e.Public).IsModified = true;
                        
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


        public Boolean Insert(MainHeading headingtoUpload)
        {
            bool ret = false;
            try
            {
                MainHeading entity = new MainHeading();
                entity.Heading = headingtoUpload.Heading;
                entity.Public = headingtoUpload.Public;
         
                ret = Validate(entity);
                if (ret)
                {
                     using (boxerdb db = new boxerdb())
                    {
  
                        db.MainHeadings.Add(entity);
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


        public bool Delete(MainHeading entity)
        {
            bool ret = false;
            using (boxerdb db = new boxerdb())
            {
                db.MainHeadings.Attach(entity);
                db.MainHeadings.Remove(entity);
                db.SaveChanges();
                ret = true;
            }
            return ret;

        }
    }
}
