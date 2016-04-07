using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class RegattaDisplayViewModel : BaseModel.ViewModelBase
    {

        public RegattaDisplayViewModel() : base()
        {

        }

        //Properties--------------
        public Regatta regatta { get; set; }
        public List<Document> stories { get; set; }
        public List<Image> photos { get; set; }
        public List<CrewMember> crew { get; set; }

        //---------------------------------------------------------------
        protected override void Init()
        {
            regatta = new Regatta();
            stories = new List<Document>();
            photos = new List<Image>();
            crew = new List<CrewMember>();

            base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

       

        protected override void Get()
        {
            RegattaDisplayManager rgm = new RegattaDisplayManager();
            regatta = rgm.Get(regatta.Id);
            stories = rgm.GetStories(regatta.Id);
            photos = rgm.GetPhotos(regatta.Id);
          //  crew = rgm.GetCrew(regatta.Id);

        }








    }
}
