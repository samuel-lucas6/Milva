# Milva
Milva is a simple, cross-platform command line tool for hashing files.

## Usage
```
Usage: milva [options] [file]

Options:
  --blake512  retrieve the BLAKE2-512 of a file
  --blake256  retrieve the BLAKE2-256 of a file
  --sha512    retrieve the SHA512 of a file
  --sha384    retrieve the SHA384 of a file
  --sha256    retrieve the SHA256 of a file
  --sha1      retrieve the SHA1 of a file
  --md5       retrieve the MD5 of a file
  --about     view the version and license
  --help      show help information

Examples:
  --sha256 [file]
  --sha1 [file]
  --md5 [file]
```
When specifying file paths/file names that contain spaces, you must surround them with ' ' on Linux/macOS and " " on Windows.
```
$ ./milva --sha256 'Milva Logo.png'
$ ./milva --sha256 '/home/samuel/Documents/Milva Logo.png'
```

## Running Milva
On Linux & macOS:
```
$ cd Downloads
$ chmod +x milva
$ ./milva --help
```
On Windows:
```
$ cd Downloads
$ milva --help
```
