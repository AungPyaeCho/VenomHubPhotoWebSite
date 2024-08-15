using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VenomHubPhotoWebSiteRestAPI.Middlewares;

namespace VenomHubPhotoWebSiteRestAPI.Models
{
    [Table("tblAdmin")]
    public class AdminModel
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? AdminName { get; set; }

        [Required]
        [EmailAddress]
        public string? AdminEmail { get; set; }

        [Required]
        public string? AdminPassword { get; set; }

        public DateTime AdminCreatedAt { get; set; } = DateTime.UtcNow;

        public void SetEncryptedPassword(string plainPassword)
        {
            AdminPassword = SimpleEncryptionHelper.Encrypt(plainPassword);
        }

        public string GetDecryptedPassword()
        {
            return SimpleEncryptionHelper.Decrypt(AdminPassword);
        }
    }

    public class LoginResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }

    public class AdminResponseModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool IsEndOfPage => PageNo >= PageCount;
        public List<AdminModel> AdminData { get; set; } = new List<AdminModel>();
    }
}
