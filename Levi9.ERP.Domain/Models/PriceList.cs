using System.ComponentModel.DataAnnotations;

namespace Levi9.ERP.Domain.Models
{
    public class PriceList
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        [StringLength(18), MinLength(18)]
        public string LastUpdate { get; set; }
        public List<Price> Prices { get; set; }
    }
}
