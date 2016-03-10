using ImageStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataRepository.Models
{
    public class ImageViewModel: BaseModel.ViewModelBase
    {

        public ImageViewModel(): base()
        {

              
        }
        
        //Properties--------------
        public List<Image> Images { get; set; }
        public Image SearchEntity { get; set; }
        public Image Entity { get; set; }

        public UploadedImage imagetoUpload { get; set; }

        //public IList<SelectListItem> RegattaList
        //{
        //    get
        //    {
        //        using (boxerdb db = new boxerdb())
        //        {
        //            var SelectList = (from item in
        //                              db.Regattas.OrderBy(x => x.StartDate)
        //                              select new SelectListItem()
        //                              {
        //                                  RegattaID = item.Id,
        //                                  Value = item.RegattaName
        //                              }).ToList();

        //            return SelectList;
        //        }

        //    }


        //    set { }
        //}




        //---------------------------------------------------------------
        protected override void Init()
        {
          Images = new List<Image>();
          SearchEntity = new Image();
          Entity = new Image();
          imagetoUpload = new UploadedImage();
 
            base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new Image();
        }

        protected override void Get()
        {
            ImageManager cmm = new ImageManager();
            Images = cmm.Get(SearchEntity);
        }

       protected override void Edit()
        {
            ImageManager cmm = new ImageManager();
            Entity =cmm.Find(Convert.ToInt32(EventArgument));
            base.Edit();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = new Image();
            Entity.Caption = "";
            base.Add();
        }


        protected override void Save()
        {
            ImageManager cmm = new ImageManager();
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
            ImageManager cmm = new ImageManager();
            Entity = cmm.Find(Convert.ToInt32(EventArgument));
            cmm.Delete(Entity);
            Get();
            base.Delete();

        }


    }
}
