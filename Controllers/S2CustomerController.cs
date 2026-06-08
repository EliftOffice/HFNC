using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;
using HFNC_Coaches.Business.BLL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S2CustomerController : ControllerBase
    {
        private readonly S2CustomerBLL _bll;

        public S2CustomerController(IConfiguration configuration)
        {
             string connectionString = "server=mibu9ojpzcmahn1uxysoyoqf;port=3306;database=hfnc;user=root;password=Hfnc@2026;";
           // string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "server=localhost;port=3306;database=hfnc;user=root;password=Hfnc@2026;";
            _bll = new S2CustomerBLL(connectionString);
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody] S2CustomerDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest("Invalid data");
                bool isSaved = _bll.AddCustomer(dto);
                if (isSaved) return Ok(new { message = "S2 Customer saved successfully!" });
                return StatusCode(500, "Error saving S2 Customer data");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save S2Customer Controller Error: {ex.Message}");
                return StatusCode(500, new { message = $"An internal error occurred: {ex.Message}" });
            }
        }

        [HttpGet("stats/active-last-30-days")]
        public IActionResult GetActiveLast30DaysStats()
        {
            try
            {
                int count = _bll.GetActiveCustomersLast30Days();
                return Ok(new { activeCustomersLast30Days = count });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stats S2Customer Controller Error: {ex.Message}");
                return StatusCode(500, new { message = $"An internal error occurred: {ex.Message}" });
            }
        }
    }
}