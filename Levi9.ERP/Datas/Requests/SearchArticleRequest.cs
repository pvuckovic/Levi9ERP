using Levi9.ERP.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Levi9.ERP.Datas.Requests
{
    public class SearchArticleRequest
    {
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Please enter a value bigger than {0})]")]
        public int PageId { get; set; }
        public string? SearchString { get; set; }
        public OrderByArticleType? OrderBy { get; set; }
        public DirectionType? Direction { get; set; }

    }
}
