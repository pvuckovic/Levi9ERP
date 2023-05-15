using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class SearchDocumentRequest
    {
        [Required(ErrorMessage = "Page property is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Page number cannot be 0!")]
        public int Page { get; set; }
        [Required(ErrorMessage = "Name property is required.")]
        public string Name { get; set; }
        [RegularExpression("^(id|globalId|documentType)$", ErrorMessage = "Order by parameter must be id, globalId, documentType.")]
        public string? OrderBy { get; set; }
        [RegularExpression("^(asc|dsc)$", ErrorMessage = "The direction must be asc or dsc")]
        public string? Direction { get; set; }
    }
}
