//using Microsoft.AspNetCore.Mvc;
//using VenomHubPhotoWebSiteAdminPanel.Db;
//using VenomHubPhotoWebSiteAdminPanel.Models;
//using VenomHubPhotoWebSiteAdminPanel.Services;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

//namespace VenomHubPhotoWebSiteAdminPanel.Controllers
//{
//    public class PhotoController : Controller
//    {
//        private readonly CRUDService<PhotoModel> _CRUDService;
//        private readonly CRUDService<CategoryModel> _categoryService;
//        private readonly CRUDService<AlbumModel> _albumService;
//        private readonly AppDbContext _appDbContext;

//        public PhotoController(CRUDService<PhotoModel> cRUDService, CRUDService<CategoryModel> categoryService, CRUDService<AlbumModel> albumService, AppDbContext appDbContext)
//        {
//            _CRUDService = cRUDService;
//            _categoryService = categoryService;
//            _albumService = albumService;
//            _appDbContext = appDbContext;
//        }

//        [ActionName("Index")]
//        public async Task<IActionResult> PhotoIndex(int pageNo = 1, int pageSize = 10)
//        {
//            var response = await _CRUDService.GetPhotoPagedData(pageNo, pageSize);
            
//            return View("PhotoIndex", response);
//        }


//        [ActionName("Create")]
//        public async Task<IActionResult> PhotoCreate()
//        {
//            try
//            {
//                // Retrieve categories and albums asynchronously
//                var categories = await _categoryService.GetValues();
//                var albums = await _albumService.GetValues();

//                // Pass the data to the view using ViewBag
//                ViewBag.Categories = categories;
//                ViewBag.Albums = albums;

//                // Return the "PhotoCreate" view
//                return View("PhotoCreate");
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", $"An error occurred while loading the create item page: {ex.Message}");
//                return View("PhotoCreate");
//            }
            
//        }

//        [HttpPost]
//        [ActionName("Save")]
//        public async Task<IActionResult> PhotoSave(PhotoModel photoModel, IFormFile Photo)
//        {
//            bool result = await _CRUDService.Create(photoModel, Photo, nameof(photoModel.Photo), "photo");
//            var model = new MsgResponseModel
//            {
//                IsSuccess = result,
//                ResponseMessage = result ? "Save Success" : "Save Fail"
//            };
//            return Json(model);
//        }

//        [ActionName("Edit")]
//        public async Task<IActionResult> PhotoEdit(int Id)
//        {
//            var item = await _CRUDService.GetById(Id);
//            if (item == null)
//            {
//                return Redirect("/Photo");
//            }

//            int catgeoryId = Convert.ToInt32(item.CategoryId);
//            int albumId = Convert.ToInt32(item.AlbumId);

//            var category= await _categoryService.GetById(catgeoryId);
//            var album = await _albumService.GetById(albumId);

//            var list = new PhotoModel
//            {
//                PhotoName = item.PhotoName,
//                PhotoDescription = item.PhotoDescription,
//                Photo = item.Photo,
//                CategoryId = category.Id,
//                CategoryName = category.CategoryName,
//                AlbumId = album.Id,
//                AlbumName = album.AlbumName
//            };
//            var categories = await _categoryService.GetValues();
//            var albums = await _albumService.GetValues();

//            // Pass the data to the view using ViewBag
//            ViewBag.Categories = categories;
//            ViewBag.Albums = albums;

//            return View("PhotoEdit", list);
//        }

//        [HttpPost]
//        [ActionName("Update")]
//        public async Task<IActionResult> PhotoUpdate(PhotoModel photoModel, IFormFile Photo, bool updatePhoto)
//        {
//            try
//            {
//                Console.WriteLine(photoModel.Id);
//                bool result = await _CRUDService.Update(photoModel, Photo, nameof(photoModel.Photo), updatePhoto, "photo");
//                var msg = new MsgResponseModel
//                {
//                    IsSuccess = result,
//                    ResponseMessage = result ? "Update Success" : "Update Fail"
//                };
//                return Json(msg);
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", $"An error occurred while loading the create item page: {ex.Message}");
//                var msg = new MsgResponseModel
//                {
//                    ResponseMessage = ex.Message
//                };
//                return View("Photo");
//            }
//        }

//        [HttpPost]
//        [ActionName("Delete")]
//        public async Task<IActionResult> PhotoDelete(int Id)
//        {
//            bool result = await _CRUDService.Delete(Id, nameof(PhotoModel.Photo));
//            var msg = new MsgResponseModel
//            {
//                IsSuccess = result,
//                ResponseMessage = result ? "Delete Success" : "Delete Fail"
//            };
//            return Json(msg);
//        }
//    }
//}
