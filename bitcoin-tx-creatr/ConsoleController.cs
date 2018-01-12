using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CommandDotNet.Attributes;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	[ApplicationMetadata(Description = "Howdy friend, i am your bitcoin-tx-creatr.", ExtendedHelpText = "\nI can create bitcoin transactions manually for you.")]
	public class ConsoleController
	{
		public IBitcoinTxCreatr TxCreatr { get; set; }
		public ITransactionConsoleWriter Writer { get; set; }

		[ApplicationMetadata(Description = "Creates an empty transaction")]
		public void Create()
	    {
		    var tx = this.TxCreatr.Create();

			this.Writer.WriteTransaction(tx);
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
			    this.Writer.WriteTransaction(tx);
				return 0;
			}
		    catch (Exception ex)
		    {
			    Console.WriteLine("Invalid hex string");
			    return 1;
		    }
		}

	    [ApplicationMetadata(Description = "Takes a raw transaction and adds an unspent transaction (UTXO) to it.")]
		public int AddIn([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Description = "UTXO transaction-id")]string txId, [Argument(Description = "UTXO index")]int index)
	    {
		    var tx = this.TxCreatr.AddIn(transactionHex, txId, index);
			this.Writer.WriteTransaction(tx);

			return 0;
	    }

	    [ApplicationMetadata(Description = "Takes a raw transaction and removes a transaction input from it.")]
	    public int RemoveIn([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of vout")]int index)
	    {
		    var tx = this.TxCreatr.RemoveIn(transactionHex, index);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }

		[ApplicationMetadata(Description = "Takes a raw transaction and adds a transaction output to it.")]
		public int AddOut([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "address",Description = "Bitcoin address")]string addressString, [Argument(Name = "amount", Description = "Amount of Bitcoins to send to address")]string amountString)
		{
			var tx = this.TxCreatr.AddOut(transactionHex, addressString, amountString);
			this.Writer.WriteTransaction(tx);

		    return 0;
	    }

	    [ApplicationMetadata(Description = "Takes a raw transaction and sets the amount of an existing transaction output.")]
	    public int SetAmount([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of vout")]int index, [Argument(Name = "amount", Description = "Amount of Bitcoins to send to address")]string amountString)
	    {
		    var tx = this.TxCreatr.SetAmount(transactionHex, index, amountString);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }

	    [ApplicationMetadata(Description = "Takes a raw transaction and sets the amount of an existing transaction output.")]
	    public int SetLockValue([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "lockvalue", Description = "Values > 0 && < 500000000 are interpreted as block height. Above that -> Unix epoch timestamps.")]int lockvalue)
	    {
		    var tx = this.TxCreatr.SetLockValue(transactionHex, lockvalue);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }

		[ApplicationMetadata(Description = "Takes a raw transaction and removes a transaction output from it.")]
	    public int RemoveOut([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of vout")]int index)
		{
			var tx = this.TxCreatr.RemoveOut(transactionHex, index);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }


		[ApplicationMetadata(Description = "Takes a raw transaction and signs it.")]
	    public int Sign([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "privatekey", Description = "Private key")]string privateKeyString)
		{
			var tuple = this.TxCreatr.Sign(transactionHex, privateKeyString);
			Console.WriteLine($"You signed your transaction on the {tuple.Item2}");
			this.Writer.WriteTransaction(tuple.Item1);

		    return 0;
	    }

	    [ApplicationMetadata(Description = "Takes a raw transaction and calculates the total output and fee.")]
	    public int GetOutputs([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Description = "Bitcoin amount of tx in")]string amountInString)
	    {
		    var tx = new Transaction(transactionHex);

		    var amountIn = Convert.ToDecimal(amountInString, CultureInfo.InvariantCulture);
		    decimal amountOut = 0;
		    foreach (var txOutput in tx.Outputs)
		    {
			    amountOut += Convert.ToDecimal(txOutput.Value.ToDecimal(MoneyUnit.BTC), CultureInfo.InvariantCulture);
		    }

		    decimal fee = amountIn - amountOut;

		    Console.WriteLine($"This transaction has total output of {amountOut} BTC and a fee of {fee} BTC.");
		    return 0;
	    }
    }
}
