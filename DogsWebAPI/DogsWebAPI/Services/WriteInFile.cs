namespace DogsWebAPI.Services
{
    public class WriteInFile : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string fileName = "Archivo 1.txt";
        private Timer timer;

        public WriteInFile(IWebHostEnvironment env)
        {
            this.env = env; 
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Se ejecuta cuando cargamos la aplicacion 1 vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Escribir("Proceso iniciado: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            //Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //Se ejecuta cuando detenemos la aplicacion aunque puede que no se ejecute por algún erro
            timer.Dispose();
            Escribir("Proceso finalizado: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            //Escribir("Proceso Finalizado");
            return Task.CompletedTask;
        }

        public void DoWork(object state)
        {
            Escribir("Estado de las perreras: correcto");
        }

        private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{fileName}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }
    }
}
