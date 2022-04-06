[![License: GPL v3](https://img.shields.io/badge/License-GPL%20v3-green.svg)](http://www.gnu.org/licenses/gpl-3.0)
# Milva
A simple, cross-platform command line tool for hashing files and text.

![milva](https://user-images.githubusercontent.com/63159663/162065739-1ba96ed0-30af-4f85-a68b-64266d191f70.gif)

## Usage
```
Usage: milva [options] <inputs>

Arguments:
  inputs        specify files/directories or text

Options:
  --shake256    use SHAKE256
  --shake128    use SHAKE128
  --sha3512     use SHA3-512
  --sha3384     use SHA3-384
  --sha3256     use SHA3-256
  --blake3      use BLAKE3-256
  --blake2b512  use BLAKE2b-512
  --blake2b256  use BLAKE2b-256
  --sha512      use SHA512
  --sha384      use SHA384
  --sha256      use SHA256
  --sha1        use SHA1
  --md5         use MD5
  -t|--text     specify text instead of files/directories
  -a|--about    view the program version and license
  -h|--help     show help information

Examples:
  --sha256 [file]
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
