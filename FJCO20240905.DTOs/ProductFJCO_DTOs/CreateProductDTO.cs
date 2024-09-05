using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJCO20240905.DTOs.ProductFJCO_DTOs
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo Descripcion no puede tener más de 100 caracteres.")]
        public string Decripcion { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0, 1000000, ErrorMessage = "El campo Precio debe estar entre 0 y 1000000.")]
        public decimal Precio { get; set; }

    }
}
