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
		public void Create()
	    {
		    var tx = new Transaction();
		    WriteTransaction(tx);
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
				WriteTransaction(tx);
				return 0;
			}
		    catch (Exception ex)
		    {
			    Console.WriteLine("Invalid hex string");
			    return 1;
		    }
		}

	    public int AddIn([Argument(Description = "Adds an unspent transaction (UTXO) to raw transaction")]string transactionHex, string txId, int index)
	    {
		    var outTxId = new uint256(txId);
			var outPoint = new OutPoint(outTxId, index);

			var txIn = new TxIn(outPoint);

			var tx = new Transaction(transactionHex);
			tx.Inputs.Add(txIn);

		    WriteTransaction(tx);

			return 0;
	    }

	    private static void WriteTransaction(Transaction tx)
	    {
		    Console.WriteLine("Here is your transaction (json)");
		    Console.WriteLine(tx.ToString());
		    Console.WriteLine();
		    Console.WriteLine("Here is your transaction (hex)");
		    Console.WriteLine(tx.ToHex());
	    }
    }
}
