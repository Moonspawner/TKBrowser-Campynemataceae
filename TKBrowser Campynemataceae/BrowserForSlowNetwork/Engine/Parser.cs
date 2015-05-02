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

namespace BrowserForSlowNetwork
{
    class Engine
    {
        public static bool inbatch;
        public static bool tellnoskript;
        public static string CodeInDatei;
        public static void Parser()
        {
            bool incode = false;
            bool inhead = false;
            bool incode2 = false;
            bool inbox = false;
            bool inbeep = false;
            int beepcounter = 1;
            int beep2 = 0;
            bool finishbeep = false;
            bool inhyperlink = false;



            var tags = new List<string>();

            var code = new List<string>();
            string text = "";
            bool intitle = false;
            //bool readtitle = false;



            foreach (var zeile in CoreClass.FileSpace.Split('\n'))
            {
                if (zeile.Trim() == "<!— ignorerest —>")
                {
                    break;
                }


                //Plugin Support
                //if (CoreClass.plugins != null)
                //{
                //    foreach (var plugin in plugins)
                //    {
                //        plugin.ZeilenAbruf(zeile, ref text, incode || incode2, inhead, intitle);
                //    }
                //}
                if (zeile.Trim() == "<head>")
                {
                    inhead = true;
                }
                if (zeile.Trim() == "</head>")
                {
                    inhead = false;
                    continue;
                }
                if (inhead == true)
                {

                    if (zeile.Trim() == "<title>")
                    {
                        intitle = true;
                    }
                    if (zeile.Trim() == "</title>")
                    {
                        intitle = false;
                    }
                    else if (intitle == true)
                    {
                        Console.Title = zeile;
                    }
                }
                if (inhead == false)
                {

                    if (zeile.Trim() == "<link>")
                    {
                        inhyperlink = true;
                        continue;
                    }
                    if (zeile.Trim() == "</link>")
                    {
                        inhyperlink = false;
                        continue;
                    }
                    if (inhyperlink == true)
                    {

                    }



                    if (zeile.Trim() == "<text>")
                    {
                        continue;
                    }
                    if (zeile.Trim() == "</text>")
                    {
                        continue;
                    }

                    if (zeile.Trim() == "<beep>")
                    {
                        inbeep = true;
                        continue;
                    }
                    if (zeile.Trim() == "</beep>")
                    {
                        inbeep = false;
                        continue;
                    }
                    if (zeile.Trim() == "<box>")
                    {
                        inbox = true;
                        continue;

                    }
                    if (zeile.Trim() == "</box>")
                    {
                        inbox = false;
                        OutputBox.Ausgabe();
                        continue;
                    }
                    if (inbox == true)
                    {
                        OutputBox.Nachricht(zeile);
                        continue;
                    }

                    //Schinken
                    if (inbeep == true)
                    {
                        if (beepcounter == 1)
                        {
                            beep1 = Int32.Parse(zeile);
                            beepcounter++;
                            continue;
                        }
                        if (beepcounter == 2)
                        {
                            finishbeep = true;
                            beep2 = Int32.Parse(zeile);
                        }
                        if (finishbeep == true)
                        {
                            Console.Beep(beep1, beep2);
                        }
                        continue;
                    }
                    if (incode2 == true)
                    {

                        if (zeile.Trim() == "<skriptIO=true>")
                        {
                            tellnoskript = true;
                            continue;
                        }

                        if (zeile.Trim() == "</skriptIO=false>")
                        {
                            tellnoskript = false;
                            continue;
                        }
                        if (zeile.Trim() == "<skriptIO=false>")
                        {
                            tellnoskript = true;
                            continue;
                        }

                        if (zeile.Trim() == "</skriptIO=false>")
                        {
                            tellnoskript = false;
                            continue;
                        }


                        if (zeile != "<batch>" && zeile != "</batch>")
                        {
                            CodeInDatei = CodeInDatei + zeile + "\n";
                        }
                    }

                    if (zeile.Trim() == "<batch>")
                    {
                        CodeInDatei = "";
                        incode2 = true;
                        incode = true;
                    }
                    else if (zeile.Trim() == "</batch>")
                    {
                         CoreClass.CodeWriter();
                        //datei ausführen und den consolen-output hier hin ausgeben
                        incode2 = false;
                        incode = false;
                    }
                    else if (incode)
                    {
                        if (true) { ;}
                    }
                    else
                    {
                        text += zeile + "\n";
                    }
                }
            }

            CoreClass.FileSpace = text;
            tags = Regex.Split(CoreClass.FileSpace, "<|>").ToList();
            foreach (var tag in tags.Select(t => t.Trim()))
            {
                if (tag.StartsWith("color="))
                {
                    Color(tag);
                }
                else if (tag.StartsWith("/color"))
                {
                    Console.ResetColor();
                }
                else
                {
                    //Ausgabe
                    Console.WriteLine(tag);
                }
            }
        }

        public static void Color(string tagcontent)
        {
            switch (tagcontent) //<Color=&2> TextReader bla bla </Color>
            {
                case "color=&0":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "color=&1":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "color=&2":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "color=&3":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "color=&4":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "color=&5":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "color=&6":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "color=&7":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "color=&8":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "color=&9":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "color=&a":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "color=&b":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "color=&c":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "color=&d":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "color=&e":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "color=&f":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "color=&r":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public static int beep1 { get; set; }
    }
}