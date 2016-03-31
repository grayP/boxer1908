using System.Web.Mvc;

namespace bw01.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  
            return View("NotFound");
        }
    }
}