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

	    private string emptyTx = "01000000000000000000";

		// Tx with 1 in (famous Pizza tx)
	    private string txWithPizzaIn = "01000000012a0cc4177bf6b3243183c77a179ed31f501b8bf7ab7b35d846fa76e4b6add2490000000000ffffffff0000000000";

		// Tx with out for address "myX4hBezqR4mM6L37e2xBG8EyaNCjTnDHR" and amount "0.0025"
		private string txWith1OutAmount00025 = "01000008000190d00300000000001976a914c5779b05e5f272284665befc881e9e8c4eb8d82b88ac00000000";
		private string txWith1OutAmount1 = "01000008000100e1f505000000001976a914c5779b05e5f272284665befc881e9e8c4eb8d82b88ac00000000";
		private string outAddress = "myX4hBezqR4mM6L37e2xBG8EyaNCjTnDHR";
	    private string outAmount00025 = "0.0025";
	    private string outAmount1 = "1";

		// txWith1OutAmount00025 and locktime = 1
		private string txWith1OutAmount00025LockTime1 = "01000008000190d00300000000001976a914c5779b05e5f272284665befc881e9e8c4eb8d82b88ac01000000";
	    private int lockTime1 = 1;

		// TxWithPizzaIn with valid unlocking script (privateKey = "cN5Ybcxaks2khiKCQu7ymg4KryLeTDJuNcYdeS8DXDK7S8CWx6RZ")
		private string privateKey1 = "cN5Ybcxaks2khiKCQu7ymg4KryLeTDJuNcYdeS8DXDK7S8CWx6RZ";
		private string txWithPizzaInSigned = "01000000012a0cc4177bf6b3243183c77a179ed31f501b8bf7ab7b35d846fa76e4b6add249000000006a47304402202926ddea1d7a45b1c2e3be03e4ae09840281d9dadfdd5fb90ddaa3b5494f7cd902203d33ff341d4887b8c9fadbf24ac537c2520ac116a82f216e36c37fdc6359fc91012103427e0db2662bb9c5b9aa6eb77bff244570751431dc2ab2099ee22da6b843cc2cffffffff0000000000";

		public BitcoinTxCreatrTests()
	    {
			var txCreatrStr = new BitcoinTxCreatrStringOutput();
			txCreatrStr.TxCreatr = new BitcoinTxCreatr();
		    this.txCreatr = txCreatrStr;
	    }

	    [Fact]
        public void ShouldCreate()
        {
	        var tx = this.txCreatr.Create();

			Assert.Equal(tx, this.emptyTx);
        }

		[Fact]
	    public void ShouldAddIn()
		{
			string pizzaTxIn = "49d2adb6e476fa46d8357babf78b1b501fd39e177ac7833124b3f67b17c40c2a";
			int index = 0;

			var tx = this.txCreatr.AddIn(new Transaction().ToHex(), pizzaTxIn, index);

			Assert.Equal(this.txWithPizzaIn, tx);
		}

	    [Fact]
	    public void ShouldRemoveIn()
	    {
		    var tx = this.txCreatr.RemoveIn(this.txWithPizzaIn, 0);

			Assert.Equal(this.emptyTx, tx);
	    }

	    [Fact]
	    public void ShouldAddOut()
	    {
			var tx = this.txCreatr.AddOut(this.emptyTx, this.outAddress, this.outAmount00025);

			Assert.Equal(this.txWith1OutAmount00025, tx);
	    }

	    [Fact]
	    public void ShouldSetAmount()
	    {
		    var tx = this.txCreatr.SetAmount(this.txWith1OutAmount00025, 0, this.outAmount1);

			Assert.Equal(this.txWith1OutAmount1, tx);
	    }

	    [Fact]
	    public void ShouldSetLockValue()
	    {
		    var tx = this.txCreatr.SetLockValue(this.txWith1OutAmount00025, this.lockTime1);

			Assert.Equal(this.txWith1OutAmount00025LockTime1, tx);
	    }

	    [Fact]
	    public void ShouldRemoveOut()
	    {
		    var tx = this.txCreatr.RemoveOut(this.txWith1OutAmount00025, 0);
			Assert.Equal(this.emptyTx, tx);
	    }

	    [Fact]
	    public void ShouldSign()
		{
		    var tx = this.txCreatr.SignIn(this.txWithPizzaIn, 0, this.privateKey1);

			Assert.Equal(this.txWithPizzaInSigned, tx);
	    }
    }
}
