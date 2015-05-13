using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DiscUtils.Iso9660;

namespace DirToIso
{
    class Program
    {
        static Dictionary<string, string> options = new Dictionary<string, string>
        {
            {"d", "Specifies input directory path"},
            {"o", "Specifies output filename"},
            {"id", "Specifies an output volume identifier"},
            {"h", "Shows this help view."}
        };

        private static void Main(string[] args)
        {
            var inputDir = Directory.GetCurrentDirectory();
            var outputFn = Path.Combine(inputDir, Path.ChangeExtension(Path.GetFileName(inputDir), "iso"));
            var identifier = Path.GetFileName(inputDir);

            var lastArg = string.Empty;
            foreach (var arg in args)
            {
                if (arg.TrimStart('-').Trim().ToLower() == "h")
                {
                    ShowHelp();
                    return;
                }
                switch (lastArg)
                {
                    case "d":
                        inputDir = arg;
                        break;
                    case "o":
                        outputFn = arg;
                        break;
                    case "id":
                        identifier = arg;
                        break;
                }
                lastArg = arg.TrimStart('-').Trim().ToLower();
            }
            if (identifier.Length > 32)
                identifier = identifier.Substring(0, 32);
            var builder = new CDBuilder
            {
                UseJoliet = true,
                VolumeIdentifier = Regex.Replace(identifier, "[^\\w|_]", "").Trim().ToUpper()
            };
            MakeIso(builder, inputDir, inputDir);
            builder.Build(outputFn);
        }

        static void ShowHelp()
        {
            Console.WriteLine("Syntax: dirtoiso [options]");
            Console.WriteLine("Options:");
            foreach (var option in options)
            {
                Console.WriteLine("\t-{0}: {1}.", option.Key, option.Value);
            }
        }

        static void MakeIso(CDBuilder builder, string topdir, string dir, bool inner = false)
        {
            foreach (var directory in Directory.GetDirectories(dir))
            {
                MakeIso(builder, topdir, directory, true);
            }
            foreach (var file in Directory.GetFiles(dir))
            {
                var fn = file.Replace(topdir, "");
                builder.AddFile(fn, file);
            }
        }
    }
}
