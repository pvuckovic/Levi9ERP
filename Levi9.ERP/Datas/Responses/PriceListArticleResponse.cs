using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Datas.Responses
{
    public class PriceListArticleResponse
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string? Name { get; set; }
        public string? LastUpdate { get; set; }
        public List<ArticleDTO>? Articles { get; set; }
    }
}
