using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class LocationViewModel: BaseModel.ViewModelBase
    {

        public LocationViewModel(): base()
        {
          
        }
        
        //Properties--------------
        public List<Location> locations { get; set; }
        public Location SearchEntity { get; set; }
        public Location Entity { get; set; }
    
        //---------------------------------------------------------------
        protected override void Init()
        {
          locations = new List<Location>();
          SearchEntity = new Location();
          Entity = new Location();
          base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new Location();
        }

        protected override void Get()
        {
            LocationManager lcm = new LocationManager();
            locations = lcm.Get(SearchEntity);
        }

       protected override void Edit()
        {
            LocationManager lcm = new LocationManager();
            Entity =lcm.Find(Convert.ToInt32(EventArgument));
            base.Edit();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new Location();
            Entity.Longitude = 0;
            Entity.Latitude =0;
            base.Add();
        }


        protected override void Save()
        {
            LocationManager lcm = new LocationManager();
            if (Mode == "Add")
            {
                lcm.Insert(Entity);
            }
            else
            {
                lcm.Update(Entity);
            }
            ValidationErrors = lcm.ValidationErrors;

            base.Save();
        }

        protected override void Delete()
        {
            LocationManager lcm = new LocationManager();
            Entity = lcm.Find(Convert.ToInt32(EventArgument));
            lcm.Delete(Entity);
            Get();
            base.Delete();

        }


    }
}
