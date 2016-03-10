using DataRepository;
using DataRepository.Models;
using ImageStorage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
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
            imv.imagetoUpload.Status = false;

            return View(imv);
        }


        [HttpPost]
        public async Task<ActionResult> Index(FormCollection formCollection, ImageViewModel model)
        {
            ImageManager imm = new ImageManager();
            ImageStorage.UploadedImage _imageToUpload = new UploadedImage
                         {RegattaID=model.imagetoUpload.RegattaID,
                         Caption=model.imagetoUpload.Caption};
           


            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["file"];
                _imageToUpload = await _imageService.CreateUploadedImage(file, _imageToUpload);
                await _imageService.AddImageToBlobStorageAsync(_imageToUpload);

                Image imageToStoreInDB = new Image
                {
                    RegattaID=_imageToUpload.RegattaID,
                    Caption=_imageToUpload.Caption,
                    ImageURL=_imageToUpload.Url,
                    ThumbNailSmall = ImageService.ImageToByte(_imageToUpload.Thumbnails[0].bitmap),
                    ThumbNailLarge = ImageService.ImageToByte(_imageToUpload.Thumbnails[1].bitmap)
                };


                model.imagetoUpload.Status=imm.Insert(imageToStoreInDB);
               
            }
            model.imagetoUpload = _imageToUpload;

            return View(model);
        }

        [HttpGet]
        public ActionResult Show()
        {

            // ImageStorage.ImageService imageService = new ImageService();
            // CloudBlobContainer container= imageService.GetImagesBlobContainer();
            ImageManager iim = new ImageManager();


            List<Image> images = new List<Image>();
            Image _image = new Image();
            images = iim.Get(_image);

            //foreach (IListBlobItem item in container.ListBlobs())
            //{
            //    CloudBlockBlob blob = (CloudBlockBlob)item;
            //    blob.FetchAttributes();
            //    images.Add(new UploadedImage ()
            //    {
                 
            //        Url = item.Uri.ToString(),
            //        Name = blob.Metadata["Name"],
                 
            //        Caption = blob.Metadata["Caption"]
            //    });

            //   // images.Add((UploadedImage)blob);


            //}


            return View(images);
           // return View(container.ListBlobs());
        }
    }



}