using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaAntro.Modelos;
using PaginaAntro.Servicios;

namespace PaginaAntro.Pages.Admin.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        // Propiedad para vincular los datos del formulario de edición
        [BindProperty]

        // Propiedad para mantener el producto a editar
        public ProductoDTO ProductoDTO { get; set; } = new ProductoDTO();

        public Producto Producto { get; set; } = new Producto();

        // Propiedades para manejar mensajes de error y éxito
        public string errorMessage = "";
        public string successMessage = "";

        public EditarModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        // Método que se ejecuta al cargar la página por primera vez (HTTP GET)
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            // Busca el producto por su ID en la base de datos
            var product = context.Producto.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            // Asigna los valores del producto a los campos del formulario de edición
            ProductoDTO.NombreProducto = product.NombreProducto;
            ProductoDTO.Descripcion = product.Descripcion;
            ProductoDTO.Precio = product.Precio;
            ProductoDTO.Cantidad = product.Cantidad;
            

            Producto = product;
        }

        // Método que se ejecuta al enviar el formulario de edición (HTTP POST)
        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Completa todos los campos";
                return;
            }

            var product = context.Producto.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Productos/Index");
                return;
            }


            // Actualiza el archivo de imagen si se ha proporcionado uno nuevo
            string newFileName = product.Imagen;
            if (ProductoDTO.Imagen != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductoDTO.Imagen!.FileName);

                string imageFullPath = environment.WebRootPath + "/Productos/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductoDTO.Imagen.CopyTo(stream);
                }

                // Elimina la imagen antigua del servidor
                string oldImageFullPath = environment.WebRootPath + "/Productos/" + product.Imagen;
                System.IO.File.Delete(oldImageFullPath);
            }


            // Actualiza los datos del producto en la base de datos
            product.NombreProducto = ProductoDTO.NombreProducto;
            product.Descripcion = ProductoDTO.Descripcion;
            product.Precio = ProductoDTO.Precio;
            product.Cantidad = ProductoDTO.Cantidad;
            product.Imagen = newFileName;

            context.SaveChanges();


            Producto = product;

            // Establece un mensaje de éxito y redirige al índice de productos
            successMessage = "Producto modificado exitosamente";

            Response.Redirect("/Admin/Productos/Index");
        }
    }
}
