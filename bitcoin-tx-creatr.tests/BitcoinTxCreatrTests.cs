using System;
using System.Globalization;
using System.Linq;
using NBitcoin;
using Xunit;

namespace bitcoin_tx_creatr.tests
{
    public class BitcoinTxCreatrTests
    {
	    private IBitcoinTxCreatrStringOutput txCreatr;

	    string outAddress1 = "myX4hBezqR4mM6L37e2xBG8EyaNCjTnDHR";
	    string outAmount1 = "0.0025";

	    string outAmount2 = "1";

		private int lockTime1 = 1;
	    private int lockTime2 = 499000000;
	    private int lockTime3 = 500000000;

	    private string txIdIn1 = "3fb7e8f1f1b134eaec6c046548192bc3c59d81c8a4afe28370263e058752deba";
	    string privateKey1 = "cN5Ybcxaks2khiKCQu7ymg4KryLeTDJuNcYdeS8DXDK7S8CWx6RZ";

		public BitcoinTxCreatrTests()
	    {
			var txCreatrStr = new BitcoinTxCreatrStringOutput();
			txCreatrStr.TxCreatr = new BitcoinTxCreatr();
		    this.txCreatr = txCreatrStr;
	    }

	    [Fact]
        public void ShouldCreate()
        {
	        var resultTx = new Transaction().ToHex();
	        var tx = this.txCreatr.Create();

			Assert.Equal(tx, resultTx);
        }

		[Fact]
	    public void ShouldAddIn()
		{
			string txId = "49d2adb6e476fa46d8357babf78b1b501fd39e177ac7833124b3f67b17c40c2a";
			int index = 0;
			var expectedTx = GetTxWithIn(txId, index);

			var tx = this.txCreatr.AddIn(new Transaction().ToHex(), txId, index);

			Assert.Equal(expectedTx.ToHex(), tx);
		}

	    [Fact]
	    public void ShouldRemoveIn()
	    {
		    var expectedTx = GetTxPizzaIn();
		    expectedTx.Inputs.RemoveAt(0);

		    var tx = this.txCreatr.RemoveIn(GetTxPizzaIn().ToHex(), 0);

			Assert.Equal(expectedTx.ToHex(), tx);
	    }

	    [Fact]
	    public void ShouldAddOut()
	    {
			var expectedTx = GetTxWithOut(this.outAddress1, this.outAmount1);

		    var tx = this.txCreatr.Create();
		    tx = this.txCreatr.AddOut(tx, this.outAddress1, this.outAmount1);

			Assert.Equal(expectedTx.ToHex(), tx);
	    }


	    [Fact]
	    public void ShouldSetAmount()
	    {
		    var expectedTx = GetTxWithOut(this.outAddress1, this.outAmount1);
		    expectedTx.Outputs.First().Value = this.outAmount2;

		    var tx = this.txCreatr.SetAmount(GetTxWithOut(this.outAddress1, this.outAmount1).ToHex(), 0, this.outAmount2);

			Assert.Equal(expectedTx.ToHex(), tx);
	    }

	    [Fact]
	    public void ShouldSetLockValue()
	    {
		    var expectedTx = GetTxWithOut(this.outAddress1, this.outAmount1);
			expectedTx.LockTime = new LockTime(this.lockTime1);

		    var tx = this.txCreatr.SetLockValue(GetTxWithOut(this.outAddress1, this.outAmount1).ToHex(), this.lockTime1);

			Assert.Equal(expectedTx.ToHex(), tx);
	    }

	    [Fact]
	    public void ShouldRemoveOut()
	    {
		    var expectedTx = GetTxWithOut(this.outAddress1, this.outAmount1);
			expectedTx.Outputs.RemoveAt(0);

		    var tx = this.txCreatr.RemoveOut(GetTxWithOut(this.outAddress1, this.outAmount1).ToHex(), 0);
			Assert.Equal(expectedTx.ToHex(), tx);
	    }

	    [Fact]
	    public void ShouldSign()
	    {
		    var expectedTx = GetTxWithIn(this.txIdIn1, 0);

			var privKey = new BitcoinSecret(this.privateKey1);
		    expectedTx.Inputs.First().ScriptSig = privKey.ScriptPubKey;
		    expectedTx.Sign(privKey, false);

		    var tx = this.txCreatr.SignIn(GetTxWithIn(this.txIdIn1, 0).ToHex(), 0, this.privateKey1);

			Assert.Equal(expectedTx.ToHex(), tx);
	    }

	    private static Transaction GetTxWithOut(string addressString, string amountString)
	    {
		    var address = BitcoinAddress.Create(addressString);
		    var scriptPubKey = new Script(address.ScriptPubKey.ToString());
		    var amount = new Money(Convert.ToDecimal(amountString, CultureInfo.InvariantCulture), MoneyUnit.BTC);

		    var tx = new Transaction();
		    tx.AddOutput(new TxOut(amount, scriptPubKey));
		    return tx;
	    }

		private static Transaction GetTxPizzaIn()
	    {
			// Pizza transaction id
		    string txId = "49d2adb6e476fa46d8357babf78b1b501fd39e177ac7833124b3f67b17c40c2a";
		    int index = 0;
		    return GetTxWithIn(txId, index);
	    }

	    private static Transaction GetTxWithIn(string txId, int index)
	    {
			var tx = new Transaction();
		    var outTxId = new uint256(txId);
		    var outPoint = new OutPoint(outTxId, index);
		    var txIn = new TxIn(outPoint);
		    tx.Inputs.Add(txIn);
		    return tx;
	    }
    }
}
