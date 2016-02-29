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
        public List<Regatta> regattas { get; set; }
        public Regatta SearchEntity { get; set; }
        public Regatta Entity { get; set; }
    
        //---------------------------------------------------------------
        protected override void Init()
        {
          regattas = new List<Regatta>();
          SearchEntity = new Regatta();
          Entity = new Regatta();
          base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new Regatta();
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
            Entity = new Regatta();
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
