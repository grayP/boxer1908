using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class RegattaViewModel: BaseModel.ViewModelBase
    {

        public RegattaViewModel(): base()
        {
            
        }
        
        //Properties--------------
        public List<tblRegatta> regattas { get; set; }
        public tblRegatta SearchEntity { get; set; }
        public tblRegatta Entity { get; set; }
    
        //---------------------------------------------------------------
        protected override void Init()
        {
          regattas = new List<tblRegatta>();
          SearchEntity = new tblRegatta();
          Entity = new tblRegatta();
          base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new tblRegatta();
        }

        protected override void Get()
        {
            RegattaManager rgm = new RegattaManager();
            regattas = rgm.Get(SearchEntity);
        }

       protected override void Edit()
        {
            RegattaManager rgm = new RegattaManager();
            Entity=rgm.Find(Convert.ToInt32(EventArgument));
            base.Edit();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new tblRegatta();
            Entity.RegattaName = "";
            Entity.StartDate = DateTime.Now;
            base.Add();
        }


        protected override void Save()
        {
            RegattaManager rgm = new RegattaManager();
            if (Mode == "Add")
            {
                rgm.Insert(Entity);
            }
            else
            {
                rgm.Update(Entity);
            }
            ValidationErrors = rgm.ValidationErrors;

            base.Save();
        }


    }
}
