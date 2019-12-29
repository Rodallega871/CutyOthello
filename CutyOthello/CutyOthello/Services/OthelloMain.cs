using CutyOthello.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CutyOthello.Services
{
    class OthelloMain
    {
        Board board;

        public OthelloMain()
        {
            board = new Board();
        }

        public bool PutStone(int x, int y)
        {
            var put = board.coordinateToBit(x, y);

            if (board.canPut(put))
            {
                board.reverse(put);
                board.swapBoard();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PutStoneCPU()
        {
            board.StartUpMinMax();
            board.reverse(board.OptimalBoard);
            board.swapBoard();
        }

        public bool IsPass()
        {
            if (board.isPass())
            {
                board.swapBoard();
                return true;
            }
            return false;
        }

        public bool IsGameFinished()
        {
            return board.isGameFinished();
        }

        public List<List<int>> GetBlackStoneList()
        {
            return board.GetBlackBoard();
        }

        public List<List<int>> GetWhiteStoneList()
        {
            return board.GetWhiteBoard();
        }

        public List<List<int>> GetNextStoneList()
        {
            return board.UintToListInt(board.makeLegalBoard(board));
        }

        public List<List<int>> GetBlankList()
        {
            return board.GetBlankBoard();
        }

        public int GetBlackStoneCount()
        {
            return board.getResult(true); //true : Black            
        }

        public int GetWhiteStoneCount()
        {
            return board.getResult(false); //false : White            
        }

        public void SetCpuLevel(int LevelInfo)
        {
            board.setSakiyomiNum(LevelInfo * 2 - 1);
        }

        public bool GetNowTurn()
        {
            return board.NowTurn;
        }
    }
}
