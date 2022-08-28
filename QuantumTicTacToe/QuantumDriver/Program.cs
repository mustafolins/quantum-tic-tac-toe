using Microsoft.Quantum.Simulation.Core;

namespace QuantumDriver
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            bool hasPlayer = false;
            if (args.Length > 0)
            {
                bool.TryParse(args[0], out hasPlayer);
            }

            var board = new Board();
            var winStats = new int[3];

            for (int i = 0; i < 50; i++)
            {
                // play game of tic tac toe
                var (isWin, player) = await board.PlayGame(hasPlayer);
                // increment win stat for players
                if (isWin && player != null)
                {
                    winStats[(int)player]++;
                }
                // for ties
                else
                {
                    winStats[2]++;
                }

                board.Clear();

                await Task.Delay(2_500);
            }

            Console.WriteLine($"X:{winStats[0]},O:{winStats[1]},T:{winStats[2]}");

            return 0;
        }
    }
}