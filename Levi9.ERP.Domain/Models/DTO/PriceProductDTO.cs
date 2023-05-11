namespace Levi9.ERP.Domain.Models.DTO
{
    public class PriceProductDTO
    {
        public int PriceListId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
