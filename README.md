[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-green.svg)](https://github.com/samuel-lucas6/Milva/blob/main/LICENSE)

# Milva

A simple, cross-platform command line tool for hashing files and text.

![milva](https://user-images.githubusercontent.com/63159663/162065739-1ba96ed0-30af-4f85-a68b-64266d191f70.gif)

## Usage

```
Usage: milva [options] <inputs>

Arguments:
  inputs         specify files/directories or text

Options:
  --blake3       use BLAKE3-256
  --shake256     use SHAKE256
  --shake128     use SHAKE128
  --sha3-512     use SHA3-512
  --sha3-384     use SHA3-384
  --sha3-256     use SHA3-256
  --blake2b-512  use BLAKE2b-512
  --blake2b-384  use BLAKE2b-384
  --blake2b-256  use BLAKE2b-256
  --blake2b-160  use BLAKE2b-160
  --blake2s-256  use BLAKE2s-256
  --blake2s-224  use BLAKE2s-224
  --blake2s-160  use BLAKE2s-160
  --blake2s-128  use BLAKE2s-128
  --sha512       use SHA-512
  --sha384       use SHA-384
  --sha256       use SHA-256
  --whirlpool    use Whirlpool
  --ripemd-320   use RIPEMD-320
  --ripemd-256   use RIPEMD-256
  --ripemd-160   use RIPEMD-160
  --ripemd-128   use RIPEMD-128
  --sha1         use SHA-1
  --md5          use MD5
  -t|--text      specify text instead of files/directories
  -a|--about     view the program version and license
  -h|--help      show help information

Examples:
  --sha256 [file]
  --sha256 [directory]
  --sha256 --text [text]
```

When specifying file names/paths or text containing spaces, you must surround them with "speech marks":

```
$ milva --sha256 "GitHub Logo.png"
$ milva --sha256 "C:\Users\samuel-lucas6\Pictures\GitHub Logo.png"
```

## Running Milva

On Windows via the Command Prompt:

```
$ cd "[download path]"
$ milva -h
```

On Linux and macOS via the terminal:

```
$ cd "[download path]"
$ chmod +x milva
$ ./milva -h
```
