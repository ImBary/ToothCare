using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToothCareAPI.Model;
using ToothCareAPI.Model.DTO;
using ToothCareAPI.Repository.IRepository;

namespace ToothCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        protected ApiResponse _res;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _res = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.user == null || string.IsNullOrEmpty(loginResponse.Token)) 
            {
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.ErrorMessages.Add("Username or password is incorecct");
                _res.IsSuccess = false;
                return BadRequest(_res);
            }
            _res.StatusCode=HttpStatusCode.OK;
            _res.Result = loginResponse;
            return Ok(_res);
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
        {
            bool isUserNameUnique = _userRepo.isUnique(model.UserName);
            if (!isUserNameUnique)
            {
                _res.IsSuccess = false;
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.ErrorMessages.Add("Username already exitsts");
                return BadRequest(_res);
            }
            var user = await _userRepo.Register(model);
            if (user == null) 
            {
                _res.IsSuccess = false;
                _res.StatusCode = HttpStatusCode.BadRequest;
                _res.ErrorMessages.Add("Error while registration");
                return BadRequest(_res);
            }
            _res.StatusCode = HttpStatusCode.OK;
            return Ok(_res);
        }

    }
}
