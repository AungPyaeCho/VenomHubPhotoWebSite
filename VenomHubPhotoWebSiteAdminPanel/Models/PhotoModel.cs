
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VenomHubPhotoWebSiteAdminPanel.Models
{
    [Table("tblPhoto")]
    public class PhotoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? PhotoName { get; set; }

        [Required]
        [StringLength(100)]
        public string? PhotoDescription { get; set; }

        [Required]
        [StringLength(250)]
        public string? Photo { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? AlbumId { get; set; }
        public string? AlbumName { get; set; }
        public DateTime AlbumCreateAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(CategoryId))]
        public CategoryModel Category { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public AlbumModel Album { get; set; }
    }
}
