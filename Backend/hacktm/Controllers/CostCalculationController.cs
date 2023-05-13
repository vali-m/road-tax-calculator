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
        private readonly IStreetsRepository _streetsRepository;

        public CostCalculationController(IStreetsRepository streetsRepository)
        {
            _streetsRepository = streetsRepository;
        }

        [HttpPost("distance")]
        public ActionResult<CostResponseModel> CalculateForRoute([FromBody] RouteModel model)
        {
            CostResponseModel cost = new CostResponseModel();
            double biggestDiffrenceOfTonage = 0;
            
            List<StreetModel> lowestTonageStreets = new List<StreetModel>();
            foreach (var street in model.Streets)
            {
                var streetFromDb = _streetsRepository.FindByName(street.Name);
                if (streetFromDb == null)
                {
                    continue;
                }

                if (streetFromDb.Accessible == false)
                {
                    return BadRequest($"The street with name {street.Name} is not accessible.");
                }

                if (streetFromDb.MaxWeight < model.WeightInTons)
                {
                    var diffrenceOfTonage = model.WeightInTons - streetFromDb.MaxWeight;
                    if (diffrenceOfTonage > biggestDiffrenceOfTonage)
                    {
                        biggestDiffrenceOfTonage = diffrenceOfTonage;
                        lowestTonageStreets.Clear();
                        lowestTonageStreets.Add(street);
                    }
                    else if (diffrenceOfTonage == biggestDiffrenceOfTonage)
                    {
                        lowestTonageStreets.Add(street);
                    }
                }
            }

            cost.Cost = biggestDiffrenceOfTonage * 10 * model.RouteLengthInKm;
            cost.HighestCostStreets = lowestTonageStreets;
            cost.CostPerKm = model.RouteLengthInKm / cost.Cost;
            return cost;
        }

        [HttpPost("day")]
        public ActionResult<CostResponseModel> CalculateForDay([FromBody] VehicleModel weight)
        {
            if(weight == null) 
            {
                return BadRequest("No weight provided");
            }
            CostResponseModel cost = new CostResponseModel();
            if (weight.WeightInTons <= 3.5)
            {
                cost.Cost = 0;
            }
            else if(weight.WeightInTons > 3.5 && weight.WeightInTons <= 5)
            {
                cost.Cost = 50;
            }
            else if (weight.WeightInTons > 5 && weight.WeightInTons <= 7.5)
            {
                cost.Cost = 75;
            }
            else if (weight.WeightInTons > 7.5 && weight.WeightInTons <= 10)
            {
                cost.Cost = 100;
            }
            else
            {
                cost.Cost = 150;
            }

            return cost;
        }
    }
}
