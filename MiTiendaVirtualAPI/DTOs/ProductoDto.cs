namespace MiTiendaVirtualAPI.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int? IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public int? IdMarca { get; set; }
        public string? Marca { get; set; }
    }
}
