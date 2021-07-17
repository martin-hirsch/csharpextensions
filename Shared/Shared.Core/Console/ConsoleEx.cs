using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Core.Console
{
    public static class ConsoleEx
    {
        public static int PaddingLeft { get; set; }

        public static void CheckInternetConnectionWithScreen()
        {
            var connectionEstablished = false;

            while (!connectionEstablished)
                try
                {
                    using (var client = new WebClient())
                    {
                        using (var stream = client.OpenRead("http://www.google.com"))
                        {
                            connectionEstablished = true;
                        }
                    }
                }
                catch
                {
                    WriteLine("Cannot reach your WIFI  ...", ConsolePosition.Center);
                    connectionEstablished = false;
                }
        }

        /// <summary>
        ///     Scrolls current WindowHeight up.
        /// </summary>
        public static void ScrollUpTransition()
        {
            var lines = System.Console.WindowHeight;
            const int scrollSpeed = 10;

            for (var i = 0; i < lines; i++)
            {
                Thread.Sleep(scrollSpeed);
                System.Console.WriteLine();
            }

            System.Console.Clear();
        }

        public static Task ShowMenuAsync(string introduction, List<ConsoleMenuItem> consoleMenuItems, bool isRootMenu = false)
        {
            var _continue = true;
            while (_continue)
            {
                System.Console.Clear();
                System.Console.WriteLine(introduction);

                foreach (var consoleMenuItem in consoleMenuItems)
                {
                    System.Console.WriteLine(
                        $@"{consoleMenuItem.Label} ({string.Join(" | ", consoleMenuItem.PossibleArguments)})");
                }

                System.Console.WriteLine(isRootMenu ? "Exit Application (c)" : "Back (b)");

                var menu = System.Console.ReadLine();
                foreach (var consoleMenuItem in consoleMenuItems)
                {
                    _continue = WriteExitOrBackOption(isRootMenu, menu);

                    if (consoleMenuItem.PossibleArguments.Contains(menu))
                    {
                        consoleMenuItem.Action.Invoke();
                    }
                    else
                    {
                        System.Console.WriteLine("Wrong argument. Please try again.");
                    }
                }
            }

            return Task.CompletedTask;
        }

        public static void Wait(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        public static void WaitAndClear(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            System.Console.Clear();
        }

        public static void WriteLine(string line, ConsolePosition? consolePosition = null)
        {
            if (consolePosition != null)
            {
                WriteLineToConsolePosition(line, consolePosition);
            }
            else
            {
                System.Console.WriteLine(line.PadLeft(line.Length + PaddingLeft));
            }
        }

        public static void WriteWholeLine(char c)
        {
            var times = System.Console.WindowWidth;
            for (var i = 0; i < times; i++)
            {
                System.Console.Write(c);
            }
        }

        public static bool YesNoModal(string question)
        {
            var result = false;
            var userDidAnswer = false;
            System.Console.WriteLine($@"{question} ([Y]es, [N]o)");

            while (!userDidAnswer)
                switch (System.Console.ReadLine()?.ToLower())
                {
                    case "y":
                    case "yes":
                        userDidAnswer = true;
                        result = true;
                        break;
                    case "n":
                    case "no":
                        userDidAnswer = true;
                        result = false;
                        break;
                    default:
                        System.Console.WriteLine("Accepted Values are '[Y]es, [N]o'.");
                        break;
                }

            return result;
        }

        private static void ReadKeys()
        {
            var key = new ConsoleKeyInfo();

            while (!System.Console.KeyAvailable && key.Key != ConsoleKey.Escape)
            {
                key = System.Console.ReadKey(true);

                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        System.Console.WriteLine("UpArrow was pressed");
                        break;
                    case ConsoleKey.DownArrow:
                        System.Console.WriteLine("DownArrow was pressed");
                        break;

                    case ConsoleKey.RightArrow:
                        System.Console.WriteLine("RightArrow was pressed");
                        break;

                    case ConsoleKey.LeftArrow:
                        System.Console.WriteLine("LeftArrow was pressed");
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        if (System.Console.CapsLock && System.Console.NumberLock)
                        {
                            System.Console.WriteLine(key.KeyChar);
                        }

                        break;
                }
            }
        }

        private static bool WriteExitOrBackOption(bool isRootMenu, string menu)
        {
            if (isRootMenu)
            {
                if (menu == "c" || menu == "close")
                {
                    return false;
                }
            }
            else
            {
                if (menu == "b" || menu == "back")
                {
                    return false;
                }
            }

            return true;
        }

        private static void WriteLineToCenter(string line)
        {
            for (var h = 0; h < System.Console.WindowHeight; h++)
            {
                if (h == System.Console.WindowHeight / 2)
                {
                    System.Console.WriteLine(line.PadLeft(System.Console.WindowWidth / 2 + line.Length / 2));
                }
                else
                {
                    System.Console.WriteLine();
                }
            }
        }

        private static void WriteLineToConsolePosition(string line, ConsolePosition? consolePosition)
        {
            switch (consolePosition)
            {
                case ConsolePosition.Center:
                    WriteLineToCenter(line);
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(consolePosition), consolePosition, null);
            }
        }
    }

    internal class ConsoleBusyIndicator
    {
        private int _currentBusySymbol;

        public char[] BusySymbols { get; set; }

        public ConsoleBusyIndicator()
        {
            BusySymbols = new[] { '|', '/', '-', '\\' };
        }

        public void UpdateProgress()
        {
            while (true)
            {
                Thread.Sleep(100);
                var originalX = System.Console.CursorLeft;
                var originalY = System.Console.CursorTop;

                System.Console.Write(BusySymbols[_currentBusySymbol]);

                _currentBusySymbol++;

                if (_currentBusySymbol == BusySymbols.Length)
                {
                    _currentBusySymbol = 0;
                }

                System.Console.SetCursorPosition(originalX, originalY);
            }
        }
    }

    public enum ConsolePosition
    {
        Center
    }

    public class ConsoleMenuItem
    {
        public Action Action { get; }

        public string Label { get; }

        public List<string> PossibleArguments { get; }

        public ConsoleMenuItem(string label, List<string> possibleArguments, Action action)
        {
            Label = label;
            PossibleArguments = possibleArguments;
            Action = action;
        }
    }
}