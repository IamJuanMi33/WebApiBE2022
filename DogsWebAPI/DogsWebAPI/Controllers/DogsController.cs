using DogsWebAPI.Entities;
using DogsWebAPI.Filters;
using DogsWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPI.Controllers
{
    [ApiController]
    [Route("api/dogs")]
    //[Authorize]
    public class DogsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<DogsController> logger;

        public DogsController(ApplicationDbContext dbContext, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<DogsController> logger)
        {
            this.dbContext = dbContext;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(ActionFilter))]
        public ActionResult ObtenerGuid()
        {
            logger.LogInformation("Durante la ejecución");
            return Ok(new
            {
                DogsContrllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                DogsControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                DogsControllerSinleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton(),
            });
        }

        [HttpGet]
        [HttpGet("listado")] 
        [HttpGet("/listado")]
        //[ResponseCache(Duration = 15)]
        //[Authorize]
        //[ServiceFilter(typeof(ActionFilter))]
        public async Task<ActionResult<List<Dog>>> Get()
        {
            throw new NotImplementedException();
            logger.LogInformation("Se obtitene el listado de perros");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
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

        //[HttpGet("{id:int}/{param=Vito}")]
        [HttpGet("{param?}")] //Se puede usar ? para que no sea obligatorio el parámetro
        public async Task<ActionResult<Dog>> Get(int id, string param)
        {
            var dog = await dbContext.Dogs.FirstOrDefaultAsync(x => x.Id == id);

            if(dog == null)
                return NotFound();

            return dog;
        }

        [HttpGet("obtenerDog/{nombre}")]
        public async Task<ActionResult<Dog>> Get([FromRoute] string nombre)
        {
            var dog = await dbContext.Dogs.FirstOrDefaultAsync(x => x.Name.Contains(nombre));

            if (dog == null)
            {
                logger.LogError("No se encontró el perro");
                return NotFound();
            }

            return dog;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Dog dog)
        {
            //Validar desde el controlador
            var existePerroMismoNombre = await dbContext.Dogs.AnyAsync(x => x.Name == dog.Name);

            if (existePerroMismoNombre)
            {
                return BadRequest("Ya existe un perro con ese nombre");
            }    
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
