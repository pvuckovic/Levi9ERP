namespace Levi9.ERP.Domain.Models.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Phone { get; set; }
        public string LastUpdate { get; set; }
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }

    }
}
