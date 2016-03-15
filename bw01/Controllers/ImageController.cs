using DataRepository;
using DataRepository.Models;
using ImageStorage;
using System.Collections.Generic;
using System.Web.Mvc;

namespace bw01.Controllers
{
    public class ImageController : Controller
    {

        private readonly IImageService _imageService = new ImageService();

        private ImageViewModel imv = new ImageViewModel();
        // GET: Image
        public ActionResult Index(int RegattaID)
        {

            imv.SearchEntity.RegattaID = RegattaID;
            imv.HandleRequest();
            imv.imageToUpload.Status = false;

            return View(imv);
        }


        [HttpPost]
        public ActionResult Index(FormCollection formCollection, ImageViewModel iVm)
        {

            iVm.IsValid = ModelState.IsValid;
            if (Request != null)
            {
                iVm.file = Request.Files["file"];
            }

            iVm.HandleRequest();

            if (iVm.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in iVm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

            }

            return View(iVm);


        }



    }

}