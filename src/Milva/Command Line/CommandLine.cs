using System;
using System.IO;
using System.Security;
using System.Text;

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

public static class CommandLine
{
    public static void HashEachInput(string[] inputs, bool text, HashFunction hashFunction)
    {
        if (inputs == null)
        {
            DisplayMessage.Error(!text ? "Please specify a file/directory to hash." : "Please specify text to hash.");
            return;
        }
        foreach (string input in inputs)
        {
            try
            {
                switch (text)
                {
                    case false when Directory.Exists(input):
                    {
                        string[] filePaths = Directory.GetFiles(input, searchPattern: "*", SearchOption.AllDirectories);
                        int arrayIndex = Array.IndexOf(inputs, input);
                        if (arrayIndex > 0) { Console.WriteLine(); }
                        DisplayMessage.Message(input, "Hashing each file in the directory...");
                        HashEachInput(filePaths, text: false, hashFunction);
                        if (arrayIndex != inputs.Length - 1) { Console.WriteLine(); }
                        continue;
                    }
                    case false when !File.Exists(input):
                        DisplayMessage.NamedError(input, "This file/directory path doesn't exist.");
                        continue;
                }
                using Stream stream = !text ? new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read, HashingAlgorithms.BufferSize, FileOptions.SequentialScan) : new MemoryStream(Encoding.UTF8.GetBytes(input));
                byte[] hash = HashingAlgorithms.GetHash(stream, hashFunction);
                DisplayMessage.Message(input, BitConverter.ToString(hash).Replace("-", "").ToLower());
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or SecurityException or NotSupportedException)
            {
                DisplayMessage.NamedError(input, ex.GetType().ToString());
            }
        }
    }
}