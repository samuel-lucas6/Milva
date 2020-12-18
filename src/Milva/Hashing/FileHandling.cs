using System;
using System.IO;

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
    public static class FileHandling
    {
        public static byte[] HashFile(string filePath, Program.HashFunction hashFunction)
        {
            try
            {
                const int bufferSize = 131072;
                using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan);
                return GetHash(fileStream, hashFunction);
            }
            catch (Exception ex) when (ExceptionFilters.FileAccess(ex))
            {
                Console.WriteLine($"{Path.GetFileName(filePath)} - Error: {ex.GetType()}");
                return null;
            }
        }

        private static byte[] GetHash(FileStream fileStream, Program.HashFunction hashFunction)
        {
            return hashFunction switch
            {
                Program.HashFunction.BLAKE512 => HashingAlgorithms.BLAKE512(fileStream),
                Program.HashFunction.BLAKE256 => HashingAlgorithms.BLAKE256(fileStream),
                Program.HashFunction.SHA512 => HashingAlgorithms.SHA512(fileStream),
                Program.HashFunction.SHA384 => HashingAlgorithms.SHA384(fileStream),
                Program.HashFunction.SHA256 => HashingAlgorithms.SHA256(fileStream),
                Program.HashFunction.SHA1 => HashingAlgorithms.SHA1(fileStream),
                Program.HashFunction.MD5 => HashingAlgorithms.MD5(fileStream),
                _ => null,
            };
        }
    }
}
