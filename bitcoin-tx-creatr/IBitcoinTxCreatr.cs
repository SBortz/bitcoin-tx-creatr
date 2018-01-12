using System;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public interface IBitcoinTxCreatr
	{
		Transaction Create();
		Transaction AddIn(string transactionHex, string txId, int index);
		Transaction RemoveIn(string transactionHex, int index);
		Transaction AddOut(string transaction, string address, string amount);
		Transaction SetAmount(string transactionHex, int index, string amountString);
		Transaction SetLockValue(string transactionHex, int lockvalue);
		Transaction RemoveOut(string transactionHex, int index);
		Tuple<Transaction, string> SignIn(string transactionHex, int index, string privateKeyString);
	}
}