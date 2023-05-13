using hacktm.Entities;
using hacktm.Models;
using hacktm.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hacktm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostCalculationController : ControllerBase
    {
        private readonly IStreetsRepository _repository;

        public CostCalculationController(IStreetsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("distance")]
        public CostResponseModel CalculateForRoute([FromBody] RouteModel model)
        {
            var streets = new List<Street>();
            foreach (var s in model.Streets)
            {
                var str = _repository.FindByName(s.Name);
                if (str != null)
                    streets.Add(_repository.FindByName(s.Name));
            }

            streets = streets.Where(s => s.MaxWeight < model.WeightInTons).ToList();
            streets = streets.DistinctBy(s => s.Name).ToList();

            return new CostResponseModel()
            {
                Cost = streets.Count() * 10,
                HighestCostStreets = streets.Select(s => new StreetModel { Name = s.Name }).ToList(),
                CostPerKm = streets.Count() * 10 / model.RouteLengthInKm 
            };
        }

        [HttpPost("day")]
        public CostResponseModel CalculateForDay([FromBody] VehicleModel weight)
        {
            return new CostResponseModel();
        }
    }
}
