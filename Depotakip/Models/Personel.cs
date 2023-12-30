using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Personel")]
    public partial class Personel
    {
        [Key]
        public int PersonelId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Adı")]        
        
        public string? Ad { get; set; }
        [StringLength(255)]
        [Unicode(false)]

        [DisplayName("Soyadı")]

        public string? Soyad { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
   
        [DisplayName("Maaş")]
        public decimal? Maas { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("Doğum Tarihi")]
        public DateTime? DogumTarihi { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Personel Fotoğrafı")]
        public string? PersonelFoto { get; set; }
        public int? DepoId { get; set; }

        [ForeignKey("DepoId")]
        [InverseProperty("Personels")]
        public virtual Depo? Depo { get; set; }
        [NotMapped]      
        [DisplayName("Fotoğrafı Yükle")]
        public IFormFile ImageFile { get; set; }
    }
}
