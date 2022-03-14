using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beadando.Models
{
    public class Arukereses
    {
        public string elnevezeskereses { get; set; }
        public string kategoriaKereses { get; set; }
        public SelectList kategorialista { get; set; }
        public List<Adatmodel> Aru { get; set; }
    }
}
