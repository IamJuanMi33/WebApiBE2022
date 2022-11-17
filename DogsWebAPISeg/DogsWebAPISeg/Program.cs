//using DogsWebAPISeg;
//using DogsWebAPISeg.CustomConfigurationProvider;

//var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddSecurityConfiguration();

//var startup = new StartUp(builder.Configuration);
//startup.ConfigureServices(builder.Services);


//var app = builder.Build();

//var serviceLogger = (ILogger<StartUp>)app.Services.GetService(typeof(ILogger<StartUp>));

//startup.Configure(app, app.Environment, serviceLogger);

//app.Run();

class Program
{
    static void Main(string[] args)
    {
        //program execution starts from here
        System.Console.WriteLine("Total Arguments: {0}", args.Length);

        System.Console.Write("Arguments: ");

        foreach (var arg in args)
            System.Console.Write(arg + ", ");
    }
}
