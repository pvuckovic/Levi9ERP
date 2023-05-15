namespace Levi9.ERP.Datas.Responses
{
    public class SearchDocumentResponse
    {
        public IEnumerable<DocumentResponse> Items { get; set; }
        public int Page { get; set; }
    }
}
