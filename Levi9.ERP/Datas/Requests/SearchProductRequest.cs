using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Datas.Requests
{
    public class SearchProductRequest
    {
        [Required(ErrorMessage = "Page property is required.")]
        public int Page { get; set; }
        [Required(ErrorMessage = "Name property is required.")]
        public string Name { get; set; }
        [RegularExpression("^(name|id|globalId|availableQuantity)$", ErrorMessage = "Order by parameter must be name, id, globalId, availableQuantity.")]
        public string OrderBy { get; set; } = "name";

        [RegularExpression("^(asc|dsc)$", ErrorMessage = "The direction must be asc or dsc.")]
        public string Direction { get; set; } = "asc";
    }
}
