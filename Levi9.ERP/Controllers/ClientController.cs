﻿using AutoMapper;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Requests;
using Levi9.ERP.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public ClientController(IClientService clientService, IMapper mapper, IUrlHelper urlHelper)
        {
            _clientService = clientService;
            _mapper = mapper;
            _urlHelper = urlHelper;

        }

        [HttpPost]
        [Consumes("application/json")]
        [AllowAnonymous]
        public ActionResult<ClientResponse> CreateClient([FromBody] ClientRequest client)
        {
            ClientDTO clientMap = _mapper.Map<ClientDTO>(client);

            if (_clientService.GetClientByEmail(client.Email) != null)
            {
                return BadRequest("Email already exists");
            }

            ClientDTO clientDto = _clientService.CreateClient(clientMap);
            string location = _urlHelper.Action("CreateClient", "Client", new { clientId = clientDto.Id }, Request.Scheme);

            return Created(location, _mapper.Map<ClientResponse>(clientDto));
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ClientResponse> GetClientById(int id)
        {
            var clientDTO = _clientService.GetClientById(id);
            if (clientDTO == null)
            {
               return NotFound("User not found");
            }
            var clientResponse = _mapper.Map<ClientResponse>(clientDTO);
            return Ok(clientResponse);
        }
    }
}
