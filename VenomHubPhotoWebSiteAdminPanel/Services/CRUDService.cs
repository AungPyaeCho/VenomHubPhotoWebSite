using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VenomHubPhotoWebSiteAdminPanel.Db;
using VenomHubPhotoWebSiteAdminPanel.Models;

namespace VenomHubPhotoWebSiteAdminPanel.Services
{
    public class CRUDService<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CRUDService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PaginatedResponseModel<T>> GetPagedData(int pageNo = 1, int pageSize = 10)
        {
            int rowCount = await _context.Set<T>().CountAsync();
            int pageCount = (int)Math.Ceiling((double)rowCount / pageSize);

            List<T> list = await _context.Set<T>()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            PaginatedResponseModel<T> response = new()
            {
                Data = list,
                Pagination = new PaginationModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    PageCount = pageCount
                }
            };

            return response;
        }

        public async Task<PaginatedResponseModel<PhotoModel>> GetPhotoPagedData(int pageNo = 1, int pageSize = 10)
        {
            var query = from photo in _context.Photos
                        join category in _context.Categories on photo.CategoryId equals category.Id
                        join album in _context.Albums on photo.AlbumId equals album.Id
                        select new PhotoModel
                        {
                            Id = photo.Id,
                            PhotoName = photo.PhotoName,
                            PhotoDescription = photo.PhotoDescription,
                            Photo = photo.Photo,
                            CategoryId = photo.CategoryId,
                            CategoryName = category.CategoryName,
                            AlbumId = photo.AlbumId,
                            AlbumName = album.AlbumName
                        };

            int rowCount = await query.CountAsync();
            int pageCount = (int)Math.Ceiling((double)rowCount / pageSize);

            List<PhotoModel> list = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            PaginatedResponseModel<PhotoModel> response = new()
            {
                Data = list,
                Pagination = new PaginationModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    PageCount = pageCount
                }
            };

            return response;
        }



        public async Task<R> GetPagedData<R>(int pageNo = 1, int pageSize = 10) where R : PaginatedResponseModel<T>, new()
        {
            var genericResponse = await GetPagedData(pageNo, pageSize);
            R response = new R
            {
                Data = genericResponse.Data,
                Pagination = genericResponse.Pagination
            };
            return response;
        }
        
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetValues()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> Create(T entity, IFormFile photo = null, string photoPropertyName = null, string folderName = "uploads")
        {
            if (photo != null && photoPropertyName != null)
            {
                string uniqueFileName = SavePhoto(photo, folderName);
                typeof(T).GetProperty(photoPropertyName).SetValue(entity, $"/{folderName}/{uniqueFileName}");
            }

            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateData(T entity)
        {
            // Ensure that the entity is tracked by the context and updates are applied
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(int Id,T entity, IFormFile photo = null, string photoPropertyName = null, bool updatePhoto = false, string folderName = "uploads")
        {
            var existingEntity = await _context.Set<T>().FindAsync(Id);
            if (existingEntity == null)
            {
                return false; // Entity not found
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            if (updatePhoto && photo != null && photoPropertyName != null)
            {
                // Delete the old photo if it exists
                string currentPhotoPath = (string)typeof(T).GetProperty(photoPropertyName).GetValue(existingEntity);
                if (!string.IsNullOrEmpty(currentPhotoPath))
                {
                    string oldPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, currentPhotoPath.TrimStart('/'));
                    if (File.Exists(oldPhotoPath))
                    {
                        File.Delete(oldPhotoPath);
                    }
                }

                // Save the new photo
                string uniqueFileName = SavePhoto(photo, folderName);
                typeof(T).GetProperty(photoPropertyName).SetValue(existingEntity, $"/{folderName}/{uniqueFileName}");
            }

            _context.Set<T>().Update(existingEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        private string SavePhoto(IFormFile photo, string folderName)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        public async Task<bool> Delete(int id, string photoPropertyName = null)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }

            if (photoPropertyName != null)
            {
                // Delete the photo if it exists
                string photoPath = (string)typeof(T).GetProperty(photoPropertyName).GetValue(entity);
                if (!string.IsNullOrEmpty(photoPath))
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, photoPath.TrimStart('~', '/'));
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        
    }

}