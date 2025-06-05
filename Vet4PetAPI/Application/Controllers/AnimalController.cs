using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Service.Interfaces;
using Domain;
using System.Collections.Generic;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAll()
        {
            var animals = await _animalService.GetAllAnimalsAsync();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetById(int id)
        {
            var animal = await _animalService.GetAnimalByIdAsync(id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        public async Task<ActionResult<Animal>> Create([FromBody] Animal animal)
        {
            var createdAnimal = await _animalService.CreateAnimalAsync(animal);
            return CreatedAtAction(nameof(GetById), new { id = createdAnimal.Id }, createdAnimal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Animal animal)
        {
            if (id != animal.Id) return BadRequest();
            await _animalService.UpdateAnimalAsync(animal);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _animalService.DeleteAnimalAsync(id);
            return NoContent();
        }
    }
} 