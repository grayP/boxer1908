using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class CrewMemberViewModel: BaseModel.ViewModelBase
    {

        public CrewMemberViewModel(): base()
        {
            
        }
        
        //Properties--------------
        public List<CrewMember> CrewMembers { get; set; }
        public CrewMember SearchEntity { get; set; }
        public CrewMember Entity { get; set; }
    
        //---------------------------------------------------------------
        protected override void Init()
        {
          CrewMembers = new List<CrewMember>();
          SearchEntity = new CrewMember();
          Entity = new CrewMember();
          base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new CrewMember();
        }

        protected override void Get()
        {
            CrewManager cmm = new CrewManager();
            CrewMembers = cmm.Get(SearchEntity);
        }

       protected override void Edit()
        {
            CrewManager cmm = new CrewManager();
            Entity =cmm.Find(Convert.ToInt32(EventArgument));
            base.Edit();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new CrewMember();
            Entity.CrewName = "";
            base.Add();
        }


        protected override void Save()
        {
            CrewManager cmm = new CrewManager();
            if (Mode == "Add")
            {
                cmm.Insert(Entity);
            }
            else
            {
                cmm.Update(Entity);
            }
            ValidationErrors = cmm.ValidationErrors;

            base.Save();
        }

        protected override void Delete()
        {
            CrewManager cmm = new CrewManager();
            Entity = cmm.Find(Convert.ToInt32(EventArgument));
            cmm.Delete(Entity);
            Get();
            base.Delete();

        }


    }
}
