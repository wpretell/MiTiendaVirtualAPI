using System;
using System.Collections.Generic;

namespace MiTiendaVirtualAPI.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
