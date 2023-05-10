using Levi9.ERP.Datas.Requests;
using Levi9.ERP.Domain.Helpers;
using Levi9.ERP.Domain.Models.DTO;
using Levi9.ERP.Domain.Services;
using Levi9.ERP.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Levi9.ERP.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly JwtOptions _config;

        public AuthenticationController(IClientService clientService, JwtOptions config)
        {
            _clientService = clientService;
            _config = config;

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public IActionResult ClientAuthentication([FromBody] AuthenticationRequest authenticationRequest)
        {
            ClientDTO authenticationDTO = _clientService.GetClientByEmail(authenticationRequest.Email);
            if (authenticationDTO == null)
            {
                throw new BadRequestException("Bad request - email not valid");
            }
            bool validateUser = AuthenticationHelper.ValidateUser(authenticationDTO.Password, authenticationDTO.Salt, authenticationRequest.Password);
            if (!validateUser)
            {
                throw new BadRequestException("Bad Request - wrong password");
            }
            var token = AuthenticationHelper.GenerateJwt(_config);
            return Ok(token);
        }

    }
}
