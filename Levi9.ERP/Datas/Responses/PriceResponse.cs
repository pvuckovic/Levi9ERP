using Levi9.ERP.Domain.Models;

namespace Levi9.ERP.Datas.Responses
{
    public class PriceResponse
    {
        public int PriceListId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
