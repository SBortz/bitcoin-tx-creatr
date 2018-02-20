using System.ComponentModel;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public interface IBitcoinKeyFormatter
	{
		Key FormatBinary(string binaryIn);
		Key FormatHex(string hexIn);
	}
}