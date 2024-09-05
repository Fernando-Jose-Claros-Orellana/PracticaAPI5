using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJCO20240905.DTOs.ProductFJCO_DTOs
{
    public class SearchQueryProductDTO
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }

        [Display(Name = "Pagina")]
        public int skip { get; set; }
        [Display(Name = "CantReg X Pagina")]
        public int take { get; set; }

        public byte? SendRowCount { get; set; }

    }
}
