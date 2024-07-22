using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToothCareAPI.Model;
using ToothCareAPI.Model.DTO;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Controllers
{
    [Route("api/Clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsRepository _dbClients;
        private readonly IMapper _mapper;
        protected ApiResponse _res;
        public ClientsController(IClientsRepository dbClients, IMapper mapper)
        {
            _dbClients = dbClients;
            _mapper = mapper;
            _res = new();
        }

        [HttpGet]
        [Authorize(Roles = "admin,staff")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                IEnumerable<Clients> clientsList = await _dbClients.GetAllAsync();
                _res.Result = _mapper.Map<List<ClientsDTO>>(clientsList);
                return Ok(clientsList);

            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:int}", Name = "GetClient")]
        [Authorize(Roles = "admin,staff")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClient(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<String>() { "Invalid ID" };
                    return BadRequest(_res);
                }
                if (await _dbClients.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<String>() { "Client not found" };
                    return NotFound(_res);
                }

                Clients client = await _dbClients.GetAsync(u => u.Id == id);
                _res.StatusCode = HttpStatusCode.OK;
                _res.Result = _mapper.Map<ClientsDTO>(client);
                return Ok(_res);
            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<String>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin,staff")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateClient([FromBody] ClientsCreateDTO clientCreate)
        {
            try
            {
                if(clientCreate == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Client is null" };
                    return BadRequest(_res);
                }
                if (await _dbClients.GetAsync(u => u.Pesel == clientCreate.Pesel,false) != null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Client already Exists" };
                    return BadRequest(_res);
                }
                Clients client = _mapper.Map<Clients>(clientCreate);
                await _dbClients.CreatAsync(client);
                _res.Result = _mapper.Map<ClientsDTO>(client);
                _res.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetClient", new { id = client.Id }, _res);
            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id:int}",Name ="DeleteClient")]
        [Authorize(Roles = "admin,staff")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Invalid ID" };
                    return BadRequest(_res);
                }
                if (await _dbClients.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<string>() { "Client not found" };
                    return NotFound(_res);
                }

                Clients client = await _dbClients.GetAsync(u => u.Id == id);
                await _dbClients.DeleteAsync(client);
                _res.StatusCode = HttpStatusCode.OK;
                return Ok(_res);
            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin,staff")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateClient([FromBody] ClientsUpdateDTO clientsUpdate, int id)
        {
            
            try
            {
                if (clientsUpdate == null || await _dbClients.GetAsync(u => u.Id == id,false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Client is null" };
                    return BadRequest(_res);
                }
                if (clientsUpdate.Id != id || id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Invalid ID" };
                    return BadRequest(_res);
                }
                Clients client = _mapper.Map<Clients>(clientsUpdate);
                await _dbClients.UpdateAsync(client);
                _res.StatusCode = HttpStatusCode.OK;
                return Ok(_res);
            }
            catch(Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }
    }
}
