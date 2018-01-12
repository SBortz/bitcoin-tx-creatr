# bitcoin-tx-creatr - create & manipulate bitcoin transactions manually on command line

In the approach of understanding bitcoin transactions i like the idea of having a simple command line tool that makes it simple to create & and manipulate raw bitcoin transactions that could then be sent to the bitcoin network.

So i created this little console app using the NBitcoin library (https://github.com/MetacoSA/NBitcoin). It outputs the transaction in json and hex format. The raw transaction output (hex) can then be used as input for further manipulation. So you can construct a valid bitcoin transaction step by step that will be accepted by bitcoin nodes.

<b>IMPORTANT: This is the first version and it works only for very simple transactions at the moment. Use it for learning/testing purposes only.</b>

# Using the app

## Installation

This is a .NET Core 2.0 command line project. Clone the git repository and use the dotnet command to build the app. Type:

Windows:
```
dotnet publish -c Release -r win10-x64 
```

Linux:
```
dotnet publish -c Release -r ubuntu.16.10-x64
```

## Overview of app usage

This leads to the overview of all commands:

```
> bitcoin-tx-creatr.exe --help

Hey, bitcoin-tx-creatr here :) How can i help you? 1.0.0

Usage: dotnet bitcoin-tx-creatr.dll [options] [command]

Options:
  -v | --version  Show version information
  -h | --help     Show help information

Commands:
  add-in            adds an unspent transaction (UTXO) to tx.
  add-out           adds a transaction output to tx.
  create            Creates an empty tx
  get-outputs       calculates the total output and fee.
  remove-in         removes a transaction input from tx.
  remove-out        removes a transaction output from tx.
  set-out-amount    sets the amount of an existing transaction output.
  set-tx-lockvalue  sets the lock value of tx.
  show              returns it in json format
  sign-in           signs one input of tx it.

Use "dotnet bitcoin-tx-creatr.dll [command] --help" for more information about a command.

I can create bitcoin transactions manually for you.
```

## Creating a transaction from scratch

Creating a new transaction is fairly simple. Here is how an empty transaction is created.

```
> bitcoin-tx-creatr.exe create

Here is your transaction (json)
{
  "hash": "d21633ba23f70118185227be58a63527675641ad37967e2aa461559f577aec43",
  "ver": 1,
  "vin_sz": 0,
  "vout_sz": 0,
  "lock_time": 0,
  "size": 10,
  "in": [],
  "out": []
}

Here is your transaction (hex)
01000000000000000000
```

### Adding a transaction input

Then add a transaction input by copying the hex-output and using it as input for the addin command:
```
> bitcoin-tx-creatr.exe add-in previousTransactionHex txId index

Here is your transaction (json)
{
  "hash": "e16cb90e6b527cf72234b7e9f9188df151c8eca976a694038821d4d42ac275ee",
  "ver": 1,
  "vin_sz": 1,
  "vout_sz": 0,
  "lock_time": 0,
  "size": 51,
  "in": [
    {
      "prev_out": {
        "hash": "3fb7e8f1f1b134eaec6c046548192bc3c59d81c8a4afe28370263e058752deba",
        "n": 0
      },
      "scriptSig": ""
    }
  ],
  "out": []
}

Here is your transaction (hex)
0100000001bade5287053e267083e2afa4c8819dc5c32b194865046cecea34b1f1f1e8b73f0000000000ffffffff0000000000
```

### Adding a transaction output

Use the addout command to add a transaction output. Also provide an address and an amount (BTC).

<b> At the moment P2PKH is supported only. </b>
```
> bitcoin-tx-creatr.exe add-out previousTransactionHex address amount

Here is your transaction (json)
{
  "hash": "2b11f5918ee21d5429f7f3bafc785c012d08ce906df6851b500560393b79ee4f",
  "ver": 1,
  "vin_sz": 1,
  "vout_sz": 1,
  "lock_time": 0,
  "size": 85,
  "in": [
    {
      "prev_out": {
        "hash": "3fb7e8f1f1b134eaec6c046548192bc3c59d81c8a4afe28370263e058752deba",
        "n": 0
      },
      "scriptSig": ""
    }
  ],
  "out": [
    {
      "value": "1.00000000",
      "scriptPubKey": "OP_DUP OP_HASH160 c5779b05e5f272284665befc881e9e8c4eb8d82b OP_EQUALVERIFY OP_CHECKSIG"
    }
  ]
}

Here is your transaction (hex)
0100000001bade5287053e267083e2afa4c8819dc5c32b194865046cecea34b1f1f1e8b73f0000000000ffffffff0100e1f505000000001976a914c5779b05e5f272284665befc881e9e8c4eb8d82b88ac00000000
```

### Sign transaction

Sign the transaction with your private key (WIF).
```
> bitcoin-tx-creatr.exe sign-in previousTransactionHex index privateKey

You signed your transaction on the TestNet
Here is your transaction (json)
{
  "hash": "d93634442940de712d09e3bfb63bdd747fb1c40c35a73b617dc80b4991c83840",
  "ver": 1,
  "vin_sz": 1,
  "vout_sz": 1,
  "lock_time": 0,
  "size": 192,
  "in": [
    {
      "prev_out": {
        "hash": "3fb7e8f1f1b134eaec6c046548192bc3c59d81c8a4afe28370263e058752deba",
        "n": 0
      },
      "scriptSig": "304502210096e1c9d1f4b72db6819330736012646119894a4ded3e01d43759de98956c77e302205e588345e63b1e07d8dd8d222facdb8e208b3b7e9e7e0090a20bc479f85ddbb501 03427e0db2662bb9c5b9aa6eb77bff244570751431dc2ab2099ee22da6b843cc2c"
    }
  ],
  "out": [
    {
      "value": "1.00000000",
      "scriptPubKey": "OP_DUP OP_HASH160 c5779b05e5f272284665befc881e9e8c4eb8d82b OP_EQUALVERIFY OP_CHECKSIG"
    }
  ]
}

Here is your transaction (hex)
0100000001bade5287053e267083e2afa4c8819dc5c32b194865046cecea34b1f1f1e8b73f000000006b48304502210096e1c9d1f4b72db6819330736012646119894a4ded3e01d43759de98956c77e302205e588345e63b1e07d8dd8d222facdb8e208b3b7e9e7e0090a20bc479f85ddbb5012103427e0db2662bb9c5b9aa6eb77bff244570751431dc2ab2099ee22da6b843cc2cffffffff0100e1f505000000001976a914c5779b05e5f272284665befc881e9e8c4eb8d82b88ac00000000
```
And that´s it. Send the transaction (hex) to the bitcoin network. For example via https://live.blockcypher.com/btc/pushtx/ or the bitcoin core client. Have fun!

## Frameworks
The project uses NBitcoin and CommandDotNet.
