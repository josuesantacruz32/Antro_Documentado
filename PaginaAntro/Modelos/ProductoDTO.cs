using System.ComponentModel.DataAnnotations;

namespace PaginaAntro.Modelos
{
    public class ProductoDTO
    {
        [Required]
        public string NombreProducto {  get; set; } // Nombre del producto (obligatorio)

        public string Descripcion { get; set; } // Descripción del producto

        public double Precio { get; set; } // Precio del producto

        public int Cantidad { get; set; } // Cantidad del producto

        public IFormFile Imagen { get; set; } // Imagen del producto
    }
}
