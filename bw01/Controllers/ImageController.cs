using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;

using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace bw01.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                //var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                //file.SaveAs(path);

                string connstring = ConfigurationManager.AppSettings["imageStorage"].ToString();
                var storageAccount = CloudStorageAccount.Parse(connstring);

                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer container = blobStorage.GetContainerReference("blobimages");
                 if (container.CreateIfNotExists())
                {
                // configure container for public access
                   var permissions = container.GetPermissions();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions);
                }

                CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

                using (var fileStream = System.IO.File.OpenRead(@"path\myfile"))
                {
                    blockBlob.UploadFromStream(fileStream);
                }

            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

    }



}