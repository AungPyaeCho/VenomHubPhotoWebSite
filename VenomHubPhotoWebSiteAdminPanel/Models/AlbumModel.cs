using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VenomHubPhotoWebSiteAdminPanel.Models
{
    [Table("tblAlbum")]
    public class AlbumModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [StringLength(100)]
        public string? AlbumName { get; set; }


        [StringLength(100)]
        public string? AlbumDescription { get; set; }


        [StringLength(2500)]
        public string? AlbumPhoto { get; set; }

        public DateTime AlbumCreateAt { get; set; } = DateTime.UtcNow;

        public ICollection<PhotoModel> Photos { get; set; }
    }
}
