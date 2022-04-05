using System.IO;
using System.Security.Cryptography;
using Blake3;
using Sodium;
using Org.BouncyCastle.Crypto.Digests;

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

public static class HashingAlgorithms
{
    public const int BufferSize = 131072;

    public static byte[] GetHash(FileStream fileStream, HashFunction hashFunction)
    {
        return hashFunction switch
        {
            HashFunction.SHAKE256 => GetSHAKE(fileStream, 256),
            HashFunction.SHAKE128 => GetSHAKE(fileStream, 128),
            HashFunction.SHA3_512 => GetSHA3(fileStream, 512),
            HashFunction.SHA3_384 => GetSHA3(fileStream, 384),
            HashFunction.SHA3_256 => GetSHA3(fileStream, 256),
            HashFunction.BLAKE3 => GetBLAKE3(fileStream),
            HashFunction.BLAKE2b512 => GetBLAKE2b(fileStream, 64),
            HashFunction.BLAKE2b256 => GetBLAKE2b(fileStream, 32),
            HashFunction.SHA512 => GetSHA512(fileStream),
            HashFunction.SHA384 => GetSHA384(fileStream),
            HashFunction.SHA256 => GetSHA256(fileStream),
            HashFunction.SHA1 => GetSHA1(fileStream),
            HashFunction.MD5 => GetMD5(fileStream),
            _ => null,
        };
    }

    private static byte[] GetSHAKE(FileStream fileStream, int outputBitLength)
    {
        int bytesRead;
        var buffer = new byte[BufferSize];
        var shake = new ShakeDigest(outputBitLength);
        while ((bytesRead = fileStream.Read(buffer, offset: 0, buffer.Length)) > 0)
        {
            shake.BlockUpdate(buffer, inOff: 0, bytesRead);
        }
        var hash = new byte[outputBitLength / 4];
        shake.DoFinal(hash, outOff: 0);
        return hash;
    }

    private static byte[] GetSHA3(FileStream fileStream, int outputBitLength)
    {
        int bytesRead;
        var buffer = new byte[BufferSize];
        var sha3 = new Sha3Digest(outputBitLength);
        while ((bytesRead = fileStream.Read(buffer, offset: 0, buffer.Length)) > 0)
        {
            sha3.BlockUpdate(buffer, inOff: 0, bytesRead);
        }
        var hash = new byte[outputBitLength / 8];
        sha3.DoFinal(hash, outOff: 0);
        return hash;
    }

    private static byte[] GetBLAKE3(FileStream fileStream)
    {
        int bytesRead;
        var buffer = new byte[BufferSize];
        using var memoryStream = new MemoryStream();
        using var blake3 = new Blake3Stream(memoryStream);
        while ((bytesRead = fileStream.Read(buffer, offset: 0, buffer.Length)) > 0)
        {
            blake3.Write(buffer, offset: 0, bytesRead);
        }
        var hash = blake3.ComputeHash();
        return hash.AsSpanUnsafe().ToArray();
    }

    private static byte[] GetBLAKE2b(FileStream fileStream, int outputBytesLength)
    {
        using var blake2b = new GenericHash.GenericHashAlgorithm(key: (byte[])null, outputBytesLength);
        return blake2b.ComputeHash(fileStream);
    }

    private static byte[] GetSHA512(FileStream fileStream) => SHA512.Create().ComputeHash(fileStream);

    private static byte[] GetSHA384(FileStream fileStream) => SHA384.Create().ComputeHash(fileStream);

    private static byte[] GetSHA256(FileStream fileStream) => SHA256.Create().ComputeHash(fileStream);

    private static byte[] GetSHA1(FileStream fileStream) => SHA1.Create().ComputeHash(fileStream);

    private static byte[] GetMD5(FileStream fileStream) => MD5.Create().ComputeHash(fileStream);
}