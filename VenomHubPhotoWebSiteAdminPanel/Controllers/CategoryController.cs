using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VenomHubPhotoWebSiteAdminPanel.Db;
using VenomHubPhotoWebSiteAdminPanel.Models;
using VenomHubPhotoWebSiteAdminPanel.Services;

namespace VenomHubPhotoWebSiteAdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CRUDService<CategoryModel> _CRUDService;

        public CategoryController(CRUDService<CategoryModel> cRUDService)
        {
            _CRUDService = cRUDService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> CategoryIndex(int pageNo = 1, int pageSize = 10)
        {
            var response = await _CRUDService.GetPagedData<CategoryResponseModel>(pageNo, pageSize);
            return View("CategoryIndex", response);
        }


        [ActionName("Create")]
        public IActionResult CategoryCreate()
        {
            return View("CategoryCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> CategorySave(CategoryModel categoryModel, IFormFile categoryPhoto)
        {
            bool result = await _CRUDService.Create(categoryModel, categoryPhoto, nameof(categoryModel.CategoryPhoto), "category");
            var model = new MsgResponseModel
            {
                IsSuccess = result,
                ResponseMessage = result ? "Save Success" : "Save Fail"
            };
            return Json(model);
        }

        [ActionName("Edit")]
        public async Task<IActionResult> CategoryEdit(int Id)
        {
            var item = await _CRUDService.GetById(Id);
            if (item == null)
            {
                return Redirect("/Category");
            }
            return View("CategoryEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> CategoryUpdate(CategoryModel categoryModel, IFormFile categoryPhoto, bool updatePhoto)
        {
            bool result = await _CRUDService.Update(categoryModel, categoryPhoto, nameof(categoryModel.CategoryPhoto), updatePhoto, "category");
            var msg = new MsgResponseModel
            {
                IsSuccess = result,
                ResponseMessage = result ? "Update Success" : "Update Fail"
            };
            return Json(msg);
        }



        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> CategoryDelete(int Id)
        {
            bool result = await _CRUDService.Delete(Id, nameof(CategoryModel.CategoryPhoto));
            var msg = new MsgResponseModel
            {
                IsSuccess = result,
                ResponseMessage = result ? "Delete Success" : "Delete Fail"
            };
            return Json(msg);
        }
    }
}
