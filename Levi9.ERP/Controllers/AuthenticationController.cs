using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Domain.Helpers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly JwtOptions _config;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IClientService clientService, JwtOptions config, ILogger<AuthenticationController> logger)
        {
            _clientService = clientService;
            _config = config;
            _logger = logger;
        }

        [HttpPost]
        [Consumes("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> ClientAuthentication([FromBody] AuthenticationRequest authenticationRequest)
        {
            _logger.LogInformation("Entering {FunctionName} in AuthenticationController. Timestamp: {Timestamp}.", nameof(ClientAuthentication), DateTime.UtcNow);
            ClientDTO authenticationDTO = await _clientService.GetClientByEmail(authenticationRequest.Email);
            if (authenticationDTO == null)
            {
                _logger.LogWarning("Invalid emaila address: {Email} in {FunctionName} of AuthenticationController. Timestamp: {Timestamp}.", authenticationRequest.Email, nameof(ClientAuthentication), DateTime.UtcNow);
                return BadRequest("Email not valid");
            }
            bool validateUser = AuthenticationHelper.ValidateUser(authenticationDTO.Password, authenticationDTO.Salt, authenticationRequest.Password);
            if (!validateUser)
            {
                _logger.LogWarning("Wront password in {FunctionName} of AuthenticationController. Timestamp: {Timestamp}.", nameof(ClientAuthentication), DateTime.UtcNow);
                return BadRequest("Bad Request - wrong password");
            }
            var token = AuthenticationHelper.GenerateJwt(_config);
            _logger.LogInformation("Client successfully validated in {FunctionName} of AuthenticationController. Timestamp: {Timestamp}.", nameof(ClientAuthentication), DateTime.UtcNow);
            return Ok(token);
        }

    }
}
