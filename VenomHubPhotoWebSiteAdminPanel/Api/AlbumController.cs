using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VenomHubPhotoWebSiteAdminPanel.Db;
using VenomHubPhotoWebSiteAdminPanel.Models;
using System;

namespace VenomHubPhotoWebSiteAdminPanel.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // Requires valid JWT for access
    public class AlbumController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(AppDbContext context, ILogger<AlbumController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Read()
        {
            try
            {
                var albums = _context.Albums.ToList();
                return Ok(albums);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while reading photos.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving data.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            try
            {
                var albums = _context.Albums.FirstOrDefault(x => x.Id==id);

                if (albums == null)
                {
                    return NotFound("No Data Found");
                }
                return Ok(albums);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving photo with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the photo.");
            }
        }

        //[HttpPost]
        //public IActionResult Create(PhotoModel photoModel)
        //{
        //    try
        //    {
        //        _context.Photos.Add(photoModel);
        //        var result = _context.SaveChanges();
        //        string message = result > 0 ? "Save successful" : "Save failed";
        //        return Ok(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error while creating photo.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the photo.");
        //    }
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, PhotoModel photoModel)
        //{
        //    try
        //    {
        //        var photo = _context.Photos.FirstOrDefault(x => x.Id == id);

        //        if (photo == null)
        //        {
        //            return NotFound("No Data Found");
        //        }

        //        photo.PhotoName = photoModel.PhotoName;
        //        photo.PhotoDescription = photoModel.PhotoDescription;
        //        photo.Photo = photoModel.Photo;
        //        photo.CategoryId = photoModel.CategoryId;
        //        photo.AlbumId = photoModel.AlbumId;

        //        var result = _context.SaveChanges();
        //        string message = result > 0 ? "Update successful" : "Update failed";

        //        return Ok(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error while updating photo with ID {id}.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the photo.");
        //    }
        //}

        //[HttpPatch("{id}")]
        //public IActionResult Patch(int id, PhotoModel photoModel)
        //{
        //    try
        //    {
        //        var photo = _context.Photos.FirstOrDefault(x => x.Id == id);

        //        if (photo == null)
        //        {
        //            return NotFound("No Data Found");
        //        }

        //        photo.PhotoName = photoModel.PhotoName;
        //        photo.PhotoDescription = photoModel.PhotoDescription;
        //        photo.Photo = photoModel.Photo;
        //        photo.CategoryId = photoModel.CategoryId;
        //        photo.AlbumId = photoModel.AlbumId;

        //        var result = _context.SaveChanges();
        //        string message = result > 0 ? "Update successful" : "Update failed";
        //        return Ok(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error while patching photo with ID {id}.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the photo.");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var photo = _context.Photos.FirstOrDefault(x => x.Id == id);
        //        if (photo == null)
        //        {
        //            return NotFound("No Data Found");
        //        }

        //        _context.Photos.Remove(photo);
        //        var result = _context.SaveChanges();
        //        string message = result > 0 ? "Delete successful" : "Delete failed";
        //        return Ok(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error while deleting photo with ID {id}.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the photo.");
        //    }
        //}
    }
}
