using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        // Create a 3x3 empty board
        char[,] board = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        List<Player> players = new List<Player>();

        // Get player details
        for (int i = 0; i < 2; i++)
        {
            Console.Write($"Player {i + 1}, enter your name: ");
            string name = Console.ReadLine();
            name = char.ToUpper(name[0]) + name.Substring(1).ToLower();

            Console.Write($"{name}, enter a character to use: ");
            char character = Console.ReadKey().KeyChar;
            Console.WriteLine(); 

            players.Add(new Player { Name = name, Character = character });

            Console.WriteLine($"Player {i + 1} ({name}) added to the game.\n");
        }

        // Start game loop
        int currentPlayerIndex = 0;
        bool endOfGame = false;

        while (true && endOfGame != true)
        {
            PrintBoard(board);
            char currentCharacter = players[currentPlayerIndex].Character;

            // Handle player input
            bool validMove = false;
            int x = 0, y = 0;

            while (!validMove)
            {
                Console.Write($"Player {currentPlayerIndex + 1} ({players[currentPlayerIndex].Name}), enter Y coordinate (1-3): ");
                if (!int.TryParse(Console.ReadLine(), out x) || x < 1 || x > 3)
                {
                    Console.WriteLine("Invalid X coordinate. Enter a number between 1 and 3.");
                    continue;
                }

                Console.Write($"Player {currentPlayerIndex + 1} ({players[currentPlayerIndex].Name}), enter X coordinate (1-3): ");
                if (!int.TryParse(Console.ReadLine(), out y) || y < 1 || y > 3)
                {
                    Console.WriteLine("Invalid Y coordinate. Enter a number between 1 and 3.");
                    continue;
                }

                if (IsValidCoordinate(board, x - 1, y - 1))
                {
                    board[x - 1, y - 1] = currentCharacter;
                    validMove = true;
                }
                else
                {
                    Console.WriteLine("Invalid move! That spot is already taken. Try again.");
                }
            }

            // Check if the player has won
            if (HasWon(board, currentCharacter))
            {
                PrintBoard(board);
                Console.WriteLine($"\n###### Player {currentPlayerIndex + 1} ({players[currentPlayerIndex].Name}) wins the game! ######");
                break;
            }

            if (DrawCheck(board) == true)
            {
                Console.WriteLine("\v=============Draw=============\v");
                endOfGame = true;
            }
                

            // Switch turns
            currentPlayerIndex = (currentPlayerIndex + 1) % 2;
        }
    }

    static bool DrawCheck(char[,] board)
    {

        if (board[0, 0] != ' ' && board[0, 1] != ' ' && board[0, 2] != ' ' &&
            board[1, 0] != ' ' && board[1, 1] != ' ' && board[1, 2] != ' ' &&
            board[2, 0] != ' ' && board[2, 1] != ' ' && board[2, 2] != ' ')
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    static bool IsValidCoordinate(char[,] board, int x, int y)
    {
        return x >= 0 && x < 3 && y >= 0 && y < 3 && board[x, y] == ' ';
    }

    static void PrintBoard(char[,] board)
    {
        Console.WriteLine();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("--+---+--");
        }
        Console.WriteLine();
    }

    static bool HasWon(char[,] board, char player)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                return true;
        }
        return (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
               (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player);
    }
}