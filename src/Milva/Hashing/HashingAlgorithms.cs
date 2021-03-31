using System.IO;
using System.Security.Cryptography;
using Blake3;
using Sodium;

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
    public static class HashingAlgorithms
    {
        public static byte[] BLAKE3(FileStream fileStream)
        {
            int bytesRead;
            var buffer = new byte[131072];
            using var memoryStream = new MemoryStream();
            using var blake3 = new Blake3Stream(memoryStream);
            while ((bytesRead = fileStream.Read(buffer, offset: 0, buffer.Length)) > 0)
            {
                blake3.Write(buffer, offset: 0, bytesRead);
            }
            var hash = blake3.ComputeHash();
            return hash.AsSpan().ToArray();
        }

        public static byte[] BLAKE2b512(FileStream fileStream)
        {
            using var blake2b = new GenericHash.GenericHashAlgorithm(key: (byte[])null, bytes: 64);
            return blake2b.ComputeHash(fileStream);
        }

        public static byte[] BLAKE2b256(FileStream fileStream)
        {
            using var blake2b = new GenericHash.GenericHashAlgorithm(key: (byte[])null, bytes: 32);
            return blake2b.ComputeHash(fileStream);
        }

        public static byte[] SHA512(FileStream fileStream)
        {
            using var sha512 = new SHA512CryptoServiceProvider();
            return sha512.ComputeHash(fileStream);
        }

        public static byte[] SHA384(FileStream fileStream)
        {
            using var sha384 = new SHA384CryptoServiceProvider();
            return sha384.ComputeHash(fileStream);
        }

        public static byte[] SHA256(FileStream fileStream)
        {
            using var sha256 = new SHA256CryptoServiceProvider();
            return sha256.ComputeHash(fileStream);
        }

        public static byte[] SHA1(FileStream fileStream)
        {
            using var sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(fileStream);
        }

        public static byte[] MD5(FileStream fileStream)
        {
            using var md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(fileStream);
        }
    }
}
