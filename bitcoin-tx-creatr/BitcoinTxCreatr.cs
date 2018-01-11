using System;
using System.Collections.Generic;
using System.Text;
using CommandDotNet.Attributes;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	[ApplicationMetadata(Description = "Howdy friend, i am your bitcoin-tx-creatr.", ExtendedHelpText = "I can create bitcoin transactions manually for you.")]
	public class BitcoinTxCreatr
    {
	    [ApplicationMetadata(Description = "Creates an empty transaction")]
		public void Create([Option(LongName = "json", ShortName = "j")]bool json = false)
	    {
			var tx = new Transaction();

		    if (json)
		    {
				Console.WriteLine(tx.ToString());
			}
		    else
		    {
				Console.WriteLine(tx.ToHex());
			}
		}

	    [ApplicationMetadata(Description = "Takes a raw transaction and returns it in json format.", ExtendedHelpText = "\nTakes a raw transaction and returns it in json format.")]
		public int Show([Argument(Description = "Raw transaction in hex format")]string transactionHex)
	    {
			if (string.IsNullOrWhiteSpace(transactionHex))
		    {
			    Console.WriteLine("Please provide a transaction.");
			    return 1;
		    }

			try
		    {
			    var tx = new Transaction(transactionHex);
			    Console.WriteLine("Here is your transaction:");
				Console.WriteLine(tx);
			    return 0;
			}
		    catch (Exception ex)
		    {
			    Console.WriteLine("Invalid hex string");
			    return 1;
		    }
		}
    }
}
