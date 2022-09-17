using DogsWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPI.Controllers
{
    [ApiController]
    [Route("api/kennels")]
    public class KennelsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public KennelsController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kennel>>> GetAll()
        {
            return await dbContext.Kennels.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Kennel>> GetById(int id)
        {
            return await dbContext.Kennels.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Kennel kennel)
        {
            var existsDog = await dbContext.Dogs.AnyAsync(x => x.Id == kennel.DogId);

            if (!existsDog)
            {
                return BadRequest($"No existe el perro con el id: {kennel.DogId} ");
            }

            dbContext.Add(kennel);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Kennel kennel, int id)
        {
            var exist = await dbContext.Kennels.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("La perrera especificada no existe:(");
            }

            if (kennel.Id != id)
            {
                return BadRequest("El id de la perrera no coincide con el establecido en la url");
            }

            dbContext.Update(kennel);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Kennels.AnyAsync(x => x.Id == id);

            if(!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            dbContext.Remove(new Kennel { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
