using System;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public class TransactionConsoleWriter : ITransactionConsoleWriter
	{
		public void WriteTransaction(Transaction tx)
		{
			Console.WriteLine("Here is your transaction (json)");
			Console.WriteLine(tx.ToString());
			Console.WriteLine();
			Console.WriteLine("Here is your transaction (hex)");
			Console.WriteLine(tx.ToHex());
		}
	}
}