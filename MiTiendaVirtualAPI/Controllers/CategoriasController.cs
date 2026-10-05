using Microsoft.AspNetCore.Mvc;
using MiTiendaVirtualAPI.Models;

namespace MiTiendaVirtualAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly TiendaVirtualDbContext BD;

        public CategoriasController(TiendaVirtualDbContext context)
        {
            BD = context;
        }

        //GET. /api/categorias
        [HttpGet]
        public IEnumerable<Categoria> Categorias()
        {
            return BD.Categoria.ToList();

        }

        //GET. /api/categorias/activas
        [Route("activas")]
        [HttpGet]
        public IEnumerable<Categoria> CategoriasActivas()
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            listaCategorias = (from c in BD.Categoria
                               where c.Activo == true
                               select c).ToList();

            return listaCategorias;

        }

    }
}
