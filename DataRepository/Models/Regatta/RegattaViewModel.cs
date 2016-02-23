using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class RegattaViewModel
    {

        public RegattaViewModel()
        {

            Init();

            regattas = new List<tblRegatta>();
            SearchEntity = new tblRegatta();
            Entity = new tblRegatta();
        }



        //Properties--------------
        public string EventCommand { get; set; }
        public List<tblRegatta> regattas { get; set; }
        public tblRegatta SearchEntity { get; set; }
        public tblRegatta Entity { get; set; }
        public bool IsDetailVisible { get; set; }
        public bool IsSearchVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsValid { get; set; }
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }



        public string EventArgument { get; set; }
        public string Mode { get; set; }
        //---------------------------------------------------------------
        private void Init()
        {
            ListMode();
            EventCommand = "list";
            

        }


        public void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "list":
                case "search":
                    ListMode();
                    Get();
                    break;
                case "resetsearch":
                    ResetSearch();
                    Get();
                    break;
                case "add":
                    Add();
                    Get();
                    break;
                case "cancel":
                    ListMode();
                    Get();
                    break;
                case "save":
                    Save();
                    if (IsValid)
                    {
                        Get();
                    }
                    break;
                case "delete":
                    break;
                default:
                    break;
            }
        }

        private void ResetSearch()
        {
            SearchEntity = new tblRegatta();
        }

        private void ListMode()
        {
            IsValid = true;
 
            IsListAreaVisible = true;
            IsSearchVisible = true;
            IsDetailVisible = false;

            Mode = "list";

        }


        private void AddMode()
        {
            IsListAreaVisible = false;
            IsSearchVisible = false;
            IsDetailVisible = true;

            Mode = "Add";

        }


        private void Get()
        {
            RegattaManager rgm = new RegattaManager();
            regattas = rgm.Get(SearchEntity);
        }

        private void Add()
            {
            IsValid = true;
            Entity = new tblRegatta();
            Entity.RegattaName = "";
            Entity.StartDate = DateTime.Now;


            AddMode();

            }


        private void Save()
        {

            RegattaManager rgm = new RegattaManager();
            if (Mode=="Add")
                {
                    rgm.Insert(Entity);
                }

            ValidationErrors = rgm.ValidationErrors;
            if (ValidationErrors.Count>0)
            {
                IsValid = false;
            }

            if (!IsValid)
            {
                if (Mode=="Add")
                {
                    AddMode();
                }
            }
        }

         
    }
}
