using NBitcoin;

namespace bitcoin_tx_creatr
{
	public class BitcoinTxCreatrStringOutput : IBitcoinTxCreatrStringOutput
	{
		public IBitcoinTxCreatr TxCreatr { get; set; }

		public string Create()
		{
			return this.TxCreatr.Create().ToHex();
		}

		public string AddIn(string transaction, string txId, int index)
		{
			return this.TxCreatr.AddIn(transaction, txId, index).ToHex();
		}

		public string RemoveIn(string transaction, int index)
		{
			return this.TxCreatr.RemoveIn(transaction, index).ToHex();
		}

		public string AddOut(string transaction, string address, string amount)
		{
			return this.TxCreatr.AddOut(transaction, address, amount).ToHex();
		}

		public string SetAmount(string transactionHex, int index, string amountString)
		{
			return this.TxCreatr.SetAmount(transactionHex, index, amountString).ToHex();
		}

		public string SetLockValue(string transactionHex, int lockvalue)
		{
			return this.TxCreatr.SetLockValue(transactionHex, lockvalue).ToHex();
		}

		public string RemoveOut(string transactionHex, int index)
		{
			return this.TxCreatr.RemoveOut(transactionHex, index).ToHex();
		}

		public string SignIn(string transactionHex, int index, string privateKeyString)
		{
			return this.TxCreatr.SignIn(transactionHex, index, privateKeyString).Item1.ToHex();
		}
	}
}