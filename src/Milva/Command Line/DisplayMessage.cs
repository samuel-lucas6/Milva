using System;
using System.IO;

/*
    Milva: A simple, cross-platform command line tool for hashing files.
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

public static class DisplayMessage
{
    private const string ErrorWord = "Error";

    public static void Error(string message) => Console.WriteLine($"{ErrorWord}: {message}");

    public static void NamedError(string input, string message) => Console.WriteLine($"{Path.GetFileName(input)} - {ErrorWord}: {message}");

    public static void Message(string input, string message) => Console.WriteLine($"{Path.GetFileName(input)}: {message}");
}