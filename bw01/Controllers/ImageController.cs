using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ImageStorage;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        public async Task<ActionResult> Index(FormCollection formCollection)
        {
            ImageStorage.UploadedImage image = new UploadedImage();

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["file"];
                image = await _imageService.CreateUploadedImage(file);
                await _imageService.AddImageToBlobStorageAsync(image);
            }
            return View(image);
        }

        [HttpGet]
        public ActionResult Show()
        {

            ImageStorage.ImageService imageService = new ImageService();
            CloudBlobContainer container= imageService.GetImagesBlobContainer();

            List<UploadedImage> images = new List<UploadedImage>();

            //foreach (CloudBlob blob in container.ListBlobs())
            //{
            //    images.Add((UploadedImage)blob);
                

            //}


            return View(container.ListBlobs());
        }
    }



}