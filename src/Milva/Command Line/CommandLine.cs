using System;
using System.IO;
using System.Reflection;
using System.Security;

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

namespace Milva;

public static class CommandLine
{
    public static void HashEachFile(string[] filePaths, HashFunction hashFunction)
    {
        if (filePaths == null)
        {
            DisplayMessage.Error("Please specify a file/folder to hash.");
            return;
        }
        foreach (string filePath in filePaths)
        {
            try
            {
                if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath, searchPattern: "*", SearchOption.AllDirectories);
                    DisplayMessage.FilePathMessage(filePath, "Hashing each file in the directory...");
                    HashEachFile(files, hashFunction);
                    if (filePaths.Length > 1) { Console.WriteLine(); }
                    continue;
                }
                if (!File.Exists(filePath))
                {
                    DisplayMessage.FilePathError(filePath, "This file path doesn't exist.");
                    continue;
                }
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, HashingAlgorithms.BufferSize, FileOptions.SequentialScan);
                byte[] hash = HashingAlgorithms.GetHash(fileStream, hashFunction);
                DisplayMessage.FilePathMessage(filePath, BitConverter.ToString(hash).Replace("-", "").ToLower());
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or SecurityException or NotSupportedException)
            {
                DisplayMessage.FilePathError(filePath, ex.GetType().ToString());
            }
        }
    }

    public static void DisplayAbout()
    {
        string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        Console.WriteLine($"Milva v{assemblyVersion?[..^2]}");
        Console.WriteLine("Copyright (C) 2020-2022 Samuel Lucas");
        Console.WriteLine("License GPLv3+: GNU GPL version 3 or later <https://www.gnu.org/licenses/gpl-3.0.html>.");
        Console.WriteLine("This is free software: you are free to change and redistribute it.");
        Console.WriteLine("There is NO WARRANTY, to the extent permitted by law.");
    }
}