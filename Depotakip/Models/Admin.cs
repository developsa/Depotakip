using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? KullaniciAdi { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string? Sifre { get; set; }
    }
}
