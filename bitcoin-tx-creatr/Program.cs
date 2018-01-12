using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CommandDotNet;
using CommandDotNet.IoC.Autofac;
using Microsoft.Extensions.CommandLineUtils;
using NBitcoin;

namespace bitcoin_tx_creatr
{
    class Program
    {
        static int Main(string[] args)
        {
	        try
	        {
		        ContainerBuilder containerBuilder = new ContainerBuilder();
		        containerBuilder.RegisterType<BitcoinTxCreatr>().As<IBitcoinTxCreatr>();
		        containerBuilder.RegisterType<BitcoinTxCreatrStringOutput>().As<IBitcoinTxCreatrStringOutput>();
				containerBuilder.RegisterType<TransactionConsoleWriter>().As<ITransactionConsoleWriter>();
				IContainer container = containerBuilder.Build();

		        AppRunner<ConsoleController> appRunner = new AppRunner<ConsoleController>().UseAutofac(container);
		        return appRunner.Run(args);
	        }
	        catch(Exception ex)
	        {
		        Console.WriteLine("Something wrent wrong: " + ex.Message + ", " + ex.StackTrace);
		        return 1;
	        }
		}
	}
}
