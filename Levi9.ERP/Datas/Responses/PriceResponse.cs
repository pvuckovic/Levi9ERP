using Levi9.ERP.Datas.Requests;

namespace Levi9.ERP.Datas.Responses
{
    public class PriceResponse
    {
        public int PriceListId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public Currency Currency { get; set; }
    }
}
