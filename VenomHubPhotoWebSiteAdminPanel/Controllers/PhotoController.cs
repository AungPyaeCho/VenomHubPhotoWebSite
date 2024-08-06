using Microsoft.AspNetCore.Mvc;
using VenomHubPhotoWebSiteAdminPanel.Models;
using VenomHubPhotoWebSiteAdminPanel.Services;

namespace VenomHubPhotoWebSiteAdminPanel.Controllers
{
    public class PhotoController : Controller
    {
        private readonly CRUDService<PhotoModel> _CRUDService;

        public PhotoController(CRUDService<PhotoModel> cRUDService)
        {
            _CRUDService = cRUDService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> PhotoIndex(int pageNo = 1, int pageSize = 10)
        {
            var response = await _CRUDService.GetPagedData<PhotoResponseModel>(pageNo, pageSize);
            return View("PhotoIndex", response);
        }


        [ActionName("Create")]
        public IActionResult PhotoCreate()
        {
            return View("PhotoCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> PhotoSave(PhotoModel photoModel, IFormFile Photo)
        {
            bool result = await _CRUDService.Create(photoModel, Photo, nameof(photoModel.Photo), "photo");
            var model = new MsgResponseModel
            {
                IsSuccess = result,
                ResponseMessage = result ? "Save Success" : "Save Fail"
            };
            return Json(model);
        }

        [ActionName("Edit")]
        public async Task<IActionResult> PhotoEdit(int Id)
        {
            var item = await _CRUDService.GetById(Id);
            if (item == null)
            {
                return Redirect("/Photo");
            }
            return View("PhotoEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> PhotoUpdate(PhotoModel photoModel, IFormFile Photo, bool updatePhoto)
        {
            bool result = await _CRUDService.Update(photoModel, Photo, nameof(photoModel.Photo), updatePhoto, "photo");
            var msg = new MsgResponseModel
            {
                IsSuccess = result,
                ResponseMessage = result ? "Update Success" : "Update Fail"
            };
            return Json(msg);
        }



        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> PhotoDelete(int Id)
        {
            bool result = await _CRUDService.Delete(Id, nameof(PhotoModel.Photo));
            var msg = new MsgResponseModel
            {
                IsSuccess = result,
                ResponseMessage = result ? "Delete Success" : "Delete Fail"
            };
            return Json(msg);
        }
    }
}
