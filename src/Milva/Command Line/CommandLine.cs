using System;
using System.IO;
using System.Reflection;

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
    public static class CommandLine
    {
        public static void HashEachFile(string[] filePaths, Program.HashFunction hashFunction)
        {
            if (filePaths != null)
            {
                foreach (string filePath in filePaths)
                {
                    if (File.Exists(filePath))
                    {
                        byte[] hash = FileHandling.HashFile(filePath, hashFunction);
                        if (hash != null)
                        {
                            DisplayHash(filePath, hash);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)} - Error: The specified file doesn't exist.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: No file has been specified.");
            }
        }

        private static void DisplayHash(string filePath, byte[] hashBytes)
        {
            Console.WriteLine($"{Path.GetFileName(filePath)}: {ConvertHash.ToString(hashBytes)}");
        }

        public static void DisplayAbout()
        {
            string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Console.WriteLine($"Milva {assemblyVersion.Substring(0, assemblyVersion.Length - 2)}");
            Console.WriteLine("Copyright(C) 2020 Samuel Lucas");
            Console.WriteLine("License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>");
            Console.WriteLine("This is free software: you are free to change and redistribute it.");
            Console.WriteLine("There is NO WARRANTY, to the extent permitted by law.");
        }
    }
}
