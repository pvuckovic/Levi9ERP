using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Model.DTO
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public string LastUpdate { get; set; }
    }
}
