using Levi9.ERP.Domain.Model;

namespace Levi9.ERP.Request
{
    public class ClientRequest
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LastUpdate { get; set; }
        public int PriceListId { get; set; }
    }
}
