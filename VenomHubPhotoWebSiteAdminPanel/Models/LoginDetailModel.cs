using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VenomHubPhotoWebSiteAdminPanel.Models
{
    [Table("tblLoginDetail")]
    public class LoginDetailModel
    {
        [Key]
        public string? Id { get; set; }
        public string? AdminId { get; set; }
        public string? AdminEmail { get; set; }
        public string AdminName { get; set; }
        public string? SessionId { get; set; }
        public DateTime? SessionExpired { get; set; }
        public DateTime? loginAt { get; set; } = DateTime.Now;
        public DateTime? LogOutAt { get; set; }

        [ForeignKey("Id")]
        public AdminModel Admin { get; set; }
    }

    public class LoginDetailResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }

        public bool isEndofpage => pageNo >= pageCount;
        public List<LoginDetailModel> LoginDetailData { get; set; }
    }
}
