﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using BrowserForSlowNetwork;
using System.Reflection;
using System.ComponentModel;
using System.Media;
using System.Diagnostics;
using clay;


namespace Engine
{
    class BatchScript
    {
        static int SicherheitsSchalter;
        static string Code_;

        public static void Batch(string code)
        {
            Code_ = code;
            if(code != "")
            {
                Warnung();
            }
        }

        static void CodeAbbrechen()
        {
            return;
        }

        static void Warnung()
        {
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║                              VORSICHT                               ║");
            Console.WriteLine("    ║                                                                     ║");
            Console.WriteLine("    ║    Diese Seite versuchtein Skript auf ihrem Computer auszufüren     ║");
            Console.WriteLine("    ║       Das kann Schwere Schäden auf ihrem Computer verursachen       ║");
            Console.WriteLine("    ║      Bei Wahl Ja ist zu empfehlen das sie der Seite Vertrauen       ║");
            Console.WriteLine("    ║                                                                     ║");
            Console.WriteLine("    ║           j = JA       n = Nein     b = Skriptcode Zeigen           ║");
            Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
            Console.Write("                                     >");
            switch(Console.ReadLine().ToLower())
            {
                case "j":
                    CodeAusführen();
                    break;
                case "n":
                    break;
                case "b":
                    CodeAnsehen();
                    break;
            }
        }

        static void CodeAnsehen()
        {
            Console.Clear();
            Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║                          Skriptcode = Batch                         ║");
            Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝\n");
            Console.WriteLine(Code_);
            Console.ReadKey();
            Console.WriteLine("Wollen sie diesen Code Wirklich ausfüren?\nj = JA n = NEIN");
            string WirklichAusfüren = Console.ReadLine().ToLower() ;
            if (WirklichAusfüren != "j")
            {
                CodeAbbrechen();
            }
            CodeDateiSchreiben();
        }

        static void CodeDateiSchreiben()
        {
            Console.Clear();
            try
            {
                if (File.Exists("skript.bat"))
                {
                    File.Delete("skript.bat");
                }

				/*string CodeInDatei = "";*/ Code_ = Code_.Replace("\n", "\r\n");
                using (StreamWriter writer = new StreamWriter("skript.bat"))
                {
                    writer.Write(Code_);
                }

            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Fehler beim Schreiben der Skript Datei, bitte Kontaktieren sie den Administrator");
                Console.ReadKey();
                Routine.RestartRoutine();

            }
        }

        static void CodeAusführen()
        {
            
                Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("    ║                           Skript Output                             ║");
                Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
                
                var process = new Process();
                var startinfo = new ProcessStartInfo("cmd.exe", @"/C skript.bat");
                startinfo.RedirectStandardOutput = true;
                startinfo.UseShellExecute = false;
                process.StartInfo = startinfo;
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
                
                Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("    ║                         Skript Output Ende                          ║");
                Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
        }


    }


    public class ClayScript
    {
        public static void StartClayRT(string code)
        {
            Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║                           Clay   Output                             ║");
            Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");

            RunClayCode(code);
        }


        static void RunClayCode(string code)
        {
            var clayrt = new Runtime();
            clayrt.Run(code);
        }
    }


}
