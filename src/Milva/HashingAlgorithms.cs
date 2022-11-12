using System;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto;
using static Monocypher.Monocypher;
using Blake3;

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
    public static byte[] GetHash(Stream stream, HashFunction hashFunction)
    {
        return hashFunction switch
        {
            HashFunction.BLAKE3 => GetBLAKE3(stream),
            HashFunction.SHAKE256 => GetBouncyCastleHash(stream, new ShakeDigest(256), 64),
            HashFunction.SHAKE128 => GetBouncyCastleHash(stream, new ShakeDigest(128), 32),
            HashFunction.SHA3_512 => GetBouncyCastleHash(stream, new Sha3Digest(512), 64),
            HashFunction.SHA3_384 => GetBouncyCastleHash(stream, new Sha3Digest(384), 48),
            HashFunction.SHA3_256 => GetBouncyCastleHash(stream, new Sha3Digest(256), 32),
            HashFunction.BLAKE2b512 => GetBLAKE2b(stream, 64),
            HashFunction.BLAKE2b384 => GetBLAKE2b(stream, 48),
            HashFunction.BLAKE2b256 => GetBLAKE2b(stream, 32),
            HashFunction.BLAKE2b160 => GetBLAKE2b(stream, 20),
            HashFunction.BLAKE2s256 => GetBouncyCastleHash(stream, new Blake2sDigest(256), 32),
            HashFunction.BLAKE2s224 => GetBouncyCastleHash(stream, new Blake2sDigest(224), 28),
            HashFunction.BLAKE2s160 => GetBouncyCastleHash(stream, new Blake2sDigest(160), 20),
            HashFunction.BLAKE2s128 => GetBouncyCastleHash(stream, new Blake2sDigest(128), 16),
            HashFunction.SHA512 => GetSHA512(stream),
            HashFunction.SHA384 => GetSHA384(stream),
            HashFunction.SHA256 => GetSHA256(stream),
            HashFunction.Whirlpool => GetBouncyCastleHash(stream, new WhirlpoolDigest(), 64),
            HashFunction.RIPEMD320 => GetBouncyCastleHash(stream, new RipeMD320Digest(), 40),
            HashFunction.RIPEMD256 => GetBouncyCastleHash(stream, new RipeMD256Digest(), 32),
            HashFunction.RIPEMD160 => GetBouncyCastleHash(stream, new RipeMD160Digest(), 20),
            HashFunction.RIPEMD128 => GetBouncyCastleHash(stream, new RipeMD128Digest(), 16),
            HashFunction.SHA1 => GetSHA1(stream),
            HashFunction.MD5 => GetMD5(stream),
            _ => null
        };
    }
    
    private static int GetBufferSize(long streamLength)
    {
        return streamLength switch
        {
            < 16384 => 4096,
            < 32768 => 16384,
            < 65536 => 32768,
            >= 65536 => 65536
        };
    }
    
    private static byte[] GetBouncyCastleHash(Stream stream, IDigest digest, int hashSize)
    {
        int bytesRead;
        var buffer = new byte[GetBufferSize(stream.Length)];
        while ((bytesRead = stream.Read(buffer)) > 0) {
            digest.BlockUpdate(buffer, inOff: 0, bytesRead);
        }
        var hash = new byte[hashSize];
        digest.DoFinal(hash, outOff: 0);
        return hash;
    }

    private static byte[] GetBLAKE3(Stream stream)
    {
        int bytesRead;
        var buffer = new byte[GetBufferSize(stream.Length)];
        using var blake3 = new Blake3Stream(new MemoryStream());
        while ((bytesRead = stream.Read(buffer)) > 0) {
            blake3.Write(buffer, offset: 0, bytesRead);
        }
        var hash = blake3.ComputeHash();
        return hash.AsSpanUnsafe().ToArray();
    }

    private static byte[] GetBLAKE2b(Stream stream, int hashSize)
    {
        int bytesRead;
        var ctx = new crypto_blake2b_ctx();
        crypto_blake2b_general_init(ref ctx, hashSize, IntPtr.Zero, key_size: 0);
        Span<byte> buffer = new byte[GetBufferSize(stream.Length)];
        while ((bytesRead = stream.Read(buffer)) > 0) {
            crypto_blake2b_update(ref ctx, buffer[..bytesRead]);
        }
        var hash = new byte[hashSize];
        crypto_blake2b_final(ref ctx, hash);
        return hash;
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