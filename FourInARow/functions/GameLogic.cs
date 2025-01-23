using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static FourInARow.MainWindow;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using static FourInARow.functions.GameLogic;
using FourInARow;
using static FourInARow.GameBoardBack;

namespace FourInARow.functions
{
    public static class GameLogic
    {
        public enum Won { noWin, horizontal, vertical, diagonalUpRight, diagonalDownRight }
        public static Won WayWon=Won.noWin;
        public static bool CheckWin(GameBoardBack gameBoard, Player player, out int winningRow, out int winningCol)
        {
            int rows = gameBoard.Board.GetLength(0); // Get number of rows
            int columns = gameBoard.Board.GetLength(1); // Get number of columns
            winningRow = -1;
            winningCol=-1;
            // Check horizontal and vertical wins
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    // Check horizontal (right direction)
                    if (col + 3 < columns &&
                        gameBoard.Board[row, col] == player &&
                        gameBoard.Board[row, col + 1] == player &&
                        gameBoard.Board[row, col + 2] == player &&
                        gameBoard.Board[row, col + 3] == player)
                    {
                        WayWon = Won.horizontal;
                        winningRow = row;
                        winningCol = col;

                        return true;
                    }

                    // Check vertical (down direction)
                    if (row + 3 < rows &&
                        gameBoard.Board[row, col] == player &&
                       gameBoard.Board[row + 1, col] == player &&
                        gameBoard.Board[row + 2, col] == player &&
                        gameBoard.Board[row + 3, col] == player)
                    {
                        WayWon = Won.vertical;
                        winningRow = row;
                        winningCol = col;

                        return true;
                    }

                    // Check diagonal (down-right direction)
                    if (row + 3 < rows && col + 3 < columns &&
                        gameBoard.Board[row, col] == player &&
                        gameBoard.Board[row + 1, col + 1] == player &&
                        gameBoard.Board[row + 2, col + 2] == player &&
                        gameBoard.Board[row + 3, col + 3] == player)
                    {
                        WayWon = Won.diagonalDownRight;
                        winningRow = row;
                        winningCol = col;
                        return true;
                    }

                    // Check diagonal (up-right direction)
                    if (row - 3 >= 0 && col + 3 < columns &&
                        gameBoard.Board[row, col] == player &&
                        gameBoard.Board[row - 1, col + 1] == player &&
                        gameBoard.Board[row - 2, col + 2] == player &&
                        gameBoard.Board[row - 3, col + 3] == player)
                    {
                        WayWon = Won.diagonalUpRight;
                        winningRow = row;
                        winningCol = col;
                        return true;
                    }
                }
            }

            // If no win was found
            return false;
        }
    }
}
