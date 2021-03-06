﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CommandDotNet.Attributes;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	[ApplicationMetadata(Description = "Hey, bitcoin-tx-creatr here :) How can i help you?", ExtendedHelpText = "\nI can create bitcoin transactions manually for you.", Name = "bitcoin-tx-creatr")]
	public class ConsoleController
	{
		public IBitcoinTxCreatr TxCreatr { get; set; }
		public ITransactionConsoleWriter Writer { get; set; }

		[ApplicationMetadata(Name = "create", Description = "Creates an empty tx")]
		public void Create()
	    {
		    var tx = this.TxCreatr.Create();

			this.Writer.WriteTransaction(tx);
	    }

	    [ApplicationMetadata(Name = "show", Description = "returns it in json format")]
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

	    [ApplicationMetadata(Name = "add-in", Description = "adds an unspent transaction (UTXO) to tx.")]
		public int AddIn([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Description = "UTXO transaction-id")]string txId, [Argument(Description = "UTXO index")]int index)
	    {
		    var tx = this.TxCreatr.AddIn(transactionHex, txId, index);
			this.Writer.WriteTransaction(tx);

			return 0;
	    }

	    [ApplicationMetadata(Name = "remove-in", Description = "removes a transaction input from tx.")]
	    public int RemoveIn([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of vout")]int index)
	    {
		    var tx = this.TxCreatr.RemoveIn(transactionHex, index);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }

		[ApplicationMetadata(Name = "add-out", Description = "adds a transaction output to tx.")]
		public int AddOut([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "address",Description = "Bitcoin address (Base58Check)")]string addressString, [Argument(Name = "amount", Description = "Amount of Bitcoins to send to address in BTC. (Format 0.005) ")]string amountString)
		{
			var tx = this.TxCreatr.AddOut(transactionHex, addressString, amountString);
			this.Writer.WriteTransaction(tx);

		    return 0;
	    }

	    [ApplicationMetadata(Name = "set-out-amount", Description = "sets the amount of an existing transaction output.")]
	    public int SetAmount([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of vout")]int index, [Argument(Name = "amount", Description = "Amount of Bitcoins to send to address in BTC. (Format 0.005)")]string amountString)
	    {
		    var tx = this.TxCreatr.SetAmount(transactionHex, index, amountString);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }

	    [ApplicationMetadata(Name = "set-tx-lockvalue", Description = "sets the lock value of tx.")]
	    public int SetLockValue([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "lockvalue", Description = "Values > 0 && < 500000000 are interpreted as block height. Above that -> Unix epoch timestamps.")]int lockvalue)
	    {
		    var tx = this.TxCreatr.SetLockValue(transactionHex, lockvalue);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }

		[ApplicationMetadata(Name = "remove-out", Description = "removes a transaction output from tx.")]
	    public int RemoveOut([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of vout")]int index)
		{
			var tx = this.TxCreatr.RemoveOut(transactionHex, index);
		    this.Writer.WriteTransaction(tx);

		    return 0;
	    }


		[ApplicationMetadata(Name = "sign-in", Description = "signs one input of tx it.")]
	    public int SignIn([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Name = "index", Description = "Index of transaction input")]int index, [Argument(Name = "privatekey", Description = "Private key (WIF)")]string privateKeyString)
		{
			var tuple = this.TxCreatr.SignIn(transactionHex, index, privateKeyString);
			Console.WriteLine($"You signed your transaction on the {tuple.Item2}");
			this.Writer.WriteTransaction(tuple.Item1);

		    return 0;
	    }

	    [ApplicationMetadata(Name = "get-outputs", Description = "calculates the total output and fee.")]
	    public int GetOutputs([Argument(Description = "Raw Transaction hex")]string transactionHex, [Argument(Description = "Bitcoin amount of txin in BTC (Format: 0.005)")]string amountInString)
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
