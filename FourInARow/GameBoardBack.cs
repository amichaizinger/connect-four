using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FourInARow.GameBoardBack;
using System.Windows.Media;
using System.Data.Common;

namespace FourInARow
{
    public class GameBoardBack
    {
        public enum Player { None, Blue, Red }        
        public Player[,] Board { get; set; }
        public Player CurrentPlayer { get; set; }
        public bool[] FullColumn { get; set; }

        //public event Action<string> OnGameStatusChanged;
        //public event Action OnGameWon;

        public GameBoardBack()
        {
            Board = new Player[6, 7]; // 6 rows, 7 columns
            FullColumn = new bool[7];
            CurrentPlayer = Player.Blue;           
        }
        public bool DropDisc(int column, out int discRow, out int discCol)
        {
             discRow=-1;
             discCol=-1;
            if (FullColumn[column])
            {
                return false;
            }
            for (int row = 5; row >= 0; row--)
            {
                if (Board[row, column] == Player.None)
                {
                    Board[row, column] = CurrentPlayer;
                    discRow = row;
                    discCol=column;
                    IsColumnFull(column);

                    return true;
                }
            }
            return false;   
        }
        public void IsColumnFull(int column)
        {
            if (Board[0, column] != Player.None)
            {
                FullColumn[column] = true;
            }
        }
        public bool AllColumnsFull(bool[] fullColumn)
        {         
            for (int col = 0; col < fullColumn.Length; col++)
            {
               if (!fullColumn[col])
                {
                    return false;
                }
            }
            
            return true;
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == Player.Blue) ? Player.Red : Player.Blue;
        }

    }
}
