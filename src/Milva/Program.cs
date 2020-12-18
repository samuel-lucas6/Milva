using System;
using McMaster.Extensions.CommandLineUtils;

/*
    Milva: A simple, cross-platform command line tool for hashing files.
    Copyright(C) 2020 Samuel Lucas

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
    [HelpOption("--help", ShowInHelpText = false)]
    [Command(ExtendedHelpText = @"  --help      show help information

Examples:
  --sha256 [file]
  --sha1 [file]
  --md5 [file]

Please report bugs to <https://github.com/samuel-lucas6/Milva/issues>.")]
    public class Program
    {
        [Option("--blake512", "retrieve the BLAKE2-512 of a file", CommandOptionType.NoValue)]
        public bool BLAKE512 { get; }

        [Option("--blake256", "retrieve the BLAKE2-256 of a file", CommandOptionType.NoValue)]
        public bool BLAKE256 { get; }

        [Option("--sha512", "retrieve the SHA512 of a file", CommandOptionType.NoValue)]
        public bool SHA512 { get; }

        [Option("--sha384", "retrieve the SHA384 of a file", CommandOptionType.NoValue)]
        public bool SHA384 { get; }

        [Option("--sha256", "retrieve the SHA256 of a file", CommandOptionType.NoValue)]
        public bool SHA256 { get; }

        [Option("--sha1", "retrieve the SHA1 of a file", CommandOptionType.NoValue)]
        public bool SHA1 { get; }

        [Option("--md5", "retrieve the MD5 of a file", CommandOptionType.NoValue)]
        public bool MD5 { get; }

        [Option("--about", "view the version and license", CommandOptionType.NoValue)]
        public bool About { get; }

        [Argument(0)]
        public string[] FilePaths { get; }

        public enum HashFunction { BLAKE512, BLAKE256, SHA512, SHA384, SHA256, SHA1, MD5 }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            Console.WriteLine();
            if (BLAKE512)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.BLAKE512);
            }
            else if (BLAKE256)
            {
                CommandLine.HashEachFile(FilePaths, HashFunction.BLAKE256);
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
                Console.WriteLine("Error: Unrecognized option. Specify --help for a list of available options and commands.");
            }
        }
    }
}
