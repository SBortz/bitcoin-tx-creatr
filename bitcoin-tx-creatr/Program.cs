using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using NBitcoin;

namespace bitcoin_tx_creatr
{
    class Program
    {
        static void Main(string[] args)
        {
	        var app = new CommandLineApplication();
	        app.Name = "bitcoin-tx-creatr";
	        app.HelpOption("-?|-h|--help");

			app.OnExecute(() =>
	        {
		        Console.WriteLine("Howdy friend, i am your bitcoin-tx-creatr. I can create bitcoin transactions manually for you.");
		        return 0;
	        });

	        app.Command("create", (command) =>
	        {
		        command.Description = "Creates a new empty tx.";
		        command.HelpOption("-?|-h|--help");

		        command.OnExecute(() =>
		        {
			        Console.WriteLine("Here is your transaction:");

			        var tx = new Transaction();
			        Console.WriteLine(tx);

			        return 0;
		        });

	        });

			app.Execute(args);
		}
	}
}
