using System;
using System.IO;

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

namespace Milva
{
    public static class DisplayMessage
    {
        private const string _error = "Error";

        public static void Error(string errorMessage)
        {
            Console.WriteLine($"{_error}: {errorMessage}");
        }

        public static void FilePathError(string filePath, string message)
        {
            Console.WriteLine($"{Path.GetFileName(filePath)} - {_error}: {message}");
        }

        public static void FilePathMessage(string filePath, string message)
        {
            Console.WriteLine($"{Path.GetFileName(filePath)}: {message}");
        }
    }
}
