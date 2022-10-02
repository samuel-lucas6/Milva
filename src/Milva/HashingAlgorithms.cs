using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using Blake3;
using Geralt;

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

public static class HashingAlgorithms
{
    private const int BufferSize = 65536;
    
    public static byte[] GetHash(Stream stream, HashFunction hashFunction)
    {
        return hashFunction switch
        {
            HashFunction.SHAKE256 => GetSHAKE(stream, 64),
            HashFunction.SHAKE128 => GetSHAKE(stream, 32),
            HashFunction.SHA3_512 => GetSHA3(stream, 64),
            HashFunction.SHA3_384 => GetSHA3(stream, 48),
            HashFunction.SHA3_256 => GetSHA3(stream, 32),
            HashFunction.BLAKE3 => GetBLAKE3(stream),
            HashFunction.BLAKE2b512 => GetBLAKE2b(stream, 64),
            HashFunction.BLAKE2b384 => GetBLAKE2b(stream, 48),
            HashFunction.BLAKE2b256 => GetBLAKE2b(stream, 32),
            HashFunction.BLAKE2b160 => GetBLAKE2b(stream, 20),
            HashFunction.SHA512 => GetSHA512(stream),
            HashFunction.SHA384 => GetSHA384(stream),
            HashFunction.SHA256 => GetSHA256(stream),
            HashFunction.SHA1 => GetSHA1(stream),
            HashFunction.MD5 => GetMD5(stream),
            _ => null
        };
    }

    private static byte[] GetSHAKE(Stream stream, int hashSize)
    {
        int bytesRead;
        var buffer = new byte[BufferSize];
        var shake = new ShakeDigest(hashSize * 4);
        while ((bytesRead = stream.Read(buffer)) > 0)
        {
            shake.BlockUpdate(buffer, inOff: 0, bytesRead);
        }
        var hash = new byte[hashSize];
        shake.DoFinal(hash, outOff: 0);
        return hash;
    }

    private static byte[] GetSHA3(Stream stream, int hashSize)
    {
        int bytesRead;
        var buffer = new byte[BufferSize];
        var sha3 = new Sha3Digest(hashSize * 8);
        while ((bytesRead = stream.Read(buffer)) > 0)
        {
            sha3.BlockUpdate(buffer, inOff: 0, bytesRead);
        }
        var hash = new byte[hashSize];
        sha3.DoFinal(hash, outOff: 0);
        return hash;
    }

    private static byte[] GetBLAKE3(Stream stream)
    {
        int bytesRead;
        var buffer = new byte[BufferSize];
        using var backendStream = new MemoryStream();
        using var blake3 = new Blake3Stream(backendStream);
        while ((bytesRead = stream.Read(buffer)) > 0)
        {
            blake3.Write(buffer, offset: 0, bytesRead);
        }
        var hash = blake3.ComputeHash();
        return hash.AsSpanUnsafe().ToArray();
    }

    private static byte[] GetBLAKE2b(Stream stream, int hashSize)
    {
        using var blake2b = new BLAKE2bHashAlgorithm(hashSize);
        return blake2b.ComputeHash(stream);
    }

    private static byte[] GetSHA512(Stream stream)
    {
        using var sha512 = SHA512.Create();
        return sha512.ComputeHash(stream);
    }

    private static byte[] GetSHA384(Stream stream)
    {
        using var sha384 = SHA384.Create();
        return sha384.ComputeHash(stream);
    }

    private static byte[] GetSHA256(Stream stream)
    {
        using var sha256 = SHA256.Create();
        return sha256.ComputeHash(stream);
    }

    private static byte[] GetSHA1(Stream stream)
    {
        using var sha1 = SHA1.Create();
        return sha1.ComputeHash(stream);
    }

    private static byte[] GetMD5(Stream stream)
    {
        using var md5 = MD5.Create();
        return md5.ComputeHash(stream);
    }
}