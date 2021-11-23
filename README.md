[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-green.svg)](http://www.gnu.org/licenses/gpl-3.0)
# Milva
A simple, cross-platform command line tool for hashing files.

## Usage
```
Usage: milva [options] <file>

Arguments:
  file          specify a file path

Options:
  --shake256    hash a file using SHAKE256
  --shake128    hash a file using SHAKE128
  --sha3512     hash a file using SHA3-512
  --sha3384     hash a file using SHA3-384
  --sha3256     hash a file using SHA3-256
  --blake3      hash a file using BLAKE3-256
  --blake2b512  hash a file using BLAKE2b-512
  --blake2b256  hash a file using BLAKE2b-256
  --sha512      hash a file using SHA512
  --sha384      hash a file using SHA384
  --sha256      hash a file using SHA256
  --sha1        hash a file using SHA1
  --md5         hash a file using MD5
  -a|--about    view the program version and license
  -h|--help     show help information

Examples:
  --sha256 [file]
  --sha1 [file]
  --md5 [file]
```
When specifying file paths/file names that contain spaces, you must surround them with "speech marks" on Windows and 'apostrophes' on Linux/macOS:
```
$ milva --sha256 'GitHub Logo.png'
$ milva --sha256 '/home/samuel/Pictures/GitHub Logo.png'
```

## Running Milva
On Windows:
```
$ cd "[download path]"
$ milva -h
```
On Linux & macOS:
```
$ cd '[download path]'
$ chmod +x milva
$ ./milva -h
```
