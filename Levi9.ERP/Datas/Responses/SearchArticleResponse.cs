namespace Levi9.ERP.Datas.Responses
{
    public class SearchArticleResponse
    {
        public List<PriceListArticleResponse> PricelistArticles { get; set; }
        public int Page { get; set; }
    }
}
