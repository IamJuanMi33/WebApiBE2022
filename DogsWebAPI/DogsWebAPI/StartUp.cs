using DogsWebAPI.Middlewares;
using DogsWebAPI.Services;
using DogsWebAPI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace DogsWebAPI
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //service.AddControllers().AddJsonOptions(x =>
            //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //Configura ApplicationDbContext como servicio
            service.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //Transient da una nueva instancia de la clase declarada,
            //sirve para funciones que ejecutan una funcionalidad y listo, sin tener que
            //mantener información que será reutilizada en otro lugar
            service.AddTransient<IService, ServiceA>();
            service.AddTransient<ServiceTransient>();

            //El tiempo de vida de la clase declarada aumenta, sin embargo, Scoped da 
            //diferentes instancias de acuerdo a cada quien mande la solicitud
            service.AddScoped<ServiceScoped>();

            //Se tiene la misma instancia siempre para todos los usuarios en todos los días.
            //Todos los usuarios que hagan una petición van a tener la misma info compartida entre si
            service.AddSingleton<ServiceSingleton>();
            service.AddTransient<ActionFilter>();
            service.AddHostedService<WriteInFile>();
            service.AddResponseCaching();

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DgosWebAPI", Version = "v1" });
            }); 

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<StartUp> logger) 
        {

            //object value = app.UseResponseHttpMiddleware();
            //Retorna un JSON de la informacion
            //app.Use(async (context, siguiente) =>
            //{
            //    using (var ms = new MemoryStream())
            //    {
            //        //Se asigna el body del response en una variable y se le da el valor de memory stream
            //        var bodyOriginal = context.Response.Body;
            //        context.Response.Body = ms;

            //        //Permite continuar con la linea 
            //        await siguiente.Invoke();

            //        //Guardamos lo que le respondemos al cliente en el string
            //        ms.Seek(0, SeekOrigin.Begin);
            //        string response = new StreamReader(ms).ReadToEnd();
            //        ms.Seek(0, SeekOrigin.Begin);

            //        //Leemos el stream y lo colocamos como estaba
            //        await ms.CopyToAsync(bodyOriginal);
            //        context.Response.Body = bodyOriginal;

            //        logger.LogInformation(response);
            //    }
            //});

            //Método para utilizar la clase Middleware propia
            //app.UseMiddleware<ResponseHttpMiddleware>();

            //Método para utilizar la clase Middleware sin exponer la clase
            app.UseResponseHttpMiddleware();


            app.Map("/mapping", app =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Interceptando las peticiones");
                });
            });

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Interceptando las peticiones");
            //});


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
