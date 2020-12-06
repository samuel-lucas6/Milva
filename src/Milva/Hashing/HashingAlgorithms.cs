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
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see https://www.gnu.org/licenses/. 
*/

namespace Milva
{
    public static class HashingAlgorithms
    {
        public static byte[] BLAKE2(string filePath, int hashLength)
        {
            using var blake2 = new GenericHash.GenericHashAlgorithm(key:(byte[])null, hashLength);
            return FileHandling.HashFile(filePath, blake2);
        }

        public static byte[] SHA512(string filePath)
        {
            using var sha512 = new SHA512CryptoServiceProvider();
            return FileHandling.HashFile(filePath, sha512);
        }

        public static byte[] SHA384(string filePath)
        {
            using var sha384 = new SHA384CryptoServiceProvider();
            return FileHandling.HashFile(filePath, sha384);
        }

        public static byte[] SHA256(string filePath)
        {
            using var sha256 = new SHA256CryptoServiceProvider();
            return FileHandling.HashFile(filePath, sha256);
        }

        public static byte[] SHA1(string filePath)
        {
            using var sha1 = new SHA1CryptoServiceProvider();
            return FileHandling.HashFile(filePath, sha1);
        }

        public static byte[] MD5(string filePath)
        {
            using var md5 = new MD5CryptoServiceProvider();
            return FileHandling.HashFile(filePath, md5);
        }
    }
}
