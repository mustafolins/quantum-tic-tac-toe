using Microsoft.Quantum.Simulation.Core;

namespace QuantumDriver
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var board = new Board();
            var winStats = new int[3];

            for (int i = 0; i < 50; i++)
            {
                // play game of tic tac toe
                var winCondition = await board.PlayGame();
                // increment win stat for players
                if (winCondition.player != null)
                {
                    winStats[(int)winCondition.player]++;
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