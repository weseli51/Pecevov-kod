using System;
using System.Collections.Generic;
using System.Text;

namespace pp_menu_system
{
    class Program
    {
        static char gornji_levi_ugao = '\u2554';
        static char gornji_desni_ugao = '\u2557';
        static char uspravna_linija = '\u2551';
        static char horizontalna_linija = '\u2550';
        static char donji_levi_ugao = '\u255A';
        static char donji_desni_ugao = '\u255D';

        static int menu_item_selected = 1;
        static Dictionary<int, string> stavke_menija = new Dictionary<int, string>();

        static void ispisi_menu_item(string text, int x, int y, bool selektovana)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            StringBuilder gornja_linija = new StringBuilder();
            StringBuilder donja_linija = new StringBuilder();
            StringBuilder tekst_linija = new StringBuilder();

            int duzina_menija = text.Length + 2;

            gornja_linija.Append(gornji_levi_ugao);
            for (int i = 0; i <= duzina_menija - 1; i++) gornja_linija.Append(horizontalna_linija);
            gornja_linija.Append(gornji_desni_ugao);

            donja_linija.Append(donji_levi_ugao);
            for (int i = 0; i <= duzina_menija - 1; i++) donja_linija.Append(horizontalna_linija);
            donja_linija.Append(donji_desni_ugao);

            tekst_linija.Append(uspravna_linija);
            tekst_linija.Append(" " + text + " ");
            tekst_linija.Append(uspravna_linija);

            if (selektovana) Console.BackgroundColor = ConsoleColor.Blue;

            Console.SetCursorPosition(x, y);
            Console.Write(gornja_linija);
            Console.SetCursorPosition(x, y + 1);
            Console.Write(tekst_linija);
            Console.SetCursorPosition(x, y + 2);
            Console.Write(donja_linija);
        }

        static void draw_menu(Dictionary<int, string> menu, int x, int y)
        {
            Console.SetCursorPosition(x, y);

            int offset_x = Console.CursorLeft;
            int draw_y = Console.CursorTop;

            for (int i = 1; i <= menu.Count; i++)
            {
                ispisi_menu_item(menu[i], offset_x, draw_y, i == menu_item_selected);
                offset_x += menu[i].Length + 4;
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "NTP";
            Console.SetWindowSize(150, 40);

            stavke_menija.Add(1, "Keyboard"); // 0
            stavke_menija.Add(2, "Mouse");    // 1
            stavke_menija.Add(3, "Sound");
            stavke_menija.Add(4, "Display");
            stavke_menija.Add(5, "Exit");

            Console.CursorVisible = false;

            Console.OutputEncoding = Encoding.UTF8;

            draw_menu(stavke_menija, 10, 10);

            while (true)
            {
                var c = Console.ReadKey().Key;

                switch (c)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (menu_item_selected == 1) menu_item_selected = 5;
                            else menu_item_selected--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (menu_item_selected == 5) menu_item_selected = 1;
                            else menu_item_selected++;
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            switch (menu_item_selected)
                            {
                                case 1:
                                    {
                                        System.Diagnostics.Process.Start("main.cpl", "keyboard");
                                        break;
                                    }
                                case 2:
                                    {
                                        System.Diagnostics.Process.Start("main.cpl");
                                        break;
                                    }
                                case 3:
                                    {
                                        System.Diagnostics.Process.Start("mmsys.cpl");
                                        break;
                                    }
                                case 4:
                                    {
                                        System.Diagnostics.Process.Start("desk.cpl");
                                        break;
                                    }
                                case 5:
                                    {
                                        Environment.Exit(0);
                                        break;
                                    }
                            }
                            break;
                        }
                }

                draw_menu(stavke_menija, 10, 10);
            }
        }
    }
}
