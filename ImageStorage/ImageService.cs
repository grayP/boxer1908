﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace ImageStorage
{
    public class ImageService : IImageService
    {
        private readonly string _imageRootPath;
        private readonly string _containerName;
        private readonly string _blobStorageConnectionString;
        private DateTime imageDate;
        private static Regex r = new Regex(":");


        public ImageService()
        {

           // string connstring = ConfigurationManager.AppSettings["imageStorage"].ToString();
           // string ContainerName = ConfigurationManager.AppSettings["ContainerName"].ToString();
          //  string BlobName = ConfigurationManager.AppSettings["BlobName"].ToString();


            _imageRootPath = ConfigurationManager.AppSettings["ImageRootPath"];
            _containerName = ConfigurationManager.AppSettings["ImagesContainer"];
            _blobStorageConnectionString = ConfigurationManager.ConnectionStrings["BlobStorageConnectionString"].ConnectionString;
        }

        
        public async Task<UploadedImage> CreateUploadedImage(HttpPostedFileBase file, UploadedImage oldImage)
        {
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                byte[] fileBytes = new byte[file.ContentLength];
                await file.InputStream.ReadAsync(fileBytes, 0, Convert.ToInt32(file.ContentLength));



               Image _image = Image.FromStream(file.InputStream);

               

                try
                {
                    PropertyItem propItem = _image.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    imageDate = DateTime.Parse(dateTaken);
                }
                catch (Exception)
                {
                     imageDate = DateTime.Now;
                  
                }


                CreateThumbnails(_image, oldImage);

               
               
               

                return new UploadedImage
                {
                    ContentType = file.ContentType,
                    Caption=oldImage.Caption,
                    RegattaID=oldImage.RegattaID,
                    Thumbnails=oldImage.Thumbnails,
                    Data = fileBytes,
                    Name = file.FileName,
                    Url = String.Format("{0}/{1}",
                    _imageRootPath,
                    file.FileName.ToString()),
                    DateTaken=imageDate
                };
            }
            return null;
        }

        private void CreateThumbnails(Image image, UploadedImage oldImage)
        {
         

            for (int i = 200; i <=600 ; i+=200)
            {
                Thumbnail thumb = new Thumbnail()
                {
                    bitmap = ResizeImage(image, i),
                    Height = i
                };
                 oldImage.Thumbnails.Add(thumb);
            }
          



        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Bitmap ResizeImage(Image image,  int height)
        {
            double aspectRatio = (double)image.Height / image.Width;
            int width= (int)(height/aspectRatio);

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }





        public async Task AddImageToBlobStorageAsync(UploadedImage image)
        {
            //  get the container reference
            var container = GetImagesBlobContainer();
            // using the container reference, get a block blob reference and set its type
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(image.Name);
            blockBlob.Properties.ContentType = image.ContentType;
            // finally, upload the image into blob storage using the block blob reference
            var fileBytes = image.Data;
            await blockBlob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);
        }
        public CloudBlobContainer GetImagesBlobContainer()
        {
            // use the connection string to get the storage account
            var storageAccount = CloudStorageAccount.Parse(_blobStorageConnectionString);
            // using the storage account, create the blob client
            var blobClient = storageAccount.CreateCloudBlobClient();
            // finally, using the blob client, get a reference to our container
            var container = blobClient.GetContainerReference(_containerName);
            // if we had not created the container in the portal, this would automatically create it for us at run time
            container.CreateIfNotExists();
            // by default, blobs are private and would require your access key to download.
            //   You can allow public access to the blobs by making the container public.   
            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            return container;
        }


        async public static Task ListBlobsSegmentedInFlatListing(CloudBlobContainer container)
        {
            //List blobs to the console window, with paging.
            Console.WriteLine("List blobs in pages:");

            int i = 0;
            BlobContinuationToken continuationToken = null;
            BlobResultSegment resultSegment = null;

            //Call ListBlobsSegmentedAsync and enumerate the result segment returned, while the continuation token is non-null.
            //When the continuation token is null, the last page has been returned and execution can exit the loop.
            do
            {
                //This overload allows control of the page size. You can return all remaining results by passing null for the maxResults parameter,
                //or by calling a different overload.
                resultSegment = await container.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);
                if (resultSegment.Results.Count<IListBlobItem>() > 0) { Console.WriteLine("Page {0}:", ++i); }
                foreach (var blobItem in resultSegment.Results)
                {
                    Console.WriteLine("\t{0}", blobItem.StorageUri.PrimaryUri);
                }
                Console.WriteLine();

                //Get the continuation token.
                continuationToken = resultSegment.ContinuationToken;
            }
            while (continuationToken != null);
        }



    }
}
