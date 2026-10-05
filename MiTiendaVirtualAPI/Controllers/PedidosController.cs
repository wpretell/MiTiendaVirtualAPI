using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaVirtualAPI.Models;

namespace MiTiendaVirtualAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly TiendaVirtualDbContext BD;

        public PedidosController(TiendaVirtualDbContext context)
        {
            BD = context;
        }

        //GET. /api/pedidos/2
        [HttpGet("{id}", Name = "PedidoCreado")]
        public IActionResult Pedido(int id)
        {
            var pedido = BD.Pedido
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.IdTarjetaNavigation)
                .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        //POST. /api/pedidos
        [HttpPost()]
        //public IActionResult CrearPedido([FromBody] Pedido pPedido)
        public IActionResult CrearPedido()
        {
            Pedido pPedido = new Pedido();

            pPedido.Estado = "EN PROCESO";
            pPedido.Total = 0;

            if (ModelState.IsValid)
            {
                //guardamos el pedido en la BD
                BD.Pedido.Add(pPedido);
                BD.SaveChanges();

                //devolvemos el pedido recientemente creado
                return new CreatedAtRouteResult("PedidoCreado", new { id = pPedido.Id }, pPedido);
            }

            return BadRequest(ModelState);
        }

        //PUT. /api/pedidos/5
        [HttpPut("{id}")]
        public IActionResult ActualizarPedido([FromBody] Pedido pPedido, int id)
        {
            if (pPedido.Id != id)
            {
                return BadRequest();
            }

            BD.Entry(pPedido).State = EntityState.Modified;
            BD.SaveChanges();

            return Ok();
        }

    }
}
