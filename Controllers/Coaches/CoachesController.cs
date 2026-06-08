using Microsoft.AspNetCore.Mvc;
using HFNC_Coaches.BLL.Coaches;
using HFNC_Coaches.Data.DTO.Coaches;

namespace HFNC_Coaches.Controllers.Coaches
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachesBLL _coachesBLL;

        public CoachesController(ICoachesBLL coachesBLL)
        {
            _coachesBLL = coachesBLL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coaches = await _coachesBLL.GetAllCoachesAsync();
            return Ok(coaches);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var coach = await _coachesBLL.GetCoachByIdAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return Ok(coach);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCoachDTO coach)
        {
            var created = await _coachesBLL.CreateCoachAsync(coach);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}
