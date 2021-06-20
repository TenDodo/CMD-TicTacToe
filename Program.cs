using System;
namespace TicTacToe
{
    class Program
    {
        static char[,] tictactoe = new char[3, 3];
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in TicTacToe");
            Console.WriteLine("Select game mode");
            Console.WriteLine("1 - 2 players game | 2 - game with computer (impossible) | other - exit");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    startGame();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    startAIgame();
                    break;
                default:
                    exit();
                    break;
            }
            startGame();            
        }
        static void resetGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tictactoe[i, j] = ' ';
                }
            }
        }
        static void draw()
        {
            Console.WriteLine("   1     2     3\n" + "A  " + tictactoe[0, 0] + "  |  " + tictactoe[0, 1] + "  |  " + tictactoe[0, 2] + "\n  ----------------\nB  " + tictactoe[1, 0] + "  |  " + tictactoe[1, 1] + "  |  " + tictactoe[1, 2] + "\n  ----------------\nC  " + tictactoe[2, 0] + "  |  " + tictactoe[2, 1] + "  |  " + tictactoe[2, 2] + "\n");
        }

        static string[] availableMoves = new string[] { "a1", "a2", "a3", "b1", "b2", "b3", "c1", "c2", "c3" };
        static void startGame()
        {
            bool o = true;
            resetGame();
            while (true)
            {
                wrongMove:
                Console.Clear();
                draw();
                int che = check(tictactoe);
                if (che == 1 || che == 2)
                {
                    Console.WriteLine("Player " + che + " won");
                    playAgain(1);
                    break;
                }
                else if (che == 3)
                {
                    Console.WriteLine("tie");
                    playAgain(1);
                    break;
                }
                string a = Console.ReadLine();
                bool isOK = false;
                foreach (string s in availableMoves)
                {
                    if(a.ToLower() == s)
                    {
                        isOK = true;
                    }
                }
                if (!isOK)
                {
                    goto wrongMove;
                }
                int i1 = 0;
                switch (a[0].ToString().ToLower())
                {
                    case "a":
                        i1 = 0;
                        break;
                    case "b":
                        i1 = 1;
                        break;
                    case "c":
                        i1 = 2;
                        break;
                }
                if (tictactoe[i1, int.Parse(a[1].ToString()) - 1] == ' ')
                {
                    tictactoe[i1, int.Parse(a[1].ToString()) - 1] = o ? 'O' : 'X';
                    o = !o;
                }
            }
        }

        static void startAIgame()
        {
            bool playerTurn = true;
            resetGame();
            while (true)
            {
                wrongMove:
                if (playerTurn)
                {
                    Console.Clear();
                    draw();
                    int che = check(tictactoe);
                    switch (che)
                    {
                        case 1:
                            Console.WriteLine("Player won");
                            playAgain(2);
                            break;
                        case 2:
                            Console.WriteLine("AI won");
                            playAgain(2);
                            break;
                        case 3:
                            Console.WriteLine("tie");
                            playAgain(2);
                            break;
                        default:
                            break;
                    }
                    string a = Console.ReadLine();
                    bool isOK = false;
                    foreach (string s in availableMoves)
                    {
                        if (a.ToLower() == s)
                        {
                            isOK = true;
                        }
                    }
                    if (!isOK)
                    {
                        goto wrongMove;
                    }
                    int i1 = 0;
                    switch (a[0].ToString().ToLower())
                    {
                        case "a":
                            i1 = 0;
                            break;
                        case "b":
                            i1 = 1;
                            break;
                        case "c":
                            i1 = 2;
                            break;
                    }
                    if (tictactoe[i1, int.Parse(a[1].ToString()) - 1] == ' ')
                    {
                        tictactoe[i1, int.Parse(a[1].ToString()) - 1] = 'O';
                        playerTurn = !playerTurn;
                    }
                }
                else
                {
                    int che = check(tictactoe);
                    switch (che)
                    {
                        case 1:
                            Console.Clear();
                            draw();
                            Console.WriteLine("Player won");
                            playAgain(2);
                            break;
                            Console.Clear();
                            draw();
                        case 2:
                            Console.WriteLine("AI won");
                            playAgain(2);
                            break;
                        case 3:
                            Console.Clear();
                            draw();
                            Console.WriteLine("tie");
                            playAgain(2);
                            break;
                        default:
                            break;
                    }
                    int[] AImove = AIcheck(tictactoe);
                    tictactoe[AImove[0], AImove[1]] = 'X';
                    playerTurn = !playerTurn;
                }

            }
        }

        static int[] AIcheck(char[,] array)
        {
            string line1 = array[0, 0].ToString() + array[0, 1].ToString() + array[0, 2].ToString();
            string line2 = array[1, 0].ToString() + array[1, 1].ToString() + array[1, 2].ToString();
            string line3 = array[2, 0].ToString() + array[2, 1].ToString() + array[2, 2].ToString();
            string column1 = array[0, 0].ToString() + array[1, 0].ToString() + array[2, 0].ToString();
            string column2 = array[0, 1].ToString() + array[1, 1].ToString() + array[2, 1].ToString();
            string column3 = array[0, 2].ToString() + array[1, 2].ToString() + array[2, 2].ToString();
            string slant1 = array[0, 0].ToString() + array[1, 1].ToString() + array[2, 2].ToString();
            string slant2 = array[2, 0].ToString() + array[1, 1].ToString() + array[0, 2].ToString();
            string[] checker = new string[] { line1, line2, line3, column1, column2, column3, slant1, slant2 };
            int i = 0;
            int selected = -1;
            foreach (string check in checker)
            {
                if (check.Replace("O", "").Replace(" ", "").Length == 2 && !check.Contains("O"))
                {
                    selected = i;
                    break;
                }
                i++;
            }
            switch (selected)
            {
                case 0:
                    return new int[] { 0, checker[0].IndexOf(' ') };
                case 1:
                    return new int[] { 1, checker[1].IndexOf(' ') };
                case 2:
                    return new int[] { 2, checker[2].IndexOf(' ') };
                case 3:
                    return new int[] { checker[3].IndexOf(' '), 0 };
                case 4:
                    return new int[] { checker[4].IndexOf(' '), 1 };
                case 5:
                    return new int[] { checker[5].IndexOf(' '), 2 };
                case 6:
                    return new int[] { checker[6].IndexOf(' '), checker[6].IndexOf(' ') };
                case 7:
                    if (checker[7].IndexOf(' ') == 0)
                    {
                        return new int[] { 2, 0 };
                    }
                    else if (checker[7].IndexOf(' ') == 1)
                    {
                        return new int[] { 1, 1 };
                    }
                    else
                    {
                        return new int[] { 0, 2 };
                    }
                default:
                    i = 0;
                    foreach (string check in checker)
                    {
                        if (check.Replace("X", "").Replace(" ", "").Length == 2 && !check.Contains("X"))
                        {
                            selected = i;
                            break;
                        }
                        i++;
                    }
                    switch (selected)
                    {
                        case 0:
                            return new int[] { 0, checker[0].IndexOf(' ') };
                        case 1:
                            return new int[] { 1, checker[1].IndexOf(' ') };
                        case 2:
                            return new int[] { 2, checker[2].IndexOf(' ') };
                        case 3:
                            return new int[] { checker[3].IndexOf(' '), 0 };
                        case 4:
                            return new int[] { checker[4].IndexOf(' '), 1 };
                        case 5:
                            return new int[] { checker[5].IndexOf(' '), 2 };
                        case 6:
                            return new int[] { checker[6].IndexOf(' '), checker[6].IndexOf(' ') };
                        case 7:
                            if (checker[7].IndexOf(' ') == 0)
                            {
                                return new int[] { 2, 0 };
                            }
                            else if (checker[7].IndexOf(' ') == 1)
                            {
                                return new int[] { 1, 1 };
                            }
                            else
                            {
                                return new int[] { 0, 2 };
                            }
                        default:
                            foreach (string check in checker)
                            {
                                if (check.Contains(" ") && !check.Contains("O") && check.Contains("X"))
                                {
                                    selected = i;
                                    break;
                                }
                                i++;
                            }
                            switch (selected)
                            {
                                case 0:
                                    return new int[] { 0, checker[0].IndexOf(' ') };
                                case 1:
                                    return new int[] { 1, checker[1].IndexOf(' ') };
                                case 2:
                                    return new int[] { 2, checker[2].IndexOf(' ') };
                                case 3:
                                    return new int[] { checker[3].IndexOf(' '), 0 };
                                case 4:
                                    return new int[] { checker[4].IndexOf(' '), 1 };
                                case 5:
                                    return new int[] { checker[5].IndexOf(' '), 2 };
                                case 6:
                                    return new int[] { checker[6].IndexOf(' '), checker[6].IndexOf(' ') };
                                case 7:
                                    if (checker[7].IndexOf(' ') == 0)
                                    {
                                        return new int[] { 2, 0 };
                                    }
                                    else if (checker[7].IndexOf(' ') == 1)
                                    {
                                        return new int[] { 1, 1 };
                                    }
                                    else
                                    {
                                        return new int[] { 0, 2 };
                                    }
                                default:
                                    int[][] finalChoice1 = new int[][] { new int[] { 1, 1 }, new int[] { 0, 0 }, new int[] { 0, 2 }, new int[] { 2, 2 }, new int[] { 2, 0 } };
                                    if (slant1.Replace(" ", "").Length == 2 && array[1, 1] != 'X')
                                    {
                                        finalChoice1 = new int[][] { new int[] { 0, 2 }, new int[] { 2, 0 } };
                                    }
                                    else if (slant2.Replace(" ", "").Length == 2 && array[1, 1] != 'X')
                                    {
                                        finalChoice1 = new int[][] { new int[] { 0, 0 }, new int[] { 2, 2 } };
                                    }
                                    else if (array[1, 1] == 'X' && (!slant1.Contains(" ") || !slant2.Contains(" ")))
                                    {
                                        int[][] tempChoice1 = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { 1, 2 }, new int[] { 2, 1 } };
                                        finalChoice1 = tempChoice1;
                                        
                                    }

                                    foreach (int[] fC1 in finalChoice1)
                                    {
                                        if (array[fC1[0], fC1[1]] == ' ')
                                        {
                                            return fC1;
                                        }
                                    }
                                    int[][] finalChoice2 = new int[][] { new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, 2 }, new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 } };
                                    foreach (int[] fC2 in finalChoice2)
                                    {
                                        if (array[fC2[0], fC2[1]] == ' ')
                                        {
                                            return fC2;
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
            }
            return new int[] { 1, 1 };
        }
        static int check(char[,] array)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((array[i, 0] == 'O' && array[i, 1] == 'O' && array[i, 2] == 'O') || (array[0, i] == 'O' && array[1, i] == 'O' && array[2, i] == 'O') || (array[0, 0] == 'O' && array[1, 1] == 'O' && array[2, 2] == 'O') || (array[2, 0] == 'O' && array[1, 1] == 'O' && array[0, 2] == 'O'))
                {
                    return 1;
                }
                else if ((array[i, 0] == 'X' && array[i, 1] == 'X' && array[i, 2] == 'X') || (array[0, i] == 'X' && array[1, i] == 'X' && array[2, i] == 'X') || (array[0, 0] == 'X' && array[1, 1] == 'X' && array[2, 2] == 'X') || (array[2, 0] == 'X' && array[1, 1] == 'X' && array[0, 2] == 'X'))
                {
                    return 2;
                }
                else if (array[0, 0] != ' ' && array[0, 1] != ' ' && array[0, 2] != ' ' && array[1, 0] != ' ' && array[1, 1] != ' ' && array[1, 2] != ' ' && array[2, 0] != ' ' && array[2, 1] != ' ' && array[2, 2] != ' ')
                {
                    return 3;
                }
            }
            return 0;
        }



        static void playAgain(int a)
        {
            Console.WriteLine("Play again?");
            Console.WriteLine("y - yes | n - no | m - menu");
            if (a == 1)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        Console.Clear();
                        startGame();
                        break;
                    case ConsoleKey.M:
                        Console.Clear();
                        Main(null);
                        break;
                    default:
                        exit();
                        break;
                }
            }
            else
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Y:
                        Console.Clear();
                        startAIgame();
                        break;
                    case ConsoleKey.M:
                        Console.Clear();
                        Main(null);
                        break;
                    default:
                        exit();
                        break;
                }
            }
            
        }

        static void exit()
        {
            Console.WriteLine("\nTicTacToe by Arkadiusz Kozłowski\npress any key to exit");
            Console.ReadKey();
        }
    }
}
