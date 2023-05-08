using Levi9.ERP.Domain.Model;

namespace Levi9.ERP.Request
{
    public class ClientRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PriceListId { get; set; }
    }
}
