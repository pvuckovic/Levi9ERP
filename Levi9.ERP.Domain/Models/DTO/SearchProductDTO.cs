namespace Levi9.ERP.Domain.Models.DTO
{
    public class SearchProductDTO
    {
        public int Page { get; set; }
        public string Name { get; set; }
        public string OrderBy { get; set; }
        public string Direction { get; set; }
    }
}
