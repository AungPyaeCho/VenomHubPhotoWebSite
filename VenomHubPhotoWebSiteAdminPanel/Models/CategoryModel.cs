﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VenomHubPhotoWebSiteAdminPanel.Models
{
    [Table("tblCategory")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string? CategoryName { get; set; }

        [StringLength(100)]
        public string? CategoryDescription { get; set; }

        [StringLength(250)]
        public string? CategoryPhoto { get; set; }

        public DateTime AlbumCreateAt { get; set; } = DateTime.UtcNow;

        public ICollection<PhotoModel> Photos { get; set; }
    }
}