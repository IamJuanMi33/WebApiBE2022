using DogsWebAPISeg.Entities;
using DogsWebAPISeg.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DogsWebAPISeg.Controllers
{
    [ApiController]
    [Route("dogs")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class DogsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;  

        public DogsController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = context;
            this.mapper = mapper;   
            this.configuration = configuration;          
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetDogDTO>>> Get()
        {
            var dogs = await dbContext.Dogs.ToListAsync();
            return mapper.Map<List<GetDogDTO>>(dogs);
        }

        [HttpGet("{id:int}", Name = "obtenerPerro")]
        public async Task<ActionResult<DogDTOWithKennels>> Get(int id)
        {
            var dog = await dbContext.Dogs
                .Include(dogDB => dogDB.DogKennel)
                .ThenInclude(dogKennelDB => dogKennelDB.Kennel)
                .FirstOrDefaultAsync(dogDB => dogDB.Id == id);

            if (dog == null)
            {
                return NotFound();
            }

            return mapper.Map<DogDTOWithKennels>(dog);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<GetDogDTO>>> Get([FromRoute] string nombre)
        {
            var dogs = await dbContext.Dogs.Where(dogDB => dogDB.Name.Contains(nombre)).ToListAsync();

            return mapper.Map<List<GetDogDTO>>(dogs);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DogDTO dogDto)
        {
            var existePerroMismoNombre = await dbContext.Dogs.AnyAsync(x => x.Name == dogDto.Name);

            if (existePerroMismoNombre)
            {
                return BadRequest($"Ya existe un perro con ese nombre {dogDto.Name}");
            }

            var dog = mapper.Map<Dog>(dogDto);

            dbContext.Dogs.Add(dog);
            await dbContext.SaveChangesAsync();

            var dogDTO = mapper.Map<GetDogDTO>(dog);

            return CreatedAtRoute("obtenerPerro", new { id = dog.Id }, dogDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(DogDTO dogCreacionDTO, int id)
        {
            var exist = await dbContext.Dogs.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            var dog = mapper.Map<Dog>(dogCreacionDTO);
            dog.Id = id;

            dbContext.Update(dog);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Dogs.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            dbContext.Remove(new Dog()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
