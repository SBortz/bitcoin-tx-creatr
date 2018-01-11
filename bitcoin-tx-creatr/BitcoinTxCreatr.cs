using System;
using System.Collections.Generic;
using System.Text;
using CommandDotNet.Attributes;
using NBitcoin;

namespace bitcoin_tx_creatr
{
    public class BitcoinTxCreatr
    {
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

	    public int Show(string hexInput)
	    {
			if (string.IsNullOrWhiteSpace(hexInput))
		    {
			    Console.WriteLine("Please provide a transaction.");
			    return 1;
		    }

		    Console.WriteLine("Here is your transaction:");

		    var tx = new Transaction(hexInput);
		    Console.WriteLine(tx);

		    return 0;
		}
    }
}
