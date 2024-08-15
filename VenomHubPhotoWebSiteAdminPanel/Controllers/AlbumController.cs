using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using VenomHubPhotoWebSiteAdminPanel.Db;
using VenomHubPhotoWebSiteAdminPanel.Migrations;
using VenomHubPhotoWebSiteAdminPanel.Models;

namespace VenomHubPhotoWebSiteAdminPanel.Controllers
{
    public class AlbumController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AlbumController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [ActionName("Index")]
        public IActionResult AlbumIndex(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = _appDbContext.Albums.AsNoTracking().Count();
            int pageCount = (int)Math.Ceiling((double)rowCount / pageSize); // Use Math.Ceiling to handle rounding up

            List<AlbumModel> list = _appDbContext.Albums
                .AsNoTracking() // Apply No Tracking
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            AlbumResponseModel response = new()
            {
                Data = list,
                Pagination = new PaginationModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    PageCount = pageCount
                }
            };

            return View("AlbumIndex", response); // View name is optional if the view file name matches the action name

        }

        [ActionName("Create")]
        public IActionResult AlbumCreate()
        {
            return View("AlbumCreate");
        }
        [HttpPost]
        [ActionName("Save")]
        public IActionResult AlbumSave(AlbumModel albumModel, IFormFile albumPhoto)
        {

            if (albumPhoto != null && albumPhoto.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "album");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = $"{Guid.NewGuid()}_{albumPhoto.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    albumPhoto.CopyTo(fileStream);
                }

                albumModel.AlbumPhoto = "/album/" + uniqueFileName;
            }
            //albumModel.staffCreateAt = DateTime.Now.ToString();


            _appDbContext.Albums.Add(albumModel);
            int result = _appDbContext.SaveChanges();

            string message = result > 0 ? "Save Success" : "Save Fail";
            var model = new MsgResponseModel
            {
                IsSuccess = result > 0,
                ResponseMessage = message
            };
            return Json(model);
        }

        [ActionName("Edit")]
        public IActionResult AlbumEdit(int Id)
        {
            var item = _appDbContext.Albums
                .AsNoTracking() // Ensure no tracking for read operations
                .FirstOrDefault(x => x.Id == Id);

            if (item == null)
            {
                return Redirect("/Album");
            }

            return View("AlbumEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult AlbumUpdate(AlbumModel albumModel, IFormFile albumPhoto, bool updatePhoto)
        {
            MsgResponseModel msg = new MsgResponseModel();
            var item = _appDbContext.Albums.FirstOrDefault(x => x.Id == albumModel.Id);
            if (item == null)
            {
                msg = new MsgResponseModel()
                {
                    IsSuccess = false,
                    ResponseMessage = "No data found"
                };
                return Json(msg);
            }

            item.AlbumName = albumModel.AlbumName;
            item.AlbumDescription = albumModel.AlbumDescription;

            if (updatePhoto)
            {
                // Delete the old photo if it exists
                if (!string.IsNullOrEmpty(item.AlbumPhoto))
                {
                    var oldPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, item.AlbumPhoto.TrimStart('/'));
                    if (System.IO.File.Exists(oldPhotoPath))
                    {
                        System.IO.File.Delete(oldPhotoPath);
                    }
                }

                // Save the new photo
                if (albumPhoto != null && albumPhoto.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "album");
                    var uniqueFileName = albumModel.AlbumName + "_" + Path.GetFileName(albumPhoto.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        albumPhoto.CopyTo(fileStream);
                    }

                    item.AlbumPhoto = "/album/" + uniqueFileName;
                }
            }

            int result = _appDbContext.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update Fail";
            msg = new MsgResponseModel()
            {
                IsSuccess = result > 0,
                ResponseMessage = message
            };

            return Json(msg);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult AlbumDelete(int Id)
        {
            MsgResponseModel msg = new MsgResponseModel();
            var item = _appDbContext.Albums.FirstOrDefault(x => x.Id == Id);
            if (item is null)
            {
                msg.IsSuccess = false;
                msg.ResponseMessage = "No data found";
                return Json(msg);
            }

            if (!string.IsNullOrEmpty(item.AlbumPhoto))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, item.AlbumPhoto.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _appDbContext.Remove(item);
            int result = _appDbContext.SaveChanges();
            msg.IsSuccess = result > 0;
            msg.ResponseMessage = msg.IsSuccess ? "Delete Success" : "Delete Fail";
            return Json(msg);
        }
    }

}

