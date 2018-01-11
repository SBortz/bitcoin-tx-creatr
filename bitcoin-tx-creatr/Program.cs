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

		        var formatOption = command.Option("-j|--json",
			        "Format as json.",
			        CommandOptionType.NoValue);

				command.OnExecute(() =>
		        {
			        Console.WriteLine("Here is your transaction:");

			        var tx = new Transaction();

			        if (formatOption.Value() == "on")
			        {
				        Console.WriteLine(tx);
			        }
			        else
			        {
				        Console.WriteLine(tx.ToHex());
			        }

			        return 0;
		        });
	        });

	        app.Command("show", (command) =>
	        {
		        command.Description = "Takes a raw transaction and shows it as json.";
		        command.HelpOption("-?|-h|--help");

		        var rawTx = command.Argument("[rawtransaction]",
			        "Encoded binary raw transaction.");

		        command.OnExecute(() =>
		        {
			        if (string.IsNullOrWhiteSpace(rawTx.Value))
			        {
				        Console.WriteLine("Please provide a transaction.");
				        return 1;
			        }

			        Console.WriteLine("Here is your transaction:");

			        var tx = new Transaction(rawTx.Value);
			        Console.WriteLine(tx);

			        return 0;
		        });

	        });

			app.Execute(args);
		}
	}
}
