using System.IO;
using System.Security.Cryptography;
using Sodium;

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
    public static class HashingAlgorithms
    {
        public static byte[] BLAKE512(FileStream fileStream)
        {
            const int hashLength = 64;
            using var blake2 = new GenericHash.GenericHashAlgorithm(key:(byte[])null, hashLength);
            return blake2.ComputeHash(fileStream);
        }

        public static byte[] BLAKE256(FileStream fileStream)
        {
            const int hashLength = 32;
            using var blake2 = new GenericHash.GenericHashAlgorithm(key:(byte[])null, hashLength);
            return blake2.ComputeHash(fileStream);
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
