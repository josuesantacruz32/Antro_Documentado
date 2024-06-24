using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaAntro.Modelos
{
    public class Producto
    {
        [Key] // Indica que esta propiedad es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Productoid { get; set; } // Identificador único del producto

        public string NombreProducto { get; set; } // Nombre del producto


        public string Descripcion { get; set; } // Descripcion del producto

        public double Precio { get; set; } // Precio del producto

        public int Cantidad { get; set; } // Cantidad del producto

        public string Imagen { get; set; } // Imagen del producto
    }
}
