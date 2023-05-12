using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Models.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public CurrencyType Currency { get; set; }
        public string LastUpdate { get; set; }
    }
}
