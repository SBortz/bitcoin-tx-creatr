using System;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public interface IKeyConsoleWriter
	{
		void WriteKey(Key key);
	}

	public class KeyConsoleWriter : IKeyConsoleWriter
	{
		public void WriteKey(Key key)
		{
			Console.WriteLine("Here is your key");
			Console.WriteLine(key.GetWif(Network.Main).ToWif());
		}
	}
}