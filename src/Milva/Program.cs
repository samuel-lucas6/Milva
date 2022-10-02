using System;
using System.IO;
using System.Text;
using System.Security;
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

public enum HashFunction
{
    SHAKE256,
    SHAKE128,
    SHA3_512,
    SHA3_384,
    SHA3_256,
    BLAKE3,
    BLAKE2b512,
    BLAKE2b384,
    BLAKE2b256,
    BLAKE2b160,
    SHA512,
    SHA384,
    SHA256,
    SHA1,
    MD5
}

[HelpOption("-h|--help", ShowInHelpText = false)]
[Command(ExtendedHelpText = @"  -h|--help     show help information

Examples:
  --sha256 [file]
  --sha256 [directory]
  --sha256 --text [text]

Please report bugs at <https://github.com/samuel-lucas6/Milva/issues>.")]
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
    
    [Option("--blake2b-384", "use BLAKE2b-384", CommandOptionType.NoValue)]
    public bool BLAKE2b384 { get; }

    [Option("--blake2b-256", "use BLAKE2b-256", CommandOptionType.NoValue)]
    public bool BLAKE2b256 { get; }
    
    [Option("--blake2b-160", "use BLAKE2b-160", CommandOptionType.NoValue)]
    public bool BLAKE2b160 { get; }

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
        if (SHAKE256) {
            HashEachInput(Inputs, Text, HashFunction.SHAKE256);
        }
        else if (SHAKE128) {
            HashEachInput(Inputs, Text, HashFunction.SHAKE128);
        }
        else if (SHA3_512) {
            HashEachInput(Inputs, Text, HashFunction.SHA3_512);
        }
        else if (SHA3_384) {
            HashEachInput(Inputs, Text, HashFunction.SHA3_384);
        }
        else if (SHA3_256) {
            HashEachInput(Inputs, Text, HashFunction.SHA3_256);
        }
        else if (BLAKE3) {
            HashEachInput(Inputs, Text, HashFunction.BLAKE3);
        }
        else if (BLAKE2b512) {
            HashEachInput(Inputs, Text, HashFunction.BLAKE2b512);
        }
        else if (BLAKE2b384) {
            HashEachInput(Inputs, Text, HashFunction.BLAKE2b384);
        }
        else if (BLAKE2b256) {
            HashEachInput(Inputs, Text, HashFunction.BLAKE2b256);
        }
        else if (BLAKE2b160) {
            HashEachInput(Inputs, Text, HashFunction.BLAKE2b160);
        }
        else if (SHA512) {
            HashEachInput(Inputs, Text, HashFunction.SHA512);
        }
        else if (SHA384) {
            HashEachInput(Inputs, Text, HashFunction.SHA384);
        }
        else if (SHA256) {
            HashEachInput(Inputs, Text, HashFunction.SHA256);
        }
        else if (SHA1) {
            HashEachInput(Inputs, Text, HashFunction.SHA1);
        }
        else if (MD5) {
            HashEachInput(Inputs, Text, HashFunction.MD5);
        }
        else if (About) {
            DisplayMessage.About();
        }
        else {
            DisplayMessage.Error("Unknown command. Please specify -h|--help for a list of options and examples.");
        }
        return Environment.ExitCode;
    }

    private static void HashEachInput(string[] inputs, bool text, HashFunction hashFunction)
    {
        if (inputs == null) {
            DisplayMessage.Error(!text ? "Please specify a file/directory to hash." : "Please specify text to hash.");
            return;
        }
        foreach (string input in inputs) {
            try
            {
                if (!text && Directory.Exists(input)) {
                    string[] filePaths = Directory.GetFiles(input, searchPattern: "*", SearchOption.AllDirectories);
                    int inputsIndex = Array.IndexOf(inputs, input);
                    if (inputsIndex > 0) {
                        Console.WriteLine();
                    }
                    DisplayMessage.Message(input, "Hashing each file in the directory...");
                    HashEachInput(filePaths, text: false, hashFunction);
                    if (inputsIndex != inputs.Length - 1) {
                        Console.WriteLine();
                    }
                    continue;
                }

                if (!text && !File.Exists(input)) {
                    DisplayMessage.NamedError(input, "This file/directory path doesn't exist.");
                    continue;
                }

                using Stream stream = !text ? new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read, GetFileStreamBufferSize(new FileInfo(input).Length), FileOptions.None) : new MemoryStream(Encoding.UTF8.GetBytes(input));
                byte[] hash = HashingAlgorithms.GetHash(stream, hashFunction);
                DisplayMessage.Message(input, Convert.ToHexString(hash).ToLower());
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or SecurityException or NotSupportedException)
            {
                DisplayMessage.NamedError(input, ex.GetType().ToString());
            }
        }
    }
    
    private static int GetFileStreamBufferSize(long fileLength)
    {
        return fileLength switch
        {
            <= 262144 => 0,
            <= 5242880 => 81920,
            < 104857600 => 131072,
            >= 104857600 => 1048576
        };
    }
}