using AutoMapper;
using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Requests;
using Levi9.ERP.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, IMapper mapper, IUrlHelper urlHelper, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _mapper = mapper;
            _urlHelper = urlHelper;
            _logger = logger;   
        }

        [HttpPost]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateClient([FromBody] ClientRequest client)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientController. Timestamp: {Timestamp}.", nameof(CreateClient), DateTime.UtcNow);
            ClientDTO clientMap = _mapper.Map<ClientDTO>(client);

            if ( await _clientService.GetClientByEmail(client.Email) != null)
            {
                _logger.LogWarning("Invalid email address: {Email} in {FunctionName} of AuthenticationController. Timestamp: {Timestamp}.", client.Email, nameof(CreateClient), DateTime.UtcNow);
                return BadRequest("Email already exists");
            }

            ClientDTO clientDto = await _clientService.CreateClient(clientMap);
            string location = _urlHelper.Action("CreateClient", "Client", new { clientId = clientDto.Id }, Request.Scheme);
            _logger.LogInformation("Client created successfully in {FunctionName} of ClientController. Timestamp: {Timestamp}.", nameof(CreateClient), DateTime.UtcNow);
            return Created(location, _mapper.Map<ClientResponse>(clientDto));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            _logger.LogInformation("Entering {FunctionName} in ClientController. Timestamp: {Timestamp}.", nameof(GetClientById), DateTime.UtcNow);
            var clientDTO = await _clientService.GetClientById(id);
            if (clientDTO == null)
            {
                _logger.LogWarning("Client not found with ID: {ClientId} in {FunctionName} of ClientController. Timestamp: {Timestamp}.", id, nameof(GetClientById), DateTime.UtcNow);
                return NotFound("User not found");
            }
            var clientResponse = _mapper.Map<ClientResponse>(clientDTO);
            _logger.LogInformation("Client retrieved successfully with ID: {ClientId} in {FunctionName} of ClientController. Timestamp: {Timestamp}.", id, nameof(GetClientById), DateTime.UtcNow);
            return Ok(clientResponse);
        }
    }
}
