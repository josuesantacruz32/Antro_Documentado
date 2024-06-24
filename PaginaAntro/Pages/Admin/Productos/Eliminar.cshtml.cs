using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaAntro.Servicios;

namespace PaginaAntro.Pages.Admin.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        // Constructor que inyecta el entorno web y el contexto de la base de datos
        public EliminarModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        // Método que se ejecuta al cargar la página (HTTP GET)
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

            // Obtiene la ruta completa de la imagen y la elimina del servidor
            string imageFullPath = environment.WebRootPath + "/productos/" + product.Imagen;
            System.IO.File.Delete(imageFullPath);

            // Elimina el producto de la base de datos y guarda los cambios
            context.Producto.Remove(product);
            context.SaveChanges();

            // Redirige al índice de productos después de eliminar el producto
            Response.Redirect("/Admin/Productos/Index");
        }
    }
}
