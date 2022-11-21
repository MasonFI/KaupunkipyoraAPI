using AutoMapper;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Models.Entity;
using KaupunkipyoraAPI.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace KaupunkipyoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BikeRoutesController : BaseController
    {
        public BikeRoutesController(IUnitOfWork uow,
            IMapper mapper,
            IOptionsMonitor<APIOptions> options) : base(uow, mapper, options) { }

        // GET: api/<RoutesController>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var routes = await _UOW.BikeRouteRepository.GetAllAsync();

                return Ok(_mapper.Map<IEnumerable<BikeRouteDTO>>(routes));
            }
            catch (Exception ex)
            {
                string message = "Interla Exception";
#if DEBUG
                message += $": {ex.Message}";
#endif
                return StatusCode(500, message);
            }
        }

        // GET api/<RoutesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult?> Get(int id)
        {
            try
            {
                var route = await _UOW.BikeRouteRepository.GetByIdAsync(id);
                if (route == null)
                    return NotFound("Not found");

                return Ok(_mapper.Map<BikeRouteDTO>(route));
            }
            catch (Exception ex)
            {
                string message = "Interla Exception";
#if DEBUG
                message += $": {ex.Message}";
#endif
                return StatusCode(500, message);
            }
        }

        // POST api/<RoutesController>
        [HttpPost, Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] BikeRouteCreateDTO route)
        {
            try
            {
                var toBeCreatedRuote = _mapper.Map<BikeRoute>(route);
                var createdRoute = await _UOW.BikeRouteRepository.AddAsync(toBeCreatedRuote);

                return StatusCode(StatusCodes.Status201Created, _mapper.Map<BikeRouteDTO>(createdRoute));
            }
            catch (Exception ex)
            {
                string message = "Interla Exception";
#if DEBUG
                message += $": {ex.Message}";
#endif
                return StatusCode(500, message);
            }
        }

        // PUT api/<RoutesController>/5
        [HttpPut("{id}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(int id, [FromBody] BikeRouteUpdateDTO route)
        {
            try
            {
                var routeInDb = await _UOW.BikeRouteRepository.GetByIdAsync(id);
                if (routeInDb == null)
                    return NotFound("Not found");

                var toBeUpdatedRuote = _mapper.Map<BikeRoute>(route);
                var updatedRoute = await _UOW.BikeRouteRepository.UpdateAsync(toBeUpdatedRuote);

                return Ok(_mapper.Map<BikeRouteDTO>(updatedRoute));
            }
            catch (Exception ex)
            {
                string message = "Interla Exception";
#if DEBUG
                message += $": {ex.Message}";
#endif
                return StatusCode(500, message);
            }
        }

        // DELETE api/<RoutesController>/5
        [HttpDelete("{id}"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await _UOW.BikeRouteRepository.GetByIdAsync(id);
                if (entity == null)
                    return NotFound();

                var deletedRows = await _UOW.BikeRouteRepository.DeleteAsync(entity.Id);
                if (deletedRows < 1)
                    throw new Exception("No rows deleted");

                return Ok();
            }
            catch (Exception ex)
            {
                string message = "Interla Exception";
#if DEBUG
                message += $": {ex.Message}";
#endif
                return StatusCode(500, message);
            }
        }
    }
}
