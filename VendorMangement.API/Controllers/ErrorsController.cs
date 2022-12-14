using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VendorMangement.API.Controllers
{
    public class ErrorsController : ApiController
    {
        [Route("/error")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
