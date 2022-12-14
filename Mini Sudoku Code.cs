using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Services;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;
using System.Security.Policy;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;

namespace Arbeitsuftrag_Neun_Felder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //----------------------------------------INTRO
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int[] Feld = new int[9];
            bool[] Feld2 = new bool[9];
            bool[] Zahl = new bool[10];
            int[,] Feld3D = new int[3, 3];
            int[,] CursorPosition = new int[2, 9] { { 2, 6, 10, 2, 6, 10, 2, 6, 10 },
                                                        { 1, 1, 1, 3, 3, 3, 5, 5, 5 } };
            bool[,] FalscheSpaltenReihen = new bool[2, 3];
            int Reihe, Spalte, Nummer;
            string FeldKorektur, Wiederholen;
            bool GamingRegel;
            ConsoleKeyInfo Key;
            do
            {
                //Erstellung der Variablen

                //----------------------------------------EINGABE
                //Anzeige des Feldes
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("┌───┬───┬───┐");
                Console.WriteLine("│   │   │   │"); //2, 6, 10
                Console.WriteLine("├───┼───┼───┤");
                Console.WriteLine("│   │   │   │");
                Console.WriteLine("├───┼───┼───┤");
                Console.WriteLine("│   │   │   │");
                Console.WriteLine("└───┴───┴───┘");
                Console.WriteLine("\n     (Bitte geben Sie Ihre Zahlen ein)");
                Console.WriteLine("     (Die Tasten [Enter], [Backspace], [←], [→], [↑] und [↓] sind zur Verfügung)");

                //Reset der Variablen
                int i = 0;
                FeldKorektur = "ambatakam";
                Zahl[0] = true;
                for (int j = 0; j <= 8; j++)
                {
                    Feld[j] = 0;
                    Zahl[j + 1] = true;
                    Feld2[j] = false;
                    if (j < 2)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            FalscheSpaltenReihen[j, k] = true; 
                        }
                    }
                }

                //Eingabe der Zahlen
                while (FeldKorektur != "\r")
                {
                    //Curos Position
                    if (FeldKorektur == "\b")
                    {
                        if (i > 8)
                        {
                            i--;
                            Console.SetCursorPosition(CursorPosition[0, i], CursorPosition[1, i]);
                            Console.Write(" ");
                        }
                        if (Feld[i] == 0 && i > 0)
                        {
                            i--;
                            Feld[i] = 0;
                            Console.SetCursorPosition(CursorPosition[0, i], CursorPosition[1, i]);
                            Console.Write(" ");
                        }
                        else
                        {
                            Feld[i] = 0;
                            Console.SetCursorPosition(CursorPosition[0, i], CursorPosition[1, i]);
                            Console.Write(" ");
                        }
                    }
                    if (i < 9)
                    {
                        Console.SetCursorPosition(CursorPosition[0, i], CursorPosition[1, i]);
                    }
                    do
                    {
                        if (i < 9)
                        {
                            Console.CursorVisible = true;
                        }
                        Key = Console.ReadKey(true);
                        FeldKorektur = Key.KeyChar.ToString();
                        if (FeldKorektur == "\r")
                        {//Enter
                            if (Feld[0] != 0 && Feld[1] != 0 && Feld[2] != 0 && Feld[3] != 0 && Feld[4] != 0 && Feld[5] != 0 && Feld[6] != 0 && Feld[7] != 0 && Feld[8] != 0)
                            {
                                break;
                            }
                        }
                        if (FeldKorektur == "\b")
                        {//Löschen
                            break;
                        }
                        //Pfeil nach oben
                        if (Key.Key == ConsoleKey.UpArrow && i > 2)
                        {
                            if (i > 8)
                            {
                                i--;
                            }
                            i -= 3;
                            //FeldKorektur = "ambasien";
                            break;
                        }
                        //Pfeil nach unten
                        if (Key.Key == ConsoleKey.DownArrow && i < 6)
                        {
                            i += 3;
                            //FeldKorektur = "ambasien";
                            break;
                        }
                        //Pfeil nach links
                        if (Key.Key == ConsoleKey.LeftArrow && i > 0)
                        {
                            i--;
                            //FeldKorektur = "ambasien";
                            break;
                        }
                        //Pfeil nach rechts
                        if (Key.Key == ConsoleKey.RightArrow && i < 8)
                        {
                            i++;
                            //FeldKorektur = "ambasien";
                            break;
                        }
                        if (i < 9 && Key.Key != ConsoleKey.RightArrow && Key.Key != ConsoleKey.LeftArrow && Key.Key != ConsoleKey.UpArrow && Key.Key != ConsoleKey.DownArrow)
                        {//Ausgabe
                            int.TryParse(FeldKorektur, out Feld[i]);
                            if (i == 8)
                            {
                                Console.CursorVisible = false;
                            }
                            if (Feld[i] != 0)
                            {
                                Console.Write(Feld[i]);
                                i++;
                                break;
                            }
                        }
                    } while (true);
                }
                Console.CursorVisible = false;

                //--------------------------------------------------AUSRECHNUNG

                //----------------------------------------FELD
                GamingRegel = true;
                for (i = 0; i <= 8; i++)
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        if (i != j)
                        {
                            if (Feld[i] == Feld[j])
                            {
                                //Speicherung der falschen Zahlen
                                GamingRegel = false;
                                Zahl[j] = false;
                            }
                        }
                    }
                }

                //----------------------------------------SPALTE
                //Eingabe von den alten Zahlen in einem neuen 3D Array
                Feld3D[0, 0] = Feld[0];
                Feld3D[0, 1] = Feld[3];
                Feld3D[0, 2] = Feld[6];
                Feld3D[1, 0] = Feld[1];
                Feld3D[1, 1] = Feld[4];
                Feld3D[1, 2] = Feld[7];
                Feld3D[2, 0] = Feld[2];
                Feld3D[2, 1] = Feld[5];
                Feld3D[2, 2] = Feld[8];
                for (i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j < 2)
                        {
                            //Spalten
                            if (Feld3D[i, j] == Feld3D[i, (j + 1)])
                            {
                                FalscheSpaltenReihen[0, i] = false;
                            }
                            //Reien
                            if (i < 2)
                            {
                                if (Feld3D[j, i] == Feld3D[(j + 1), i])
                                {
                                    FalscheSpaltenReihen[1, i] = false;
                                }
                            }
                            else
                            {
                                if (Feld3D[2, i] == Feld3D[0, i])
                                {
                                    FalscheSpaltenReihen[1, i] = false;
                                }
                            }
                        }
                        else
                        {
                            //Reien
                            if (i < 2)
                            {
                                if (Feld3D[j, i] == Feld3D[0, i])
                                {
                                    FalscheSpaltenReihen[1, i] = false;
                                }
                            }
                            else
                            {
                                if (Feld3D[2, i] == Feld3D[0, i])
                                {
                                    FalscheSpaltenReihen[1, i] = false;
                                }
                            }
                            //Spalten
                            if (Feld3D[i, 2] == Feld3D[i, 0])
                            {
                                FalscheSpaltenReihen[0, i] = false;
                            }
                        }
                    }
                }
                /*
                Console.WriteLine("\n\n\n" + Zahl[0] + " " + Zahl[1] + " " + Zahl[2] + " " + Zahl[3] + " " + Zahl[4] + " " + Zahl[5] + " " + Zahl[6] + " " + Zahl[7] + " " + Zahl[8]);
                Console.WriteLine("\n" + FalscheSpaltenReihen[0, 0] + " " + FalscheSpaltenReihen[0, 1] + " " + FalscheSpaltenReihen[0, 2] + "\n" + FalscheSpaltenReihen[1, 0] + " " + FalscheSpaltenReihen[1, 1] + " " + FalscheSpaltenReihen[1, 2] + "\n");
                */

                //----------------------------------------AUSGABE
                Console.Clear();
                if (GamingRegel == true)
                {//Alles Richtig
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("┌───┬───┬───┐");
                    Console.WriteLine("│ {0} │ {1} │ {2} │", Feld[0], Feld[1], Feld[2]);
                    Console.WriteLine("├───┼───┼───┤");
                    Console.WriteLine("│ {0} │ {1} │ {2} │", Feld[3], Feld[4], Feld[5]);
                    Console.WriteLine("├───┼───┼───┤");
                    Console.WriteLine("│ {0} │ {1} │ {2} │", Feld[6], Feld[7], Feld[8]);
                    Console.WriteLine("└───┴───┴───┘");
                    Console.WriteLine("\nGaming-Regel KORREKT!");
                }
                else
                {//Anzeige der Falschen Zahlen, Saplten und Reien
                 //1. Zeile (Spalte)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("┌─");
                    Nummer = 0;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┬─");
                    Nummer = 1;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┬─");
                    Nummer = 2;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.WriteLine("─┐");
                    //2. Zeile (Reihe)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (FalscheSpaltenReihen[1, 0] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.Write("│ ");
                    Reihe = 0;
                    Spalte = 0;
                    Nummer = 0;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.Write(" │ ");
                    Reihe = 0;
                    Spalte = 1;
                    Nummer = 1;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.Write(" │ ");
                    Reihe = 0;
                    Spalte = 2;
                    Nummer = 2;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.WriteLine(" │");
                    //3. Zeile (Spalte)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("├─");
                    Nummer = 0;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┼─");
                    Nummer = 1;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┼─");
                    Nummer = 2;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.WriteLine("─┤");
                    //4. Zeile (Reihe)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (FalscheSpaltenReihen[1, 1] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.Write("│ ");
                    Reihe = 1;
                    Spalte = 0;
                    Nummer = 3;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.Write(" │ ");
                    Reihe = 1;
                    Spalte = 1;
                    Nummer = 4;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.Write(" │ ");
                    Reihe = 1;
                    Spalte = 2;
                    Nummer = 5;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.WriteLine(" │");
                    //5. Zeile (Spalte)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("├─");
                    Nummer = 0;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┼─");
                    Nummer = 1;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┼─");
                    Nummer = 2;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.WriteLine("─┤");
                    //6. Zeile (Reihe)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (FalscheSpaltenReihen[1, 2] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.Write("│ ");
                    Reihe = 2;
                    Spalte = 0;
                    Nummer = 6;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.Write(" │ ");
                    Reihe = 2;
                    Spalte = 1;
                    Nummer = 7;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.Write(" │ ");
                    Reihe = 2;
                    Spalte = 2;
                    Nummer = 8;
                    ZahlenAnZeige(FalscheSpaltenReihen, Zahl, Feld, Reihe, Spalte, Nummer);
                    Console.WriteLine(" │");
                    //7. Zeile (Spalte)
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("└─");
                    Nummer = 0;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┴─");
                    Nummer = 1;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.Write("─┴─");
                    Nummer = 2;
                    SpaltenZeile(FalscheSpaltenReihen, Nummer);
                    Console.WriteLine("─┘");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("\nGaming-Regel VERLETZT!");
                }

                //----------------PROGRAMM WIEDERHOLEN----------------//
                Thread.Sleep(500);
                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Wollen Sie das Programm wiederholen?");
                Console.WriteLine("     ([J] = Ja  |  [N] = Nein)");
                do
                {
                    Console.SetCursorPosition(37, 10);
                    Wiederholen = Console.ReadKey(true).KeyChar.ToString();
                    Wiederholen = Wiederholen.ToLower();
                    if (Wiederholen != "j" && Wiederholen != "n")
                    {
                        Console.SetCursorPosition(37, 10);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(Wiederholen);
                        Thread.Sleep(200);
                        Console.SetCursorPosition(37, 10);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                } while (Wiederholen != "j" && Wiederholen != "n");
            } while (Wiederholen == "j");
        }
        static void ZahlenAnZeige(bool[,] FalscheSpaltenReihen, bool[] Zahl, int[] Feld, int Reihe, int Spalte, int Nummer)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (Zahl[Nummer] == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            if (FalscheSpaltenReihen[1, Reihe] == false)
            {
                if (FalscheSpaltenReihen[0, Spalte] == false)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    if (Zahl[Nummer] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    if (Zahl[Nummer] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
            }
            else
            {
                if (FalscheSpaltenReihen[0, Spalte] == false)
                {
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    if (Zahl[Nummer] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
            }
            Console.Write(Feld[Nummer]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (FalscheSpaltenReihen[1, Reihe] == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
        }
        static void SpaltenZeile(bool[,] FalscheSpaltenReihen, int Nummer)
        {
            if (FalscheSpaltenReihen[0, Nummer] == false)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
            Console.Write("─");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
