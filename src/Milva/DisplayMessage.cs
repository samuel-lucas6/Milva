﻿using System;
using System.IO;
using System.Reflection;

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

public static class DisplayMessage
{
    private const int ErrorCode = -1;
    
    public static void Error(string message)
    {
        Environment.ExitCode = ErrorCode;
        Console.WriteLine($"Error: {message}");
    }
    
    public static void NamedError(string input, string message) => Error($"{Path.GetFileName(input)} - {message}");

    public static void Message(string input, string message) => Console.WriteLine($"{Path.GetFileName(input)}: {message}");

    public static void About()
    {
        string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString(fieldCount: 3);
        Console.WriteLine($"Milva v{version}");
        Console.WriteLine("Copyright (C) 2020-2022 Samuel Lucas");
        Console.WriteLine("License GPLv3+: GNU GPL version 3 or later <https://www.gnu.org/licenses/gpl-3.0.html>.");
        Console.WriteLine("This is free software: you are free to change and redistribute it.");
        Console.WriteLine("There is NO WARRANTY, to the extent permitted by law.");
    }
}