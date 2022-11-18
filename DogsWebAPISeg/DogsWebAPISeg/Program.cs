using DogsWebAPISeg;
//using dogswebapiseg.customconfigurationprovider;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.addsecurityconfiguration();

var startup = new StartUp(builder.Configuration);
startup.ConfigureServices(builder.Services);


var app = builder.Build();

var servicelogger = (ILogger<StartUp>)app.Services.GetService(typeof(ILogger<StartUp>));

startup.Configure(app, app.Environment, servicelogger);

app.Run();

//class Program
//{
//    static void Main(string[] args)
//    {
//        //program execution starts from here
//        System.Console.WriteLine("Total Arguments: {0}", args.Length);

//        System.Console.Write("Arguments: ");

//        foreach (var arg in args)
//            System.Console.Write(arg + ", ");
//    }
//}
