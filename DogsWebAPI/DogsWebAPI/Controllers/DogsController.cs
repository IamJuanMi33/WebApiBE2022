using DogsWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPI.Controllers
{
    [ApiController]
    [Route("dogs")]
    public class DogsController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DogsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dog>>> Get()
        {
            //return await dbContext.Dogs.ToListAsync();
            return await dbContext.Dogs.Include(x => x.kennels).ToListAsync;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Dog dog)
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
