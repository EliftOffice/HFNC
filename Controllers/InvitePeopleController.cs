using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using HFNC_Coaches.Business.BLL;
using HFNC_Coaches.Data.DTO;

namespace HFNC_Coaches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitePeopleController : ControllerBase
    {
        private readonly InvitePeopleBLL _bll;

        public InvitePeopleController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? "server=localhost;port=3306;database=hfnc;user=root;password=Hfnc@2026;";
           // string connectionString = "server=mibu9ojpzcmahn1uxysoyoqf;port=3306;database=hfnc;user=root;password=Hfnc@2026;";
            _bll = new InvitePeopleBLL(connectionString);
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            List<InvitePeopleDTO> data = _bll.GetAllPeople();
            return Ok(data);
        }
    }
}