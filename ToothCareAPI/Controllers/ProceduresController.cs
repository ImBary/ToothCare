using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToothCareAPI.Data;
using ToothCareAPI.Model;
using ToothCareAPI.Model.DTO;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {

        private readonly IProceduresRepository _dbProcedure;
        private readonly IMapper _mapper;
        protected ApiResponse _res;
        public ProceduresController(IProceduresRepository db, IMapper mapper)
        {
            _dbProcedure = db;
            _mapper = mapper;
            _res = new ApiResponse();
            
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllProcedures([FromQuery] string? search, int pageNumber=1, int pageSize=0)
        {
            try
            {
                IEnumerable<Procedures> proceduresList = await _dbProcedure.GetAllAsync(pageNumber:pageNumber, pageSize:pageSize);
                if (!string.IsNullOrEmpty(search))
                {
                    proceduresList = proceduresList.Where(u => u.Name.ToLower().Contains(search.ToLower()));
                }

                IEnumerable<Procedures> totalProcedures = await _dbProcedure.GetAllAsync();
                int proceduresCount = totalProcedures.ToList().Count();
                var totalPages = 1;
                if (pageSize > 0)
                {
                    totalPages = (int)Math.Ceiling((Decimal)proceduresCount / pageSize);
                }

                Pagination pagination = new Pagination()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = proceduresCount,
                };

                _res.Head = pagination; 
                _res.Result = _mapper.Map<List<ProceduresDTO>>(proceduresList);
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


        [HttpGet("{id:int}", Name = "GetProcedure")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProcedure(int id)
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
                if (await _dbProcedure.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<String>() { "Client not found" };
                    return NotFound(_res);
                }

                Procedures procedure = await _dbProcedure.GetAsync(u => u.Id == id);
                _res.StatusCode = HttpStatusCode.OK;
                _res.Result = _mapper.Map<ProceduresDTO>(procedure);
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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateProcedure([FromBody] ProceduresCreateDTO procedureCreate)
        {
            try
            {
                if (procedureCreate == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Procedure is null" };
                    return BadRequest(_res);
                }
                if (await _dbProcedure.GetAsync(u => u.Name == procedureCreate.Name, false) != null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Procedure already Exists" };
                    return BadRequest(_res);
                }
                Procedures procedure = _mapper.Map<Procedures>(procedureCreate);
                await _dbProcedure.CreatAsync(procedure);
                _res.Result = _mapper.Map<ProceduresDTO>(procedure);
                _res.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetProcedure", new { id = procedure.Id }, _res);
            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }


        [HttpDelete("{id:int}", Name = "DeleteProcedure")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProcedure(int id)
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
                if (await _dbProcedure.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<string>() { "Procedure not found" };
                    return NotFound(_res);
                }

                Procedures procedure = await _dbProcedure.GetAsync(u => u.Id == id);
                await _dbProcedure.DeleteAsync(procedure);
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProcedure([FromBody] ProceduresUpdateDTO prodecuresUpdate, int id)
        {

            try
            {
                if (prodecuresUpdate == null || await _dbProcedure.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Client is null" };
                    return BadRequest(_res);
                }
                if (prodecuresUpdate.Id != id || id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Invalid ID" };
                    return BadRequest(_res);
                }
                Procedures procedure = _mapper.Map<Procedures>(prodecuresUpdate);
                await _dbProcedure.UpdateAsync(procedure);
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
    }
}
