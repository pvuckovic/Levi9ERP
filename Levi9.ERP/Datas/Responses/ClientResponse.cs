﻿namespace Levi9.ERP.Responses
{
    public class ClientResponse
    {
        public int ClientId { get; set; }
        public Guid GlobalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LastUpdate { get; set; }
    }
}
