using System;
using System.Collections;
using NBitcoin;

namespace bitcoin_tx_creatr
{
	public class BitcoinKeyFormatter : IBitcoinKeyFormatter
	{
		public Key FormatBinary(string binaryIn)
		{
			binaryIn = "11111111";
			char[] bits = binaryIn.ToCharArray();

			bool[] boolArray = new bool[binaryIn.Length];

			for(int i = 0; i < binaryIn.Length; i++)
			{
				boolArray[i] = bits[i] == '1' ? true : false;
			}
			BitArray bitArray = new BitArray(boolArray);
			byte byteArray = ConvertToByte(bitArray);

			NBitcoin.Key key = new Key(new byte[] { byteArray });


			return key;
		}

		protected byte ConvertToByte(BitArray bits)
		{
			if (bits.Count != 8)
			{
				throw new ArgumentException("illegal number of bits");
			}

			byte b = 0;
			if (bits.Get(7)) b++;
			if (bits.Get(6)) b += 2;
			if (bits.Get(5)) b += 4;
			if (bits.Get(4)) b += 8;
			if (bits.Get(3)) b += 16;
			if (bits.Get(2)) b += 32;
			if (bits.Get(1)) b += 64;
			if (bits.Get(0)) b += 128;
			return b;
		}

		public Key FormatHex(string hexIn)
		{
			throw new System.NotImplementedException();
		}
	}
}