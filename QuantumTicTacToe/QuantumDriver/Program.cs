using Microsoft.Quantum.Simulation.Core;

namespace QuantumDriver
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            IQArray<IQArray<IQArray<Result>>> moves = await Driver.GetQuantumMoves();

            foreach (var turn in moves[1])
            {
                Console.WriteLine(turn);
            }

            Console.WriteLine($"{moves}");

            return 0;
        }
    } 
}