using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenomHubPhotoWebSiteRestAPI.Db;
using VenomHubPhotoWebSiteRestAPI.Models;

namespace VenomHubPhotoWebSiteRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _photoServiceUrl;

        public PhotoController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _photoServiceUrl = configuration["PhotoServiceUrl"]!;
        }

        //[HttpGet]
        //public IActionResult Read()
        //{
        //    var lst = _context.Photos.ToList();
        //    return Ok(lst);
        //}

        [HttpGet]
        public IActionResult Read()
        {
            var photos = _context.Photos
                .Include(p => p.Category)
                .Include(p => p.Album)
                .Select(p => new
                {
                    p.Id,
                    p.PhotoName,
                    p.PhotoDescription,
                    CategoryName = p.Category.CategoryName,
                    AlbumName = p.Album.AlbumName,
                    PhotoUrl = $"{_photoServiceUrl}/{p.Photo}",  // Construct the full URL using the base address
                    p.CategoryId,
                    p.AlbumId
                })
                .ToList();

            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var photo = _context.Photos
                .Include(p => p.Category)
                .Include(p => p.Album)
                .Select(p => new
                {
                    p.Id,
                    p.PhotoName,
                    p.PhotoDescription,
                    CategoryName = p.Category.CategoryName,
                    AlbumName = p.Album.AlbumName,
                    PhotoUrl = $"{_photoServiceUrl}/{p.Photo}",  // Construct the full URL using the base address
                    p.CategoryId,
                    p.AlbumId
                })
                .FirstOrDefault(x => x.Id == id);

            if (photo == null)
            {
                return NotFound("No Data Found");
            }

            return Ok(photo);
        }



        [HttpPost]
        public IActionResult Create(PhotoModel photoModel)
        {
            _context.Photos.Add(photoModel);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Save successful" : "Save failed";

            return Ok(message);
        }

        [HttpPut("id")]
        public IActionResult Update(int id, PhotoModel photoModel)
        {
            var photo = _context.Photos.FirstOrDefault(x => x.Id == id);

            if (photo == null)
            {
                return NotFound("No Data Found");
            }

            photo.PhotoName = photoModel.PhotoName;
            photo.PhotoDescription = photoModel.PhotoDescription;
            photo.Photo = photoModel.Photo;
            photo.CategoryId = photoModel.CategoryId;
            photo.AlbumId = photoModel.AlbumId;
            
            var result = _context.SaveChanges();
            string message = result > 0 ? "Update successful" : "Update failed";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PhotoModel photoModel)
        {
            var photo = _context.Photos.FirstOrDefault(x => x.Id == id);

            if (photo == null)
            {
                return NotFound("No Data Found");
            }

            photo.PhotoName = photoModel.PhotoName;
            photo.PhotoDescription = photoModel.PhotoDescription;
            photo.Photo = photoModel.Photo;
            photo.CategoryId = photoModel.CategoryId;
            photo.AlbumId = photoModel.AlbumId;

            var result = _context.SaveChanges();
            string message = result > 0 ? "Update successful" : "Update failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var photos = _context.Photos.FirstOrDefault(x=> x.Id == id);
            if (photos == null)
            {
                return NotFound("No Data Found");
            }

            _context.Photos.Remove(photos);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Delete successful" : "Delete failed";
            return Ok(message);
        }
    }
}
