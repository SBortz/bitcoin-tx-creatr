# bitcoin-tx-creatr - Create/Manipulate/Output bitcoin transaction manually on command line

In the approach of understanding bitcoin transactions i liked the idea of having a simple command line tool that makes it simple to create, output and manipulate raw bitcoin transactions that could later be sent to the bitcoin network.

So i created this little console app using .NET Core 2.0.

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
```

```
