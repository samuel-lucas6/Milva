using System;
using System.IO;

/*
    Milva: A simple, cross-platform command line tool for hashing files.
    Copyright(C) 2020-2021 Samuel Lucas

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
        public static byte[] HashFile(string filePath, HashFunction hashFunction)
        {
            try
            {
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 131072, FileOptions.SequentialScan);
                return GetHash(fileStream, hashFunction);
            }
            catch (Exception ex) when (ExceptionFilters.FileAccess(ex))
            {
                Console.WriteLine($"{Path.GetFileName(filePath)} - Error: {ex.GetType()}.");
                return null;
            }
        }

        private static byte[] GetHash(FileStream fileStream, HashFunction hashFunction)
        {
            return hashFunction switch
            {
                HashFunction.BLAKE3 => HashingAlgorithms.BLAKE3(fileStream),
                HashFunction.BLAKE2b512 => HashingAlgorithms.BLAKE2b512(fileStream),
                HashFunction.BLAKE2b256 => HashingAlgorithms.BLAKE2b256(fileStream),
                HashFunction.SHA512 => HashingAlgorithms.SHA512(fileStream),
                HashFunction.SHA384 => HashingAlgorithms.SHA384(fileStream),
                HashFunction.SHA256 => HashingAlgorithms.SHA256(fileStream),
                HashFunction.SHA1 => HashingAlgorithms.SHA1(fileStream),
                HashFunction.MD5 => HashingAlgorithms.MD5(fileStream),
                _ => null,
            };
        }
    }
}
