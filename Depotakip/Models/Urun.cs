using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Urun")]
    public partial class Urun
    {
        [Key]
        public int UrunId { get; set; }
        public int? KategoriId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Ürün Adı")]
        public string? UrunAd { get; set; }
        [DisplayName("Ürün Adet")]
        public int? UrunAdet { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        [DisplayName("Ürün Fiyat")]
        public decimal? UrunFiyat { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("Geliş Tarihi")]
        public DateTime? Gelistarih { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Ürün Fotoğrafı")]
        public string? UrunFoto { get; set; }

        [ForeignKey("KategoriId")]
        [InverseProperty("Uruns")]
        public virtual Kategori? Kategori { get; set; }

        [NotMapped]    
        [DisplayName("Upload Image File")]
        public IFormFile ImageFile { get; set; }
    }
}
