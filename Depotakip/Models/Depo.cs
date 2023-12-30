using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Depo")]
    public partial class Depo
    {
        public Depo()
        {
            Personels = new HashSet<Personel>();
            Reyons = new HashSet<Reyon>();
        }

        [Key]
        public int DepoId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Depo Adı")]
        public string? Ad { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Adres { get; set; }
        public int? Kapasite { get; set; }
        [DisplayName("Personel Sayısı")]
        public int? PersonelSayisi { get; set; }

        [InverseProperty("Depo")]
        public virtual ICollection<Personel> Personels { get; set; }
        [InverseProperty("Depo")]
        public virtual ICollection<Reyon> Reyons { get; set; }
    }
}
