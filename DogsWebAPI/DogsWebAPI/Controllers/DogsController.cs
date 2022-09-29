using DogsWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPI.Controllers
{
    [ApiController]
    [Route("dogs")]
    public class DogsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DogsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet] //api/dog
        [HttpGet("Listado")] //api/dogs/listado
        [HttpGet("/Listado")] //Listado
        public async Task<ActionResult<List<Dog>>> Get()
        {
            return await dbContext.Dogs.Include(x => x.kennels).ToListAsync();
        }

        [HttpGet("primero")]
        public async Task<ActionResult<Dog>> FirstDog([FromHeader] int value, [FromQuery] string dog, [FromQuery] int dogId)
        {
            return await dbContext.Dogs.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]
        public ActionResult<Dog> FirstDog()
        {
            return new Dog() { Name = "DOS" };
        }

        [HttpGet("{id:int}/{param=Vito}")]
        public async Task<ActionResult<Dog>> Get(int id, string param)
        {
            var dog = await dbContext.Dogs.FirstOrDefaultAsync(x => x.Id == id);

            if(dog == null)
                return NotFound();

            return dog;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Dog>> Get([FromRoute] string nombre)
        {
            var dog = await dbContext.Dogs.FirstOrDefaultAsync(x => x.Name.Contains(nombre));

            if (dog == null)
                return NotFound();

            return dog;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Dog dog)
        {
            dbContext.Add(dog);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Dog dog, int id)
        {
            if (dog.Id != id)
            {
                return BadRequest("El id del perro no coincide con el establecido en la url");
            }

            dbContext.Update(dog);
            await dbContext.SaveChangesAsync();
            return Ok();
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
