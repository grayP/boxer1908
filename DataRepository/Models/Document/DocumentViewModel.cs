using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class DocumentViewModel: BaseModel.ViewModelBase
    {

        public DocumentViewModel(): base()
        {
            
        }
        
        //Properties--------------
        public List<Document> documents { get; set; }
        public Document SearchEntity { get; set; }
        public Document Entity { get; set; }
        public IList<RegattaSelectItem> RegattaList
        {
            get
            {
                using (boxerdb db = new boxerdb())
                {
                    var SelectList = (from item in
                                      db.Regattas.OrderBy(x => x.StartDate)
                                      select new RegattaSelectItem()
                                      {
                                          RegattaID = item.Id,
                                          Regatta = item.RegattaName
                                      }).ToList();

                    return SelectList;
                }

            }


            set { }
        }





        //---------------------------------------------------------------
        protected override void Init()
        {
          documents = new List<Document>();
          SearchEntity = new Document();
          Entity = new Document();
          base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new Document();
        }

        protected override void Get()
        {
            DocumentManager dm = new DocumentManager();
            documents = dm.Get(SearchEntity);
        }

       protected override void Edit()
        {
            DocumentManager dm = new DocumentManager();
            Entity =dm.Find(Convert.ToInt32(EventArgument));
            base.Edit();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new Document();
            Entity.DocumentType = "log";
            base.Add();
        }


        protected override void Save()
        {
            DocumentManager dm = new DocumentManager();
            if (Mode == "Add")
            {
                dm.Insert(Entity);
            }
            else
            {
                dm.Update(Entity);
            }
            ValidationErrors = dm.ValidationErrors;

            base.Save();
        }

        protected override void Delete()
        {
            DocumentManager dm = new DocumentManager();
            Entity = dm.Find(Convert.ToInt32(EventArgument));
            dm.Delete(Entity);
            Get();
            base.Delete();

        }


    }
}
