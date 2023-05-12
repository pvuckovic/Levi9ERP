namespace Levi9.ERP.Domain.Models.DTO
{
    public class DocumentItemDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float PriceValue { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
    }
}
