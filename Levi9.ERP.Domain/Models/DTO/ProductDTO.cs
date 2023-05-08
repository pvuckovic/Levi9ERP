namespace Levi9.ERP.Domain.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int AvailableQuantity { get; set; }
        public string LastUpdate { get; set; }
        public List<PriceDTO> PriceList { get; set; }
    }
}
