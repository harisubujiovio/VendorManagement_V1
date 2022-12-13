using Microsoft.AspNetCore.Mvc;

namespace VendorMangement.API.Controllers
{
    public class ErrorsController : ApiController
    {
        [Route("/error")]
        [HttpPost]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
