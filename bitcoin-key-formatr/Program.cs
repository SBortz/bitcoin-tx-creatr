using System;
using Autofac;
using bitcoin_tx_creatr;
using CommandDotNet;
using CommandDotNet.IoC.Autofac;

namespace bitcoin_key_formatr
{
    class Program
    {
        static int Main(string[] args)
        {
	        try
	        {
		        ContainerBuilder containerBuilder = new ContainerBuilder();
		        containerBuilder.RegisterType<BitcoinKeyFormatter>().As<IBitcoinKeyFormatter>();
		        containerBuilder.RegisterType<KeyConsoleWriter>().As<IKeyConsoleWriter>();
		        IContainer container = containerBuilder.Build();

		        AppRunner<ConsoleController> appRunner = new AppRunner<ConsoleController>().UseAutofac(container);
		        return appRunner.Run(args);
	        }
	        catch (Exception ex)
	        {
		        Console.WriteLine("Something wrent wrong: " + ex.Message + ", " + ex.StackTrace);
		        return 1;
	        }
        }
    }
}
