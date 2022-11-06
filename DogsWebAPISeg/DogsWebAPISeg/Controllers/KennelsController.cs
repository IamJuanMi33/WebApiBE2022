using AutoMapper;
using DogsWebAPISeg.DTOs;
using DogsWebAPISeg.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPISeg.Controllers
{
    [ApiController]
    [Route("kennels")]
    public class KennelsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public KennelsController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/listadoPerrera")]
        public async Task<ActionResult<List<Kennel>>> GetAll()
        {
            return await dbContext.Kennels.ToListAsync();
        }

        //[HttpGet("{id:int}", Name = "obtenerPerrera")]
        //public async Task<ActionResult<KennelDTOWithDogs>> GetByIt(int id)
        //{
        //    var kennel = await dbContext.Kennels
        //        .Include(kennelDB => kennelDB.DogKennel)
        //        .ThenInclude(dogKennelDB => dogKennelDB.Dog)
        //        .Include(batchDB => batchDB.Batch)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    if (kennel == null)
        //    {
        //        return NotFound();
        //    }

        //    kennel.DogKennel = kennel.DogKennel.OrderBy(x => x.Order).ToList();

        //    return mapper.Map<KennelDTOWithDogs>(kennel);
        //}

        [HttpPost]
        public async Task<ActionResult> Post(KennelCreationDTO kennelCreacionDTO)
        {
            if (kennelCreacionDTO.DogsIds == null)
            {
                return BadRequest("No se puede crear una perrera sin perros");
            }

            var dogIds = await dbContext.Dogs
                .Where(dogDB => kennelCreacionDTO.DogsIds.Contains(dogDB.Id)).Select(x => x.Id).ToListAsync();

            if (kennelCreacionDTO.DogsIds.Count != dogIds.Count)
            {
                return BadRequest("No existe uno de los perros enviados");
            }

            var kennel = mapper.Map<Kennel>(kennelCreacionDTO);

            OrderByDogs(kennel);

            dbContext.Add(kennel);
            await dbContext.SaveChangesAsync();

            var kennelDTO = mapper.Map<KennelDTO>(kennel);

            return CreatedAtRoute("obtenerPerrera", new { id = kennel.Id }, kennelDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(KennelCreationDTO kennelCreacionDTO, int id)
        {
            var kennelDB = await dbContext.Kennels
                .Include(x => x.DogKennel)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (kennelDB == null)
            {
                return NotFound();
            }

            kennelDB = mapper.Map(kennelCreacionDTO, kennelDB);

            OrderByDogs(kennelDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Kennels.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El recurso no fue encontrado:(");
            }

            dbContext.Remove(new Kennel { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        private void OrderByDogs(Kennel kennel)
        {
            if (kennel.DogKennel != null)
            {
                for (int i = 0; i < kennel.DogKennel.Count; i++)
                {
                    kennel.DogKennel[i].Order = i;
                }
            }
        }
    }
}
