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
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IDoctorsRepository _dbDoctors;
        private readonly IMapper _mapper;
        protected ApiResponse _res;

        public DoctorsController(IDoctorsRepository db, IMapper mapper)
        {
            _dbDoctors = db;
            _mapper = mapper;
            _res = new();

        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllDoctors([FromQuery] string? search, int pageNumber = 1, int pageSize = 0)
        {
            try
            {
                IEnumerable<Doctors> doctorList = await _dbDoctors.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                if (!string.IsNullOrEmpty(search))
                {
                    doctorList = doctorList.Where(u => u.Specialty.ToLower().Contains(search.ToLower()));
                }
                
                IEnumerable<Doctors> doctorCounnt = await _dbDoctors.GetAllAsync();
                var totalDoctors = doctorCounnt.ToList().Count();
                var totalPages = 1;
                if (pageSize > 0)
                {
                    totalPages = (int)Math.Ceiling((Decimal)totalDoctors / pageSize);//+1??
                }

                Pagination pagination = new Pagination()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = totalPages
                };

                _res.Head = pagination;
                _res.Result = _mapper.Map<List<DoctorsDTO>>(doctorList);
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

        [HttpGet("{id:int}", Name = "GetDoctor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDoctor(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<String>() { "Invalid id" };
                    return BadRequest(_res);
                }
                if(await _dbDoctors.GetAsync(u=>u.Id == id,false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<String>() { "Doctor not found" };
                    return NotFound(_res);

                }
                Doctors doctor = await _dbDoctors.GetAsync(u => u.Id == id);
                _res.Result = _mapper.Map<DoctorsDTO>(doctor);
                _res.StatusCode = HttpStatusCode.OK;
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorsCreateDTO doctorsCreate)
        {
            try
            {
                if (doctorsCreate == null) 
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<String>() { "doctor is null" };
                    return BadRequest(_res);
                }
                if(await  _dbDoctors.GetAsync(u => u.Name == doctorsCreate.Name && u.Surname == doctorsCreate.Surname,false) != null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<String>() { "doctor already exitsts" };
                    return BadRequest(_res);
                }
                Doctors doctor = _mapper.Map<Doctors>(doctorsCreate);
                await _dbDoctors.CreatAsync(doctor);
                _res.Result = _mapper.Map<DoctorsDTO>(doctor);
                _res.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetDoctor",new {id = doctor.Id},_res);



            }catch(Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id:int}",Name ="DeleteDoctor")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(203)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<String>() { "Invalid id" };
                    return BadRequest(_res);
                }
                if (await _dbDoctors.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<String>() { "Doctor not found" };
                    return NotFound(_res);

                }
                Doctors doctor = await _dbDoctors.GetAsync(u => u.Id == id);
                await _dbDoctors.DeleteAsync(doctor); 
                _res.StatusCode = HttpStatusCode.OK;
                return Ok(doctor);
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
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorsUpdateDTO doctorsUpdate, int id)
        {

            try
            {
                if (doctorsUpdate == null || await _dbDoctors.GetAsync(u => u.Id == id,false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Client is null" };
                    return BadRequest(_res);
                }
                if (doctorsUpdate.Id != id || id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Invalid ID" };
                    return BadRequest(_res);
                }
                Doctors doctor = _mapper.Map<Doctors>(doctorsUpdate);
                await _dbDoctors.UpdateAsync(doctor);
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
