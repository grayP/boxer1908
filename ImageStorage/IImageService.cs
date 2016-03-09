using System.Threading.Tasks;
using System.Web;


namespace ImageStorage
{


    public interface IImageService
    {
        Task<UploadedImage> CreateUploadedImage(HttpPostedFileBase file, UploadedImage oldImage);
        Task AddImageToBlobStorageAsync(UploadedImage image);
    }


}
