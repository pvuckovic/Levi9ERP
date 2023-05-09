﻿using AutoMapper;
using Levi9.ERP.Domain.Models;
using Levi9.ERP.Domain.Models.DTO;

namespace Levi9.ERP.Domain.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataBaseContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ClientDTO AddClient(ClientDTO clientModel)
        {
            Client clientMap = _mapper.Map<Client>(clientModel);
            var createdEntity = _context.Clients.Add(clientMap);
            SaveChanges();
            return _mapper.Map<ClientDTO>(createdEntity.Entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
