using Microsoft.AspNetCore.Mvc;
using CodenameRome.Contracts.Clients;
using CodenameRome.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CodenameRome.Models;

namespace CodenameRome.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    //[Authorize(Roles="0")]
    public class ClientController : ControllerBase
    {
        private readonly IClientApplication _clientApplication;

        public ClientController(IClientApplication clientApplication) =>
            _clientApplication = clientApplication;

        [HttpGet]
        [ProducesResponseType(typeof(List<Client>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var clientList = await _clientApplication.GetAllClients();
            return Ok(clientList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var client = await _clientApplication.GetClientsById(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(CreateClientDto client)
        {
            try
            {
                var response = await _clientApplication.CreateClient(client);
                return Created(String.Empty, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update(string id, ClientDto client)
        {
            try
            {
                var updatedClient = await _clientApplication.UpdateClient(id, client);
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("status/{id}")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeStatus(string id, bool status)
        {
            try
            {
                var client = await _clientApplication.ChangeClientStatus(id, status);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var client = await _clientApplication.DeleteClient(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
