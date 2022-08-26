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
        /// <summary>
        /// Casts a <see cref="IQArray{T}"/> to an integer value.
        /// </summary>
        /// <param name="qbits">The <see cref="IQArray{T}"/> of qbits.</param>
        /// <returns>An <see cref="int"/> representation of the given <paramref name="qbits"/>.</returns>
        public static int ToInt(this IQArray<Result> qbits)
        {
            int result = 0;

            for (int i = 0; i < qbits.Length; i++)
            {
                result += (int)((qbits[i] == Result.One ? 1 : 0) * Math.Pow(2, qbits.Length - 1 - i));
            }
            return result;
        }

        /// <summary>
        /// Casts a <see cref="Result"/> to a <see cref="bool"/>.
        /// </summary>
        /// <param name="qbit">The <see cref="Result"/> to cast.</param>
        /// <returns>The <see cref="bool"/> representation of the given <paramref name="qbit"/>.</returns>
        public static bool ToBool(this Result qbit)
        {
            return qbit == Result.One;
        }

        public static int Modulo9(this int num)
        {
            return num % 9;
        }
    }
}
