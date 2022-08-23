using Microsoft.Quantum.Simulation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumDriver
{
    public static class QuantumExtensions
    {
        public static int ToInt(this IQArray<Result> qbits)
        {
            int result = 0;

            for (int i = 0; i < qbits.Length; i++)
            {
                result += (int)((qbits[i] == Result.One ? 1 : 0) * Math.Pow(2, qbits.Length - 1 - i));
            }
            return result;
        }

        public static int Modulo9(this int num)
        {
            return num % 9;
        }
    }
}
