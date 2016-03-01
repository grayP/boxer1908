using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class RegattaCrewViewModel: BaseModel.ViewModelBase
    {

        public RegattaCrewViewModel(): base()
        {
            regatta = new Regatta();
            AllCrew = new List<CrewMemberSelect>();
            //Init();

        }

        //Properties--------------
        public Regatta regatta { get; set; }
       
        public List<CrewMemberSelect> AllCrew { get; set; }
        public List<int?> regattaCrew { get; set; }
       

        //---------------------------------------------------------------
        protected override void Init()
        {
        
          base.Init();
          
        }





        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        //protected override void ResetSearch()
        //{
        //    SearchEntity = new Regatta();
        //}

        public void LoadData()
        {
            Get();
        }

        protected override void Get()
        {

            RegattaCrewManager rgm = new RegattaCrewManager();
            regattaCrew = rgm.Get(regatta);
            AllCrew = rgm.GetAllCrew();

            foreach (CrewMemberSelect crew  in AllCrew)
            {
                crew.selected = rgm.IsSelected(crew.Id, regatta.Id);
            }


        }

       //protected override void Edit()
       // {
       //     RegattaManager rgm = new RegattaManager();
       //     Entity=rgm.Find(Convert.ToInt32(EventArgument));
       //     base.Edit();
       // }

        //protected override void Add()
        //{
        //    IsValid = true;
        //    Entity = new Regatta();
        //    Entity.RegattaName = "";
        //    Entity.StartDate = DateTime.Now;
        //    base.Add();
        //}


        //protected override void Save()
        //{
        //    RegattaManager rgm = new RegattaManager();
        //    if (Mode == "Add")
        //    {
        //        rgm.Insert(Entity);
        //    }
        //    else
        //    {
        //        rgm.Update(Entity);
        //    }
        //    ValidationErrors = rgm.ValidationErrors;

        //    base.Save();
        //}

        //protected override void Delete()
        //{
        //    RegattaManager rgm = new RegattaManager();
        //    Entity = rgm.Find(Convert.ToInt32(EventArgument));
        //    rgm.Delete(Entity);
        //    Get();
        //    base.Delete();

        //}


    }
}
