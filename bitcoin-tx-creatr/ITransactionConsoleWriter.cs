using System.Collections.Generic;
using System.Text;
using NBitcoin;

namespace bitcoin_tx_creatr
{
    public interface ITransactionConsoleWriter
    {
	    void WriteTransaction(Transaction tx);
    }
}
