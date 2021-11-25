using System;
using McMaster.Extensions.CommandLineUtils;

/*
    Milva: A simple, cross-platform command line tool for hashing files.
    Copyright (C) 2020-2021 Samuel Lucas

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

namespace Milva
{
    [HelpOption("-h|--help", ShowInHelpText = false)]
    [Command(ExtendedHelpText = @"  -h|--help     show help information

Examples:
  --sha256 [file]
  --sha1 [file]
  --md5 [file]

Please report bugs to <https://github.com/samuel-lucas6/Milva/issues>.")]
    public class Program
    {
        [Option("--shake256", "hash a file using SHAKE256", CommandOptionType.NoValue)]
        public bool SHAKE256 { get; }

        [Option("--shake128", "hash a file using SHAKE128", CommandOptionType.NoValue)]
        public bool SHAKE128 { get; }

        [Option("--sha3512", "hash a file using SHA3-512", CommandOptionType.NoValue)]
        public bool SHA3_512 { get; }

        [Option("--sha3384", "hash a file using SHA3-384", CommandOptionType.NoValue)]
        public bool SHA3_384 { get; }

        [Option("--sha3256", "hash a file using SHA3-256", CommandOptionType.NoValue)]
        public bool SHA3_256 { get; }

        [Option("--blake3", "hash a file using BLAKE3-256", CommandOptionType.NoValue)]
        public bool BLAKE3 { get; }

        [Option("--blake2b512", "hash a file using BLAKE2b-512", CommandOptionType.NoValue)]
        public bool BLAKE2b512 { get; }

        [Option("--blake2b256", "hash a file using BLAKE2b-256", CommandOptionType.NoValue)]
        public bool BLAKE2b256 { get; }

        [Option("--sha512", "hash a file using SHA512", CommandOptionType.NoValue)]
        public bool SHA512 { get; }

        [Option("--sha384", "hash a file using SHA384", CommandOptionType.NoValue)]
        public bool SHA384 { get; }

        [Option("--sha256", "hash a file using SHA256", CommandOptionType.NoValue)]
        public bool SHA256 { get; }

        [Option("--sha1", "hash a file using SHA1", CommandOptionType.NoValue)]
        public bool SHA1 { get; }

        [Option("--md5", "hash a file using MD5", CommandOptionType.NoValue)]
        public bool MD5 { get; }

        [Option("-a|--about", "view the program version and license", CommandOptionType.NoValue)]
        public bool About { get; }

        [Argument(order: 0, Description = "specify a file path", Name = "file")]
        public string[] FilePaths { get; }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine();
            if (SHAKE256)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHAKE256);
            }
            else if (SHAKE128)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHAKE128);
            }
            else if (SHA3_512)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA3_512);
            }
            else if (SHA3_384)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA3_384);
            }
            else if (SHA3_256)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA3_256);
            }
            else if (BLAKE3)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.BLAKE3);
            }
            else if (BLAKE2b512)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.BLAKE2b512);
            }
            else if (BLAKE2b256)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.BLAKE2b256);
            }
            else if (SHA512)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA512);
            }
            else if (SHA384)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA384);
            }
            else if (SHA256)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA256);
            }
            else if (SHA1)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.SHA1);
            }
            else if (MD5)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.MD5);
            }
            else if (About)
            {
                CommandLine.DisplayAbout();
            }
            else
            {
                DisplayMessage.Error("Unrecognised option. Specify --help for a list of available options and commands.");
            }
        }
    }
}
