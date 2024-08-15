
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VenomHubPhotoWebSiteRestAPI.Models
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

        public DateTime CategoryCreateAt { get; set; } = DateTime.UtcNow;

        public ICollection<PhotoModel> Photos { get; set; }
    }
}
