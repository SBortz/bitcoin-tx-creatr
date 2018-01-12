using System.Collections.Generic;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public interface IBitcoinTxCreatrStringOutput
	{
		string Create();
		string AddIn(string emptyTransaction, string txId, int index);
		string RemoveIn(string getTxPizzaIn, int index);
		string AddOut(string transaction, string address, string amount);
		string SetAmount(string transactionHex, int index, string amountString);

		string SetLockValue(string transactionHex, int lockvalue);
		string RemoveOut(string transactionHex, int index);
		string SignIn(string transactionHex, int index, string privateKeyString);

	}
}