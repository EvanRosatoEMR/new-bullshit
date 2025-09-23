using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Todo.Common.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Where we configure our applications.
        //Whatever that means.

        //Creates the default builder, then builds, the runs it.

        //This is the builder pattern.
        //Creates a builder instance. Build() creates a builder class we can actually use.
        //Run then runs the builder. it's the only thing in it upon creation.

        //UseConsoleLifetime Registers a handler for Operating System signals.
        //AKA actually lets you control the application. (You send the signals idiot)

        //async yyay
        //async basically means that the program continues to run while it awaits a callback from the program,
        //rather than getting stuck on the code.
        //async and await must go together and cannot run apart.
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddTransient<IInterfacebullshit, TaskService>();

        await builder.Build().RunAsync();
    }
}