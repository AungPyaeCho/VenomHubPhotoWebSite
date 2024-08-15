using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VenomHubPhotoWebsite.RestAPI.Controllers
{
    //
    [Route("api/[controller]")] //endpoint
    [ApiController]
    public class PhotoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Read()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return Ok();
        }

        [HttpPut] //update the whole resource
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpPatch] //update one by one
        public IActionResult Ptach()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
