using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaVirtualAPI.Models;

namespace MiTiendaVirtualAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly TiendaVirtualDbContext BD;

        // Constructor that receives the database context
        // This allows the controller to access the database
        // The context is injected via dependency injection
        public UsuarioController(TiendaVirtualDbContext context)
        {
            BD = context;
        }

        //GET. /api/usuario
        [HttpGet]
        public IEnumerable<Usuario> ListaDeUsuarios()
        {
            return BD.Usuario.ToList();
        }

        //GET. /api/usuario/2
        [HttpGet("{id}", Name = "UsuarioCreado")]
        public IActionResult DevolverUsuario(int id)
        {
            var usuario = BD.Usuario.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        //POST. /api/usuario
        [HttpPost()]
        public IActionResult CrearUsuario([FromBody] Usuario pUsuario)
        {
            if (ModelState.IsValid)
            {
                //guardamos el usuario en la BD
                BD.Usuario.Add(pUsuario);
                BD.SaveChanges();

                //devolvemos el usurio recientemente creado
                return new CreatedAtRouteResult("UsuarioCreado", new { id = pUsuario.Id }, pUsuario);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/usuario/5
        [HttpPut("{id}")]
        public IActionResult ModificarUsuario([FromBody] Usuario pUsuario, int id)
        {
            if (pUsuario.Id != id)
            {
                return BadRequest();
            }

            BD.Entry(pUsuario).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }

        //DELETE. /api/usuario/5
        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = BD.Usuario.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            BD.Usuario.Remove(usuario);
            BD.SaveChanges();

            return Ok(usuario);
        }

    }
}
