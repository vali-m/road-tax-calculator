using hacktm.Entities;
using hacktm.Models;
using hacktm.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace hacktm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly IStreetsRepository _streetsRepository;

        public StreetsController(IStreetsRepository streetsRepository)
        {
            _streetsRepository = streetsRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var street = await _streetsRepository.GetAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            return Ok(new StreetModel { Name = street.Name });
        }

        public async Task<IActionResult> GetAll()
        {
            List<StreetModel> streets = new List<StreetModel>();
            List<Street> streetsDB = await _streetsRepository.GetAsync();

            foreach (var street in streetsDB)
            {
                streets.Add(new StreetModel { Name = street.Name });
            }

            return Ok(streets);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterStreetModel registerStreet)
        {
            if (_streetsRepository.FindByName(registerStreet.Name) != null)
            {
                return BadRequest("The street with this name is already registered.");
            }
            await _streetsRepository.AddOrUpdateAsync(registerStreet.ToEntity());
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateStreetModel updateStreet)
        {
            if (await _streetsRepository.GetAsync(updateStreet.Id) == null)
            {
                return NotFound("The street with this id does not exist.");
            }
            await _streetsRepository.AddOrUpdateAsync(updateStreet.ToEntity());
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _streetsRepository.GetAsync(id) == null)
            {
                return NotFound("The street with this id does not exist");
            }
            await _streetsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
