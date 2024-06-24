using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaAntro.Modelos;
using PaginaAntro.Servicios;

namespace PaginaAntro.Pages.Admin.Productos
{
    public class AgregarModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;
        // Propiedad para vincular los datos del formulario
        [BindProperty]
        public ProductoDTO ProductoDTO { get; set; } = new ProductoDTO();

        // Constructor que inyecta el entorno web y el contexto de la base de datos
        public AgregarModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        // Método que se ejecuta al cargar la página por primera vez (HTTP GET)
        public void OnGet()
        {
        }

        // Propiedades para manejar mensajes de error y éxito
        public string errorMessage = "";
        public string successMessage = "";

        // Método que se ejecuta al enviar el formulario (HTTP POST)
        public void OnPost()
        {
            // Validación para asegurar que se ha seleccionado una imagen
            if (ProductoDTO.Imagen == null)
            {
                ModelState.AddModelError("ProductoDTO.Imagen", "Archivo de imagen requerido");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Ingresa todos los campos";
                return;
            }


            // Guardar el archivo
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductoDTO.Imagen!.FileName);

            
            string imageFullPath = environment.WebRootPath + "/Productos/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                ProductoDTO.Imagen!.CopyTo(stream);
            }


            // Guardar el nuevo producto en la base de datos
            Producto product = new Producto()
            {
                NombreProducto = ProductoDTO.NombreProducto,
                Descripcion = ProductoDTO.Descripcion,
                Precio = ProductoDTO.Precio,
                Cantidad = ProductoDTO.Cantidad,
                Imagen = newFileName
                
            };

            context.Producto.Add(product);
            context.SaveChanges();


            // LIMPIAR EL FORMULARIO
            ProductoDTO.NombreProducto= "";
            ProductoDTO.Descripcion= "";
            ProductoDTO.Precio = 0;
            ProductoDTO.Cantidad = 0;
            ProductoDTO.Imagen = null;


            ModelState.Clear();

            // Establecer un mensaje de éxito y redirigir al índice de productos
            successMessage = "Producto agregado exitosamente";

            Response.Redirect("/Admin/Productos/Index");  
        }
    }
}