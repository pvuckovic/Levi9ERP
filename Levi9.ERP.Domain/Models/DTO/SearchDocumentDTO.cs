using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Models.DTO
{
    public class SearchDocumentDTO
    {
        public int Page { get; set; }
        public string Name { get; set; }
        public string OrderBy { get; set; }
        public string Direction { get; set; }
    }
}
