using System;
using System.Collections.Generic;
using System.Linq;
using CommandDotNet;
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
		        AppRunner<BitcoinTxCreatr> appRunner = new AppRunner<BitcoinTxCreatr>();
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
