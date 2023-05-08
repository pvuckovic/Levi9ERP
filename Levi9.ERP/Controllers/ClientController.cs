using AutoMapper;
using Levi9.ERP.Domain.Model;
using Levi9.ERP.Domain.Model.DTO;
using Levi9.ERP.Domain.Service;
using Levi9.ERP.Request;
using Levi9.ERP.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Levi9.ERP.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;
        private readonly IUrlHelper urlHelper;

        public ClientController(IClientService clientService, IMapper mapper, IUrlHelper urlHelper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<ClientResponse> CreateClient ([FromBody] ClientRequest client)
        {   
                ClientDTO clientMap = mapper.Map<ClientDTO>(client);
                ClientDTO clientDto = clientService.CreateClient(clientMap);
                string location = urlHelper.Action("CreateClient", "Client", new { clientId = clientDto.Id }, Request.Scheme);

            return Created(location, mapper.Map<ClientResponse>(clientDto));
        }
    }
}
