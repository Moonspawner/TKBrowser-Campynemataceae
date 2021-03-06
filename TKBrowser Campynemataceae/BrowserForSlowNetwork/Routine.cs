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
    class Routine
    {
        //Einstellungen
        static bool ForcedRoutine = false;
        static bool TestRoutine = false;
        //Variablen
        static string FehlerCode;

        static private Engine.Parsing_ _parsing = new Engine.Parsing_();

        public static void StartRoutine(bool force)
        {
            Routine_();
        }

        public static void RestartRoutine()
        {
            Console.Clear();
            Routine_();
        }

        static void Routine_()
        {
            if(TestRoutine == true)
            {
                TKBrowser.UpdateModul.StartUpdateProzess("","");
                Console.ReadLine();
                //Engine.ClayScript.StartClayRT("tell LOLOLOLOLOLOLOL");
                //CoreClass.Init();
				//Lesezeichen.startLesezeichen ();
                CoreClass.FileSpace = CoreNetzwerk_.GetSite(Console.ReadLine());
                _parsing.Parser1(CoreClass.FileSpace);
                //CoreClass.Ausgabe();
                RestartRoutine();
            }
                
            if (ForcedRoutine == true)
            {
                CoreClass.Init();
                //CoreNetzwerk.Aufrufen();
                //CoreNetzwerk.Downloader();
				CoreClass.Startscreen();
				CoreClass.FileSpace = CoreNetzwerk_.GetSite(CoreClass.Eingabe);
                Engine.Parsing.Parser(CoreClass.FileSpace);
				//Console.WriteLine(CoreClass.FileSpace);
                CoreClass.Ausgabe();
                RestartRoutine();
                
            }
            else
            {
                try
                {
                    TKBrowser.UpdateModul.StartUpdateProzess("", "");
                }
                catch
                {
                    Console.WriteLine("Fehler bei dem Versuch ClayRuntime zu Aktualisieren");
                }
                try
                {
                CoreClass.Init();
                }
                catch(Exception ex1)
                {
                   //FehlerSwitch = 1;
                   FehlerCode = ex1.ToString();
                   TickfehlerAusgabe();
                }
				//Weil man sonst keinen Link eingeben kann
				try
				{
					CoreClass.Startscreen();
				}
				catch(Exception ex2)
				{
					//FehlerSwitch = 2;
					FehlerCode = ex2.ToString();
					TickfehlerAusgabe();
				}

                try
                {
					//geändert weil alter funkt nicht
					CoreClass.FileSpace = CoreNetzwerk_.GetSite(CoreClass.Eingabe);
                }
                    catch (Exception ex3)
                {
                    //FehlerSwitch = 3;
                	FehlerCode = ex3.ToString();
                	TickfehlerAusgabe();
                }
				//Weil unnötig, da im neuen Code der Downloader schon integriert ist
                /*try
                {
                    CoreNetzwerk.Downloader();
                }
                catch (Exception ex3)
                {
                    FehlerSwitch = 3;
                    FehlerCode = ex3.ToString();
                    TickfehlerAusgabe();
                }*/

                try
                {
                    Engine.Parsing.Parser(CoreClass.FileSpace);
                }
                catch (Exception ex4)
                {
                    //FehlerSwitch = 4;
                    FehlerCode = ex4.ToString();
                    TickfehlerAusgabe();
                }

                try
                {
                    CoreClass.Ausgabe();
                }
                catch (Exception ex5)
                {
                    //FehlerSwitch = 5;
                    FehlerCode = ex5.ToString();
                    TickfehlerAusgabe();
                }
            }
            
            RestartRoutine();
        }

        public static void Manuell(string url)
        {
            CoreClass.FileSpace = CoreNetzwerk_.GetSite(url);
            Engine.Parsing.Parser(CoreClass.FileSpace);
            CoreClass.Ausgabe();
        }

        static void TickfehlerAusgabe()
        {
            Console.Clear();
            Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("    ║                  Schwerer fehler im Browser Tick                    ║");
            Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("Fehler: ", FehlerCode);
            Console.ReadKey();
        }
    }
}
