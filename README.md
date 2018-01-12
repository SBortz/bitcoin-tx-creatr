# bitcoin-tx-creatr - Create/Manipulate/Output bitcoin transaction manually on command line

In the approach of understanding bitcoin transactions i liked the idea of having a simple command line tool that makes it simple to create, output and manipulate raw bitcoin transactions that could later be sent to the bitcoin network.

So i created this little console app using .NET Core 2.0. It outputs the transaction always in json format and hex. The hex output can then be used as an input for another manipulation. So you can construct a full and valid bitcoin transaction that will be accepted by bitcoin nodes.

# Using the app
## Overview of app usage

To create a transaction is fairly simple. Just type 

This is a .NET Core 2.0 command line project, so you can use it after cloning the git repository by using this command:
```
dotnet bitcoin-tx-creatr.dll --help
```

This leads to the overview of all commands.

```
Hey, bitcoin-tx-creatr here :) How can i help you? 1.0.0

Usage: dotnet bitcoin-tx-creatr.dll [options] [command]

Options:
  -v | --version  Show version information
  -h | --help     Show help information

Commands:
  AddIn         Takes a raw transaction and adds an unspent transaction (UTXO) to it.
  AddOut        Takes a raw transaction and adds a transaction output to it.
  Create        Creates an empty transaction
  GetOutputs    Takes a raw transaction and calculates the total output and fee.
  RemoveIn      Takes a raw transaction and removes a transaction input from it.
  RemoveOut     Takes a raw transaction and removes a transaction output from it.
  SetAmount     Takes a raw transaction and sets the amount of an existing transaction output.
  SetLockValue  Takes a raw transaction and sets the amount of an existing transaction output.
  Show          Takes a raw transaction and returns it in json format.
  Sign          Takes a raw transaction and signs it.

Use "dotnet bitcoin-tx-creatr.dll [command] --help" for more information about a command.

I can create bitcoin transactions manually for you.
```

## Creating a transaction from scratch

Create a new transaction by typing:

```
dotnet bitcoin-tx-creatr.dll create
```

Bitcoint-Tx-Creatr creates an empty transaction and outputs it in json & hex formats.
```
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

```
dotnet bitcoin-tx-creatr.dll addin previousTransactionHex txId index
```

```
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

```
dotnet bitcoin-tx-creatr.dll addout previousTransactionHex address amount
```

```
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

```
dotnet bitcoin-tx-creatr.dll sign previousTransactionHex privateKey
```

```
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

And that´s it. Send the transaction (hex) to the bitcoin network. For example via https://live.blockcypher.com/btc/pushtx/ or the bitcoin core client.
