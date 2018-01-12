using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public class BitcoinTxCreatr : IBitcoinTxCreatr
    {
	    public Transaction Create()
	    {
		    return new Transaction();
	    }

	    public Transaction AddIn(string transactionHex, string txId, int index)
	    {
			var tx = new Transaction(transactionHex);

			var outTxId = new uint256(txId);
		    var outPoint = new OutPoint(outTxId, index);
			var txIn = new TxIn(outPoint);

		    tx.Inputs.Add(txIn);

		    return tx;
	    }

	    public Transaction RemoveIn(string transactionHex, int index)
	    {
			var tx = new Transaction(transactionHex);
		    tx.Inputs.RemoveAt(index);

		    return tx;
	    }

	    public Transaction AddOut(string transactionHex, string addressString, string amountString)
	    {
			var address = BitcoinAddress.Create(addressString);
		    var scriptPubKey = new Script(address.ScriptPubKey.ToString());
		    var amount = new Money(Convert.ToDecimal(amountString, CultureInfo.InvariantCulture), MoneyUnit.BTC);

		    var tx = new Transaction(transactionHex);

		    tx.Outputs.Add(new TxOut(amount, scriptPubKey));

		    return tx;
	    }

	    public Transaction SetAmount(string transactionHex, int index, string amountString)
	    {
			var tx = new Transaction(transactionHex);
		    var amount = new Money(Convert.ToDecimal(amountString, CultureInfo.InvariantCulture), MoneyUnit.BTC);
			var output = tx.Outputs[index];
		    output.Value = amount;

		    return tx;
	    }

	    public Transaction SetLockValue(string transactionHex, int lockvalue)
	    {
			var tx = new Transaction(transactionHex);
		    tx.LockTime = new LockTime(lockvalue);

		    return tx;
	    }

	    public Transaction RemoveOut(string transactionHex, int index)
	    {
			var tx = new Transaction(transactionHex);
			tx.Outputs.RemoveAt(index);
		    return tx;
	    }

	    public Tuple<Transaction, string> Sign(string transactionHex, string privateKeyString)
	    {
			var privKey = new BitcoinSecret(privateKeyString);

		    var tx = new Transaction(transactionHex);
		    tx.Inputs.First().ScriptSig = privKey.ScriptPubKey;
		    tx.Sign(privKey, false);

		    return Tuple.Create(tx, privKey.Network.ToString());
	    }
    }
}
