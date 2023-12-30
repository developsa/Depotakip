using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Reyon")]
    public partial class Reyon
    {
        public Reyon()
        {
            Kategoris = new HashSet<Kategori>();
        }

        [Key]
        public int ReyonId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Reyon Adı")]
        public string? ReyonAd { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Reyon Fotoğrafı")]
        public string? ReyonImg { get; set; }
        public int? DepoId { get; set; }

        [ForeignKey("DepoId")]
        [InverseProperty("Reyons")]
        public virtual Depo? Depo { get; set; }
        [InverseProperty("Reyon")]
        public virtual ICollection<Kategori> Kategoris { get; set; }
        [NotMapped]      //created by a space outside the relationship
        [DisplayName("Upload Image File")]
        public IFormFile ImageFile { get; set; }
    }
}
