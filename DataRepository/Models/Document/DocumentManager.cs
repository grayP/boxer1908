using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class DocumentManager
    {

        public DocumentManager()
        {
            ValidationErrors = new List<KeyValuePair<string, string>>();


        }
        //Properties
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }





        public List<Document> Get(Document Entity)
        {
            List<Document> ret = new List<Document>();
            using (boxerdb db = new boxerdb())
            {
                ret = db.Document.OrderBy(x=>x.Id).ToList<Document>();
            }

            if(Entity.RegattaID.HasValue)
            {
                ret = ret.FindAll(p => p.RegattaID.Equals(Entity.RegattaID));
            }


            if (!string.IsNullOrEmpty(Entity.DocumentAuthor))
            {
                ret = ret.FindAll(p => p.DocumentAuthor.ToLower().StartsWith(Entity.DocumentAuthor));
            }

            
            return ret;
        }

        public Document Find(int docID)
        {
            Document ret = null;
            using (boxerdb db = new boxerdb())
            {
                ret = db.Document.Find(docID);
            }
             return ret;

        }

        public bool Validate(Document entity)
        {
            ValidationErrors.Clear();

            if (!string.IsNullOrEmpty(entity.DocumentAuthor))
            {
                if (String.IsNullOrEmpty(entity.DocumentType))
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("Document Type", "Document Type cannot be null"));
                }

            }
            return (ValidationErrors.Count == 0);

        }


        public Boolean Update(Document entity)
        {
            bool ret = false;
            if (Validate(entity))
            {
                try
                {
                    using (boxerdb db = new boxerdb())
                    {
                        db.Document.Attach(entity);
                        var modifiedDocument = db.Entry(entity);
                        modifiedDocument.Property(e => e.DocumentType).IsModified = true;
                        modifiedDocument.Property(e => e.DocumentText).IsModified = true;
                        modifiedDocument.Property(e => e.Public).IsModified = true;
                        modifiedDocument.Property(e => e.RegattaID).IsModified = true;
                       // modifiedDocument.Property(e => e.DocumentDateTime).IsModified = true;
                       // modifiedDocument.Property(e => e.DocumentAuthor).IsModified = true;
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

        public Boolean Insert(Document entity)
        {
            bool ret = false;
            try
            {
                ret = Validate(entity);
                if (ret)
                {
                    using (boxerdb db = new boxerdb())
                    {
                        Document newDoc = new Document()
                        {
                            DocumentAuthor = entity.DocumentAuthor,
                            DocumentDateTime = entity.DocumentDateTime,
                            DocumentText = entity.DocumentText,
                            DocumentType=entity.DocumentType,
                            Public=entity.Public ,
                            RegattaID=entity.RegattaID
                        };

                        db.Document.Add(newDoc);
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


        public bool Delete(Document entity)
        {
            bool ret = false;
            using (boxerdb db = new boxerdb())
            {
                db.Document.Attach(entity);
                db.Document.Remove(entity);
                db.SaveChanges();
                ret = true;
            }
            return ret;

        }
    }
}
