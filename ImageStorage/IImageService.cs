using System.Threading.Tasks;
using System.Web;


namespace ImageStorage
{


    public interface IImageService
    {
        Task<UploadedImage> CreateUploadedImage(HttpPostedFileBase file);
        Task AddImageToBlobStorageAsync(UploadedImage image);
    }


}
