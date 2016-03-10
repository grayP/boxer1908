using ImageStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataRepository.Models
{
    public class ImageManager
    {

        public ImageManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();


        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }





        public List<Image> Get(Image Entity)
        {
            List<Image> ret = new List<Image>();
            using (boxerdb db = new boxerdb())
            {
                ret = db.Images.OrderBy(x=>x.Id).ToList<Image>();
            }

            if (!string.IsNullOrEmpty(Entity.Caption))
            {
                ret = ret.FindAll(p => p.Caption.ToLower().StartsWith(Entity.Caption));
            }


            return ret;
        }

        public Image Find(int ImageID)
        {
            Image ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.Images.Find(ImageID);
            }
            return ret;

        }

        public bool Validate(Image entity)
        {
            ValidationErrors.Clear();

            if (!string.IsNullOrEmpty(entity.Caption))
            {
                if (entity.Caption.ToLower() == entity.Caption)
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("Caption", "Caption cannot be all lower case"));
                }

            }
            return (ValidationErrors.Count == 0);

        }


        public Boolean Update(Image entity)
        {
            bool ret = false;
            if (Validate(entity))
            {
                try
                {
                    using (boxerdb db = new boxerdb())
                    {
                        db.Images.Attach(entity);
                        var modifiedImage = db.Entry(entity);
                        modifiedImage.Property(e => e.Caption).IsModified = true;
                        modifiedImage.Property(e => e.RegattaID).IsModified = true;
                        //modifiedImage.Property(e => e.ImageURL).IsModified = true;
                       
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


        public Boolean Insert(Image entity)
        {
            bool ret = false;
            try
            {
                ret = Validate(entity);
                if (ret)
                {
                    using (boxerdb db = new boxerdb())
                    {
                        Image newImage = new Image()
                        {
                            Caption = entity.Caption,
                            RegattaID = entity.RegattaID,
                            ImageURL = entity.ImageURL ,
                            ThumbNailLarge=entity.ThumbNailLarge,
                            ThumbNailSmall=entity.ThumbNailSmall
                        };

                        db.Images.Add(newImage);
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


        public bool Delete(Image entity)
        {
            bool ret = false;
            using (boxerdb db = new boxerdb())
            {
                db.Images.Attach(entity);
                db.Images.Remove(entity);
                db.SaveChanges();
                ret = true;
            }
            return ret;

        }
    }
}
