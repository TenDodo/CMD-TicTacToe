using System;
namespace TicTacToe
{
    class Program
    {
        static char[,] tictactoe = new char[3, 3];
        static void Main(string[] args)
        {
            startGame();
            Console.ReadKey();
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
        static void startGame()
        {
            bool o = true;
            resetGame();
            while (true)
            {
                Console.Clear();
                draw();
                int che = check(tictactoe);
                if (che == 1 || che == 2)
                {
                    Console.WriteLine("Player " + che + " won");
                    break;
                }
                else if (che == 3)
                {
                    Console.WriteLine("tie");
                    break;
                }
                string a = Console.ReadLine();
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
    }
}
