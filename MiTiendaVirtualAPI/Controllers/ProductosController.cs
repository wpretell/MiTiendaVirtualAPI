using Microsoft.AspNetCore.Mvc;
using MiTiendaVirtualAPI.DTOs;
using MiTiendaVirtualAPI.Models;


namespace MiTiendaVirtualAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly TiendaVirtualDbContext BD;

        public ProductosController(TiendaVirtualDbContext context)
        {
            BD = context;
        }

        //GET. /api/productos
        [HttpGet]
        public IEnumerable<Producto> Productos()
        {
            return BD.Producto.ToList();

        }

        //GET. /api/productos/destacados
        [Route("destacados")]
        [HttpGet]
        public IEnumerable<Producto> ProductosDestacados()
        {
            List<Producto> listaProductos = new List<Producto>();

            listaProductos = (from p in BD.Producto
                              where p.Destacado == true && p.Activo == true
                              select p).ToList();

            return listaProductos;

        }

        //GET. /api/productos/porcategoria/2
        //[Route("porcategoria/{id}")]
        [HttpGet("porcategoria/{id}")]
        public IEnumerable<Producto> ProductosPorCategoria(int id)
        {
            List<Producto> listaProductos = new List<Producto>();

            listaProductos = (from p in BD.Producto
                              where p.IdCategoria == id && p.Activo == true
                              select p).ToList();

            return listaProductos;
        }

        //GET. /api/productos/producto/2
        //[Route("producto/{id}")]
        [HttpGet("producto/{id}")]
        public IActionResult Producto(int id)
        {
            var producto = BD.Producto
                .Where(p => p.Id == id)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.IdCategoriaNavigation.Nombre,
                    IdMarca = p.IdMarca,
                    Marca = p.IdMarcaNavigation.Nombre
                })
                .FirstOrDefault();

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }
    }
}
