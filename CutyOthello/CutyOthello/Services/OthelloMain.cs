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

        public void Check()
        {
            if (board.isPass())
            {
                board.swapBoard();
            }

            if (board.isGameFinished())
            {
                board.getResult();
            }
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
}
}
