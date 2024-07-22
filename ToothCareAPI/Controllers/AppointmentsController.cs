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
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsRepository _dbApp;
        private readonly IMapper _mapper;
        protected ApiResponse _res;

        public AppointmentsController(IAppointmentsRepository dbApp, IMapper mapper)
        {
            _mapper = mapper;
            _dbApp = dbApp;
            _res = new();
        }


        [HttpGet]
        [Authorize(Roles = "admin,staff,client")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllAppointments([FromQuery] int? search)
        {
            try
            {
                IEnumerable<Appointments> appointmentsList = await _dbApp.GetAllAsync();
                if (search != null && search >0)
                {
                    appointmentsList = appointmentsList.Where(u => u.ClientId == search);
                }
                _res.Result = _mapper.Map<List<AppointmentsDTO>>(appointmentsList);
                return Ok(appointmentsList);

            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:int}", Name = "GetAppointment")]
        [Authorize(Roles = "admin,staff,client")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAppointment(int id)
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
                if (await _dbApp.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<String>() { "Appointment not found" };
                    return NotFound(_res);
                }

                Appointments appointments = await _dbApp.GetAsync(u => u.Id == id);
                _res.StatusCode = HttpStatusCode.OK;
                _res.Result = _mapper.Map<AppointmentsDTO>(appointments);
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
        [Authorize(Roles = "admin,staff,client")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAppointement([FromBody] AppointmentsCreateDTO appointmentCreate)
        {
            try
            {
                if (appointmentCreate == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Appointment is null" };
                    return BadRequest(_res);
                }
                if (appointmentCreate.ClientId <= 0 || appointmentCreate.DoctorId <= 0 || appointmentCreate.ProcedureId<=0) 
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Appointment has invalid ids" };
                    return BadRequest(_res);
                }
                if (await _dbApp.GetAsync(u => u.Number == appointmentCreate.Number, false) != null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Appointment already Exists" };
                    return BadRequest(_res);
                }
                Appointments appointment = _mapper.Map<Appointments>(appointmentCreate);
                await _dbApp.CreatAsync(appointment);
                _res.Result = _mapper.Map<AppointmentsDTO>(appointment);
                _res.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetAppointment", new { id = appointment.Id }, _res);
            }
            catch (Exception ex)
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.IsSuccess = false;
                _res.ErrorMessages = new List<string>() { ex.Message.ToString() };
                return BadRequest(ex);
            }
        }



        [HttpDelete("{id:int}", Name = "DeleteAppointment")]
        [Authorize(Roles = "admin,staff,client")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAppointment(int id)
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
                if (await _dbApp.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.NotFound;
                    _res.ErrorMessages = new List<string>() { "Appointment not found" };
                    return NotFound(_res);
                }

                Appointments appointment = await _dbApp.GetAsync(u => u.Id == id);
                await _dbApp.DeleteAsync(appointment);
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "admin,staff,client")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentsUpdateDTO appointmentUpdate, int id)
        {

            try
            {
                if (appointmentUpdate == null || await _dbApp.GetAsync(u => u.Id == id, false) == null)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Appointment is null" };
                    return BadRequest(_res);
                }
                if (appointmentUpdate.Id != id || id <= 0)
                {
                    _res.IsSuccess = false;
                    _res.StatusCode = HttpStatusCode.BadRequest;
                    _res.ErrorMessages = new List<string>() { "Invalid ID" };
                    return BadRequest(_res);
                }
                Appointments client = _mapper.Map<Appointments>(appointmentUpdate);
                await _dbApp.UpdateAsync(client);
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
