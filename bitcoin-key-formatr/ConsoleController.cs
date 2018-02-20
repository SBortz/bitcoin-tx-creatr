using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CommandDotNet.Attributes;

namespace bitcoin_tx_creatr
{
	[ApplicationMetadata(Description = "Hey, bitcoin-key-formatr here :) How can i help you?", ExtendedHelpText = "\nI can create bitcoin transactions manually for you.", Name = "bitcoin-key-formatr")]
	public class ConsoleController
	{
		public IBitcoinKeyFormatter KeyFormatter { get; set; }
		public IKeyConsoleWriter Writer { get; set; }

		[ApplicationMetadata(Name = "from-binary", Description = "Takes 256 bit binary value.")]
		public void FormatFromBinary([Argument(Description = "256 bit binary value")]string binaryIn)
	    {
			var key = this.KeyFormatter.FormatBinary(binaryIn);

			this.Writer.WriteKey(key);
	    }

		[ApplicationMetadata(Name = "from-hex", Description = "Takes raw private key as hex value.")]
		public void FormatFromHex([Argument(Description = "hex value")]string hexIn)
		{
			var key = this.KeyFormatter.FormatHex(hexIn);

			this.Writer.WriteKey(key);
		}
	}
}
