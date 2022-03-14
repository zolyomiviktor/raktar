using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace beadando.Models
{
    public class Adatmodel
    {
        public int Id { get; set; }
        [StringLength(60)]
        public string Elnevezes { get; set; }
        [StringLength(60)]
        public string Kategoria { get; set; }
        [StringLength(30)]
        public string Csomagolasiegyseg { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Darabszam { get; set; }
    }
}
