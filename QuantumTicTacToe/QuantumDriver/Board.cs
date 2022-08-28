using Microsoft.Quantum.Simulation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantumDriver
{
    public class Board
    {
        public Position[] Positions { get; set; }

        public Board()
        {
            Positions = new Position[9];
        }

        public async Task<(bool isWin, Player? player)> PlayGame(bool hasPlayer = true)
        {
            Player? curPlayer = null;
            var winCondition = HasWin();
            var turn = await Driver.GetPlayerTurn();

            while (!winCondition.isWin && !IsTie())
            {
                // get new quantum moves
                IQArray<Result> moves = await Driver.GetQuantumMoves();

                // set current player if null
                if (curPlayer == null)
                {
                    curPlayer = (Player)(turn.ToBool() ? 0 : 1);
                }

                // if move was already on board try again
                if (!AddPosition(curPlayer.Value, (hasPlayer && curPlayer == Player.One) ? GetPlayerMove(curPlayer) - 1 : moves.ToInt().Modulo9()))
                    continue;

                // update current player
                curPlayer = (Player)(((int)curPlayer + 1) % 2);

                // wait and redraw board
                await Task.Delay(500);
                Console.Clear();
                Console.WriteLine(this);

                winCondition = HasWin();
            }

            // display win message
            if (winCondition.isWin)
            {
                Console.WriteLine($"Winning Player is {(winCondition.player == Player.One ? "X" : "O")}!");
            }
            else
            {
                Console.WriteLine($"Tie!");
            }

            return winCondition;
        }

        private int GetPlayerMove(Player? curPlayer)
        {
            Console.WriteLine($"Player {curPlayer}'s turn.");

            var keyInfo = Console.ReadKey(true);

            return keyInfo.Key switch
            {
                ConsoleKey.NumPad1 => 7,
                ConsoleKey.D1 => 7,
                ConsoleKey.End => 7,
                ConsoleKey.NumPad2 => 8,
                ConsoleKey.D2 => 8,
                ConsoleKey.DownArrow => 8,
                ConsoleKey.NumPad3 => 9,
                ConsoleKey.D3 => 9,
                ConsoleKey.PageDown => 9,
                ConsoleKey.NumPad4 => 4,
                ConsoleKey.D4 => 4,
                ConsoleKey.LeftArrow => 4,
                ConsoleKey.NumPad5 => 5,
                ConsoleKey.Clear => 5,
                ConsoleKey.D5 => 5,
                ConsoleKey.NumPad6 => 6,
                ConsoleKey.D6 => 6,
                ConsoleKey.RightArrow => 6,
                ConsoleKey.NumPad7 => 1,
                ConsoleKey.D7 => 1,
                ConsoleKey.Home => 1,
                ConsoleKey.NumPad8 => 2,
                ConsoleKey.D8 => 2,
                ConsoleKey.UpArrow => 2,
                ConsoleKey.NumPad9 => 3,
                ConsoleKey.D9 => 3,
                ConsoleKey.PageUp => 3,
                _ => GetPlayerMove(curPlayer),
            };
        }

        public bool AddPosition(Player player, int location)
        {
            if (Positions[location] == null)
            {
                Positions[location] = new Position(player);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Positions = new Position[9];
        }

        public (bool isWin, Player? player) HasWin()
        {
            if (AllPositionsNull())
            {
                return (false, null);
            }
            for (int i = 0; i < 3; i++)
            {
                // horizontal
                if (Positions[(i*3) + 0]?.Player == Positions[(i * 3) + 1]?.Player && Positions[(i * 3) + 1]?.Player == Positions[(i * 3) + 2]?.Player && Positions[(i * 3) + 2] != null)
                {
                    return (true, Positions[(i * 3) + 0]?.Player);
                }
                // vertical
                if (Positions[i]?.Player == Positions[3 + i]?.Player && Positions[3 + i]?.Player == Positions[6 + i]?.Player && Positions[6 + i] != null)
                {
                    return (true, Positions[i]?.Player);
                }
            }
            // diagonal
            if (Positions[0]?.Player == Positions[4]?.Player && Positions[4]?.Player == Positions[8]?.Player && Positions[8] != null)
            {
                return (true, Positions[0]?.Player);
            }
            else if (Positions[2]?.Player == Positions[4]?.Player && Positions[4]?.Player == Positions[6]?.Player && Positions[6] != null)
            {
                return (true, Positions[2]?.Player);
            }
            return (false, null);
        }

        public bool IsTie()
        {
            return !HasWin().isWin && AllPositionsNotNull();
        }

        private bool AllPositionsNull()
        {
            foreach (var position in Positions)
            {
                if (position != null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool AllPositionsNotNull()
        {
            foreach (var position in Positions)
            {
                if (position == null)
                {
                    return false;
                }
            }
            return true;
        }

        public override string? ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < Positions.Length; i++)
            {
                if (i % 3 == 0 && i > 0)
                {
                    result.AppendLine("\n-|-|-");
                }

                Position? position = Positions[i];
                if (position == null)
                {
                    result.Append(" ");
                }
                else
                {
                    result.Append($"{position}");
                }
                if ((i + 1) % 3 != 0)
                {
                    result.Append('|'); 
                }
            }
            return result.ToString();
        }
    }
}
