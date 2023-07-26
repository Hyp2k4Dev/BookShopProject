using System;
using System.Collections.Generic;
using Spectre.Console;
using System.IO;
using System.Text;
using Persistence;
using BL;
using Utilities;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            Ults ults = new Ults();
            ults.Login();
        }
    }
}