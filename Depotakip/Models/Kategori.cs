using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Kategori")]
    public partial class Kategori
    {
        public Kategori()
        {
            Uruns = new HashSet<Urun>();
        }

        [Key]
        public int KategoriId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Kategori Adı")]
        public string? KategoriAd { get; set; }
    
        public int? ReyonId { get; set; }

        [ForeignKey("ReyonId")]
        [InverseProperty("Kategoris")]
        public virtual Reyon? Reyon { get; set; }
        [InverseProperty("Kategori")]
        public virtual ICollection<Urun> Uruns { get; set; }
    }
}
