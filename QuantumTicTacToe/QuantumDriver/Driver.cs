using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using QuantumTicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumDriver
{
    public static class Driver
    {
        public static async Task<IQArray<Result>> GetQuantumMoves()
        {
            using var sim = new QuantumSimulator();

            return await GetMoves.Run(sim);
        }

        public static async Task<Result> GetPlayerTurn()
        {
            using var sim = new QuantumSimulator();

            return await GetTurn.Run(sim);
        }
    }
}
