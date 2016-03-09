using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ImageStorage;
using System.Drawing;

namespace bw01.Controllers
{
    public class ImageController : Controller
    {

        private readonly IImageService _imageService = new ImageService();


        // GET: Image
        public ActionResult Index()
        {
            ImageStorage.UploadedImage image = new ImageStorage.UploadedImage();
            return View(image);
        }


        [HttpPost]
        public async Task<ActionResult> Index(FormCollection formCollection, UploadedImage model)
        {
            ImageStorage.UploadedImage image = new UploadedImage
            {Regatta=model.Regatta,
            Caption=model.Regatta};
           


            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["file"];
                image = await _imageService.CreateUploadedImage(file, image);
                await _imageService.AddImageToBlobStorageAsync(image);
            }
            return View(image);
        }

        [HttpGet]
        public ActionResult Show()
        {

            ImageStorage.ImageService imageService = new ImageService();
            CloudBlobContainer container= imageService.GetImagesBlobContainer();

          //  List<UploadedImage> images = new List<UploadedImage>();

            //foreach (CloudBlob blob in container.ListBlobs())
            //{
            //    images.Add((UploadedImage)blob);
                

            //}


            return View(container.ListBlobs());
        }
    }



}