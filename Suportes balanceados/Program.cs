using System;
using System.Collections.Generic;

namespace Suportes_balanceados
{
    class Program
    {
        ///Você pode alterar os caracteres para qual preferir, seguindo a regra de permanecer o caractere de fechamento e abertura um em sequência do outro.
        ///EXEMPLO: (){}""''[]==
        static string colchetes = "(){}[]";
        static void Main(string[] args)
        {
            Console.Title = "Suporte Balanceado - Kevin Fávaro 2023";

            //Utilizado uma Func apenas para demonstrar como funciona, além de ser algo utilizado esclusivamente nessa parte do código.
            Func<string, string, string> colchetesToString = (texto, separador) =>
            {
                string final = "";
                char[] chars = colchetes.ToCharArray();
                for (int i = 0; i < chars.Length; i += 2)
                    final += colchetes[i].ToString() + colchetes[i + 1].ToString() + separador;
                return final.Length > 0 ? final.Substring(0, final.Length - separador.Length) : "";
            };

            WriteLineCenter("Olá! Bem vindo ao \"Suporte Balanceado\" - Kevin Fávaro 2023\n");
            WriteLineCenter("Digite um texto (ou cole) para verificar se os caracteres a seguir estão balanceados");
            WriteLineCenter(colchetesToString(colchetes, " e "));

            Console.WriteLine(@$"
- Tem suporte para colar o texto.
- Tem suporte para multiplas linhas.
- Pressione Enter + ESC para concluir a digitação.
");
            WriteLineSeparator('-');
            string digitado = String.Empty;

            do
            {
                string read = Console.ReadLine();
                digitado += read + "\n";

                ConsoleKeyInfo key;
                do
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(2, Console.CursorTop);
                    Console.Write("<Pressione ESC para concluir>");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, Console.CursorTop);

                    key = Console.ReadKey();
                    ClearCurrentConsoleLine();

                    if (key.Key == ConsoleKey.Escape)
                        break;
                    else
                    {
                        Console.Write(key.Key == ConsoleKey.Enter ? "\n" : key.KeyChar.ToString());
                        digitado += key.Key == ConsoleKey.Enter ? "\n" : key.KeyChar.ToString();
                    }
                } while (key.Key == ConsoleKey.Enter);
                if (key.Key == ConsoleKey.Escape)
                    break;
            } while (true);

            Console.Clear();

            if (digitado.Trim() == String.Empty)
            {
                Console.WriteLine("_Não foi identificado nenhum texto digitado.");
            }
            else
            {
                Console.WriteLine($"_Você digitou: \n{digitado}\n");

                WriteLineSeparator('-');

                bool balanceado = isBalanceado(digitado, colchetes);
                Console.Write("O texto digitado está ");

                Console.ForegroundColor = balanceado ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(balanceado ? "Balanceado" : "Desbalanceado");
                Console.ResetColor();

                if (!balanceado)
                    Console.Beep();
            }

            Console.Write("\n");
            WriteLineSeparator('-');

            Console.WriteLine("\nPressione qualquer tecla para tentar novamente, ou aperte ESC para sair da aplicação");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key == ConsoleKey.Escape)
                Environment.Exit(0);
            Console.Clear();
            Main(args);
        }

        public static void WriteLineCenter(string texto)
        {
            Console.SetCursorPosition((Console.WindowWidth - texto.Length) / 2, Console.CursorTop);
            Console.WriteLine(texto);
        }

        public static void WriteLineSeparator(char caracter)
        {
            Console.WriteLine(new string(caracter, Console.WindowWidth));
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static bool isBalanceado(string texto, string balanceadores)
        {
            List<char> lista_colchetes = new List<char>();
            foreach (char t in texto)
            {
                int index = balanceadores.IndexOf(t);
                if (index < 0) continue;

                if (index % 2 == 0)
                    lista_colchetes.Add(t);
                else
                {
                    int count = lista_colchetes.Count;
                    if (count > 0 && lista_colchetes[count - 1] == balanceadores[index - 1])
                        lista_colchetes.RemoveAt(count - 1);
                    else
                        return false;
                }
            }
            return lista_colchetes.Count <= 0;
        }
    }
}
