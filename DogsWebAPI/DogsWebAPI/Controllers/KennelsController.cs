using DogsWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPI.Controllers
{
    [ApiController]
    [Route("api/kennels")]
    public class KennelsController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public KennelsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kennel>>> GetAll()
        {
            return await dbContext.Kennels.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Kennel>> GetByIt(int id)
        {
            return await dbContext.Kennels.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Kennel kennel)
        {
            var existDog = await dbContext.Dogs.AnyAsync(x => x.Id == kennel.DogId);

            if (!existDog)
                return BadRequest($"No existe el perro con el ID: {kennel.DogId}");

            dbContext.Add(kennel);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Kennel kennel, int id)
        {
            var exist = await dbContext.Dogs.AnyAsync(x => x.Id == id);

            if (!exist)
                return NotFound("La perrera especificada no exste:(");

            if (kennel.Id != id)
                return BadRequest("El ID de la perrera no coincide con el establecido en la URL");

            dbContext.Update(kennel);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Kennels.AnyAsync(x => x.Id == id);

            if (!exist)
                return NotFound("El recurso no fue encontrado:(");

            dbContext.Remove(new Kennel { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
                
        }

    }
}
