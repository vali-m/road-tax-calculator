using hacktm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hacktm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostCalculationController : ControllerBase
    {
        [HttpPost("distance")]
        public CostResponseModel CalculateForRoute([FromBody] RouteModel model)
        {
            return new CostResponseModel();
        }

        [HttpPost("day")]
        public CostResponseModel CalculateForDay([FromBody] VehicleModel weight)
        {
            return new CostResponseModel();
        }
    }
}
