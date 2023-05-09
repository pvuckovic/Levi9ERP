using Levi9.ERP.Data.Responses;

namespace Levi9.ERP.Datas.Responses
{
    public class SearchProductResponse
    {
        public IEnumerable<ProductResponse> Items { get; set; }
        public int Page { get; set; }
    }
}
