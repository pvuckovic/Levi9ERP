namespace Levi9.ERP.Domain.Models.DTO
{
    public class PriceListDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string? Name { get; set; }
        public string? LastUpdate { get; set; }
    }
}
