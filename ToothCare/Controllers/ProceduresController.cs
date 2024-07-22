using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ToothCare.Models;
using ToothCare.Models.DTO;
using ToothCare.Services.IServices;
using Utility;

namespace ToothCare.Controllers
{
    public class ProceduresController : Controller
    {
        private readonly IProceduresServices _procedures;
        private readonly IMapper _mapper;
        public ProceduresController(IProceduresServices procedures, IMapper mapper)
        {
            _mapper = mapper;
            _procedures = procedures;
        }

        [HttpGet]
        public async Task<IActionResult> IndexProcedures(string? search, int pageNumber =1 , int pageSize = 3)
        {
            List<ProceduresDTO> proceduresList = new();
            var res = await _procedures.GetAllAsync<ApiResponse>(search, pageNumber, pageSize, HttpContext.Session.GetString(SD.SessionToken));
            Pagination head = JsonConvert.DeserializeObject<Pagination>(Convert.ToString(res.Head));
            proceduresList = JsonConvert.DeserializeObject<List<ProceduresDTO>>(Convert.ToString(res.Result));
            ViewModel<ProceduresDTO> viewRes = new ViewModel<ProceduresDTO>()
            {
                lists = proceduresList,
                pagination = head,
            };
            return View(viewRes);
        }
        //public async Task<IActionResult> GetAllProcedures([FromQuery] string? search, int pageNumber = 1, int pageSize = 0)
    }
}
