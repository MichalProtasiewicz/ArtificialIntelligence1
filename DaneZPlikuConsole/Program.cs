using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaneZPlikuConsole
{
    class Program
    {
        static string TablicaDoString<T>(T[][] tab)
        {
            string wynik = "";
            for (int i = 0; i < tab.Length; i++)
            {
                for (int j = 0; j < tab[i].Length; j++)
                {
                    wynik += tab[i][j].ToString() + " ";
                }
                wynik = wynik.Trim() + Environment.NewLine;
            }

            return wynik;
        }

        static double StringToDouble(string liczba)
        {
            double wynik; liczba = liczba.Trim();
            if (!double.TryParse(liczba.Replace(',', '.'), out wynik) && !double.TryParse(liczba.Replace('.', ','), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do double");

            return wynik;
        }


        static int StringToInt(string liczba)
        {
            int wynik;
            if (!int.TryParse(liczba.Trim(), out wynik))
                throw new Exception("Nie udało się skonwertować liczby do int");

            return wynik;
        }

        static string[][] StringToTablica(string sciezkaDoPliku)
        {
            string trescPliku = System.IO.File.ReadAllText(sciezkaDoPliku); // wczytujemy treść pliku do zmiennej
            string[] wiersze = trescPliku.Trim().Split(new char[] { '\n' }); // treść pliku dzielimy wg znaku końca linii, dzięki czemu otrzymamy każdy wiersz w oddzielnej komórce tablicy
            string[][] wczytaneDane = new string[wiersze.Length][];   // Tworzymy zmienną, która będzie przechowywała wczytane dane. Tablica będzie miała tyle wierszy ile wierszy było z wczytanego poliku

            for (int i = 0; i < wiersze.Length; i++)
            {
                string wiersz = wiersze[i].Trim();     // przypisuję i-ty element tablicy do zmiennej wiersz
                string[] cyfry = wiersz.Split(new char[] { ' ' });   // dzielimy wiersz po znaku spacji, dzięki czemu otrzymamy tablicę cyfry, w której każda oddzielna komórka to czyfra z wiersza
                wczytaneDane[i] = new string[cyfry.Length];    // Do tablicy w której będą dane finalne dokładamy wiersz w postaci tablicy integerów tak długą jak długa jest tablica cyfry, czyli tyle ile było cyfr w jednym wierszu
                for (int j = 0; j < cyfry.Length; j++)
                {
                    string cyfra = cyfry[j].Trim(); // przypisuję j-tą cyfrę do zmiennej cyfra
                    wczytaneDane[i][j] = cyfra;
                }
            }
            return wczytaneDane;
        }

        static void Main(string[] args)
        {
            string nazwaPlikuZDanymi = @"australian.txt";
            string nazwaPlikuZTypamiAtrybutow = @"australian-type.txt";

            string[][] wczytaneDane = StringToTablica(nazwaPlikuZDanymi);
            string[][] atrType = StringToTablica(nazwaPlikuZTypamiAtrybutow);

            Console.WriteLine("Dane systemu");
            string wynik = TablicaDoString(wczytaneDane);
            Console.Write(wynik);

            Console.WriteLine("");
            Console.WriteLine("Dane pliku z typami");

            string wynikAtrType = TablicaDoString(atrType);
            Console.Write(wynikAtrType);

            /****************** Miejsce na rozwiązanie *********************************/

            int x = wczytaneDane[0].GetLength(0);

            List<string> typ = new List<string>();
            List<int> ile = new List<int>();

            for (int i = 0; i < wczytaneDane.GetLength(0); i++)
            {
                if (typ.Count == 0)
                {
                    typ.Add(wczytaneDane[i][x - 1]);
                    ile.Add(1);
                }
                else
                {
                    int y = 0;
                    for (int j = 0; j < typ.Count; j++)
                    {
                        if (wczytaneDane[i][x - 1] == typ[j])
                        {
                            ile[j]++;
                        }
                        else
                        {
                            y++;
                        }
                    }
                    if (y == typ.Count)
                    {
                        typ.Add(wczytaneDane[i][x - 1]);
                        ile.Add(1);
                    }
                }
            }
            for (int i = 0; i < typ.Count; i++)
            {
                Console.WriteLine(typ[i] + " wystepuje " + ile[i] + " razy.");
            }



            ///////////////////////////

            int dl = atrType.GetLength(0);
            int dl2 = atrType[0].GetLength(0);
            float max = 0;
            float min = 9999999;
            float srednia = 0;

            for (int i = 0; i < dl; i++)
            {
                if (atrType[i][dl2 - 1] == "n")
                {
                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        srednia += float.Parse(wczytaneDane[j][i].Replace(".", ",")) / wczytaneDane.GetLength(0);

                        float tmp = float.Parse(wczytaneDane[j][i].Replace(".", ","));

                        if (tmp > max)
                            max = tmp;

                        if (tmp < min)
                            min = tmp;
                    }
                    Console.WriteLine("a" + (i + 1));
                    Console.WriteLine(" MAX: " + max + " MIN: " + min);

                    max = 0;
                    min = 9999999;
                    srednia = 0;

                }
            }



            ////////////////////////////

            List<string> rozne = new List<string>();
            List<int> ilosc = new List<int>();
            for (int i = 0; i < dl; i++)
            {
                for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                {
                    if (rozne.Count == 0)
                    {
                        rozne.Add(wczytaneDane[j][i]);
                        ilosc.Add(1);
                    }
                    else
                    {
                        int tmp = 0;
                        for (int z = 0; z < rozne.Count; z++)
                        {
                            if (wczytaneDane[j][i] == rozne[z])
                            {
                                ilosc[z]++;
                            }
                            else
                            {
                                tmp++;
                            }
                        }
                        if (tmp == rozne.Count)
                        {
                            rozne.Add(wczytaneDane[j][i]);
                            ilosc.Add(1);
                        }
                    }

                }
                Console.WriteLine("a" + (i + 1) + " Ilosc = " + rozne.Count + " Rozne wartosci: ");
                for (int j = 0; j < rozne.Count; j++)
                {
                    Console.Write(rozne[j] + " ");
                }
                Console.WriteLine();
                rozne.Clear();
            }


            //////////////////////////////

            Console.WriteLine("ODCHYLENIE STANDARDOWE dla wszystkich atrybutow numerycznych:");

            for (int i = 0; i < dl; i++)
            {
                if (atrType[i][dl2 - 1] == "n")
                {
                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        srednia += float.Parse(wczytaneDane[j][i].Replace(".", ","));
                    }
                    srednia = srednia / wczytaneDane.GetLength(0);
                    double odchylenie = 0;

                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        float value = float.Parse(wczytaneDane[j][i].Replace(".", ","));
                        odchylenie += Math.Pow((value - srednia), 2);
                    }
                    odchylenie = odchylenie / wczytaneDane.GetLength(0);
                    odchylenie = Math.Sqrt(odchylenie);

                    Console.WriteLine("a" + (i + 1) + " = " + odchylenie);
                    srednia = 0;
                    odchylenie = 0;
                }
            }

            Console.WriteLine("ODCHYLENIE STANDARDOWE dla poszczegolnych atrybutow w klasach decyzyjnych:");
            Console.WriteLine("System decyzyjny 0: ");

            int dll2 = wczytaneDane[0].GetLength(0);

            float srednia0 = 0, srednia1 = 0;
            double odchylenie0 = 0, odchylenie1 = 0;

            int ile0 = 0;
            int ile1 = 0;

            for (int i = 0; i < dl; i++)
            {
                if (atrType[i][dl2 - 1] == "n")
                {
                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        if (wczytaneDane[j][dll2 - 1] == "0")
                        {
                            srednia0 += float.Parse(wczytaneDane[j][i].Replace(".", ","));
                            ile0++;
                        }
                    }
                    srednia0 = srednia0 / ile0;

                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        if (wczytaneDane[j][dll2 - 1] == "0")
                        {
                            float value = float.Parse(wczytaneDane[j][i].Replace(".", ","));
                            odchylenie0 += Math.Pow((value - srednia0), 2);
                        }
                    }
                    odchylenie0 = odchylenie0 / ile0;
                    odchylenie0 = Math.Sqrt(odchylenie0);

                    Console.WriteLine("a" + (i + 1) + " = " + odchylenie0);
                    srednia0 = 0;
                    odchylenie0 = 0;
                    ile0 = 0;
                }
            }

            Console.WriteLine("System decyzyjny 1: ");

            for (int i = 0; i < dl; i++)
            {
                if (atrType[i][dl2 - 1] == "n")
                {
                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        if (wczytaneDane[j][dll2 - 1] == "1")
                        {
                            srednia1 += float.Parse(wczytaneDane[j][i].Replace(".", ","));
                            ile1++;
                        }
                    }
                    srednia1 = srednia1 / ile1;

                    for (int j = 0; j < wczytaneDane.GetLength(0); j++)
                    {
                        if (wczytaneDane[j][dll2 - 1] == "1")
                        {
                            float value = float.Parse(wczytaneDane[j][i].Replace(".", ","));
                            odchylenie1 += Math.Pow((value - srednia1), 2);
                        }
                    }
                    odchylenie1 = odchylenie1 / ile1;
                    odchylenie1 = Math.Sqrt(odchylenie1);

                    Console.WriteLine("a" + (i + 1) + " = " + odchylenie1);
                    srednia1 = 0;
                    odchylenie1 = 0;
                    ile1 = 0;
                }
            }

            /****************** Koniec miejsca na rozwiązanie ********************************/
            Console.ReadKey();
        }
    }
}