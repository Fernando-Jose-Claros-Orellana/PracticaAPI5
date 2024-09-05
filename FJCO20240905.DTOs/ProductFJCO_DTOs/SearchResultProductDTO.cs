using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJCO20240905.DTOs.ProductFJCO_DTOs
{
    public class SearchResultProductDTO
    {
        public int CountRow { get; set; }

        public List<ProductDTO> Data { get; set; }

        public class ProductDTO
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
        }
    }
}
