using ImageStorage;
using System;
using System.Collections.Generic;
using System.Web;


using System.Threading.Tasks;
using System.Linq;

namespace DataRepository.Models
{
    public class HeadingViewModel : BaseModel.ViewModelBase
    {

        public HeadingViewModel() : base()
        {
           

        }

        //Properties--------------
        public List<MainHeading> Headings { get; set; }
        public MainHeading SearchEntity { get; set; }
        public MainHeading Entity { get; set; }
        
        
        public string CommandString { get; set; }
        public string Message { get; set; }
        public int? searchRegattaID { get; set; }
      

      
        //---------------------------------------------------------------
        protected override void Init()
        {
            Headings = new List<MainHeading>();
            SearchEntity = new MainHeading();
            Entity = new MainHeading();

            
                  base.Init();
        }

        public override void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "edit":
                case "save":
                    CommandString = "save";
                    break;

                case "add":
                    CommandString = "insert";
                    break;
                default:
                    CommandString = "";
                    break;
            }
                base.HandleRequest();
           
        }

        protected override void ResetSearch()
        {
            SearchEntity = new MainHeading();
        }

        protected override void Get()
        {
            HeadingManager cmm = new HeadingManager();
            Headings = cmm.Get(SearchEntity);
        }

        protected override void Edit()
        {
            HeadingManager imm = new HeadingManager();
            Entity = imm.Find(Convert.ToInt32(EventArgument));

            base.Edit();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new MainHeading();
          
            base.Add();
        }

        protected override void Save()
        {
            HeadingManager hm = new HeadingManager();
            if (hm.Update(Entity))
            {
                Mode = "List";
                Message = "Image successfully updated";
            }

            ValidationErrors = hm.ValidationErrors;

            base.Save();

        }


        protected Boolean Insert()
        {
            bool success = false;
            HeadingManager hm = new HeadingManager();
            if (Mode == "Add")
            {
           

               
               

               success= hm.Insert(Entity);
                if (success)
                {
                    Mode = "List";
                    Message = "Image successfully added";
                }
                else
                {
                    Message = "Error uploading image";
                };
            }
          
            ValidationErrors = hm.ValidationErrors;
            return success;
           
        }

        protected override void Delete()
        {
            HeadingManager hm = new HeadingManager();
            Entity = hm.Find(Convert.ToInt32(EventArgument));
            hm.Delete(Entity);
            Get();
            base.Delete();

        }


    }
}
