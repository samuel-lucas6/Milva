using System;
using McMaster.Extensions.CommandLineUtils;

/*
    Milva: A simple, cross-platform command line tool for hashing files and text.
    Copyright (C) 2020-2022 Samuel Lucas

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see https://www.gnu.org/licenses/.
*/

namespace Milva;

[HelpOption("-h|--help", ShowInHelpText = false)]
[Command(ExtendedHelpText = @"  -h|--help     show help information

Examples:
  --sha256 [file]
  --sha256 [directory]
  --sha256 --text [text]

Please report bugs to <https://github.com/samuel-lucas6/Milva/issues>.")]
public class Program
{
    [Option("--shake256", "use SHAKE256", CommandOptionType.NoValue)]
    public bool SHAKE256 { get; }

    [Option("--shake128", "use SHAKE128", CommandOptionType.NoValue)]
    public bool SHAKE128 { get; }

    [Option("--sha3-512", "use SHA3-512", CommandOptionType.NoValue)]
    public bool SHA3_512 { get; }

    [Option("--sha3-384", "use SHA3-384", CommandOptionType.NoValue)]
    public bool SHA3_384 { get; }

    [Option("--sha3-256", "use SHA3-256", CommandOptionType.NoValue)]
    public bool SHA3_256 { get; }

    [Option("--blake3", "use BLAKE3-256", CommandOptionType.NoValue)]
    public bool BLAKE3 { get; }

    [Option("--blake2b-512", "use BLAKE2b-512", CommandOptionType.NoValue)]
    public bool BLAKE2b512 { get; }

    [Option("--blake2b-256", "use BLAKE2b-256", CommandOptionType.NoValue)]
    public bool BLAKE2b256 { get; }

    [Option("--sha512", "use SHA-512", CommandOptionType.NoValue)]
    public bool SHA512 { get; }

    [Option("--sha384", "use SHA-384", CommandOptionType.NoValue)]
    public bool SHA384 { get; }

    [Option("--sha256", "use SHA-256", CommandOptionType.NoValue)]
    public bool SHA256 { get; }

    [Option("--sha1", "use SHA-1", CommandOptionType.NoValue)]
    public bool SHA1 { get; }

    [Option("--md5", "use MD5", CommandOptionType.NoValue)]
    public bool MD5 { get; }
    
    [Option("-t|--text", "specify text instead of files/directories", CommandOptionType.NoValue)]
    public bool Text { get; }
    
    [Option("-a|--about", "view the program version and license", CommandOptionType.NoValue)]
    public bool About { get; }

    [Argument(order: 0, Description = "specify files/directories or text", Name = "inputs")]
    public string[] Inputs { get; }

    public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private int OnExecute()
    {
        Console.WriteLine();
        if (SHAKE256)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHAKE256);
        }
        else if (SHAKE128)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHAKE128);
        }
        else if (SHA3_512)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA3_512);
        }
        else if (SHA3_384)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA3_384);
        }
        else if (SHA3_256)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA3_256);
        }
        else if (BLAKE3)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.BLAKE3);
        }
        else if (BLAKE2b512)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.BLAKE2b512);
        }
        else if (BLAKE2b256)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.BLAKE2b256);
        }
        else if (SHA512)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA512);
        }
        else if (SHA384)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA384);
        }
        else if (SHA256)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA256);
        }
        else if (SHA1)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.SHA1);
        }
        else if (MD5)
        {
            CommandLine.HashEachInput(Inputs, Text, HashFunction.MD5);
        }
        else if (About)
        {
            DisplayMessage.About();
        }
        else
        {
            DisplayMessage.Error("Unknown command. Please specify -h|--help for a list of options and examples.");
        }
        return Environment.ExitCode;
    }
}