using AutoMapper;
using FreeGym.API.Dtos;
using FreeGym.Core.Entities;
using FreeGym.Core.Results;
using FreeGym.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeGym.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/muscles")]
    [Authorize]
    public class MusclesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MusclesService _musclesService;

        public MusclesController(IMapper mapper, MusclesService musclesService)
        {
            _mapper = mapper;
            _musclesService = musclesService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> FindAsync([FromRoute] int id)
        {
            var muscle = await _musclesService.FindAsync(id);

            if (muscle == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MuscleDto>(muscle));
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedAsync([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string search = "")
        {
            var pagedMuscles = await _musclesService.GetPagedAsync(skip, take, search);

            if (pagedMuscles.Data.Count() == 0)
            {
                return Ok(pagedMuscles);
            }

            var muscles = _mapper.Map<ICollection<MuscleDto>>(pagedMuscles.Data);

            return Ok(new PagedResult<MuscleDto>(muscles, pagedMuscles.Total));
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] MuscleDto muscleDto)
        {
            var muscle = _mapper.Map<Muscle>(muscleDto);

            await _musclesService.InsertAsync(muscle);

            if (_musclesService.IsValid)
            {
                return Created($"muscles/{muscle.Id}", muscle);
            }

            return BadRequest(_musclesService.Errors);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] MuscleDto muscleDto)
        {
            var muscle = _mapper.Map<Muscle>(muscleDto);

            await _musclesService.Update(id, muscle);

            if (_musclesService.IsValid)
            {
                return NoContent();
            }

            return BadRequest(_musclesService.Errors);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            await _musclesService.Remove(id);

            if (_musclesService.IsValid)
            {
                return NoContent();
            }

            return BadRequest(_musclesService.Errors);
        }
    }
}
