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


        // GET: Image
        public ActionResult Index()
        {

            ImageViewModel imv = new ImageViewModel();
            imv.HandleRequest();
            imv.imageToUpload.Status = false;

            return View(imv);
        }


        [HttpPost]
        public ActionResult Index(FormCollection formCollection, ImageViewModel ivm)
        {

            ivm.IsValid = ModelState.IsValid;
            if (Request != null)
            {
                ivm.file = Request.Files["file"];
            }

            ivm.HandleRequest();

            if (ivm.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in ivm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }

            }

            return View(ivm);


        }



    }

}