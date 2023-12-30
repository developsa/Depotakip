using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Depotakip.Models
{
    [Table("Tedarikci")]
    public partial class Tedarikci
    {
        [Key]
        
        public int TedarikciId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Tedarikçi Adı")]
        public string? Ad { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        [DisplayName("Adresi")]
        public string? Adres { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        [DisplayName(" Telefonu")]
        public string? Tel { get; set; }
    }
}
