using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Models.DTO
{
    public class SearchArticleDTO
    {
        public int PageId { get; set; }
        public string? SearchString { get; set; }
        public OrderByArticleType? OrderBy { get; set; }
        public DirectionType? Direction { get; set; }
    }
}
