using System;
using System.Collections.Generic;
using System.Text;

namespace CutyOthello.Models
{
    class Board
    {
        bool NowTurn;
        int NowIndex;
        static readonly bool BlackTurn = true;
        static readonly bool WhiteTurn = false;


        UInt64 playerBoard;
        UInt64 opponentBoard;

        public Board()
        {
            NowTurn = BlackTurn;
            NowIndex = 1;
            playerBoard = 0x0000000810000000;
            opponentBoard = 0x0000001008000000;
        }

        /// <summary>
        /// タップしたビューの入力行、列をbitに変換する。
        /// </summary>
        /// <param name="x">行</param>
        /// <param name="y">列</param>
        /// <returns></returns>
        public UInt64 coordinateToBit(int x, int y)
        {
            UInt64 mask = 0x8000000000000000;

            mask = mask >> x;
            mask = mask >> y * 8;

            return mask;
        }

        /**
 * @brief  手番の入れ替え
 */
        public void swapBoard()
        {
            //ボードの入れ替え
            var tmp = playerBoard;
            playerBoard = opponentBoard;
            opponentBoard = tmp;

            //色の入れ替え
            NowTurn = !NowTurn;
        }

        public bool canPut(UInt64 put)
        {
            UInt64 legalBoard = makeLegalBoard(this);

            return (put & legalBoard) == put;
        }

        public UInt64 makeLegalBoard(Board board)
        {
            UInt64 horizontalWatchBoard = board.opponentBoard & 0x7e7e7e7e7e7e7e7e;//上下端の番人
            UInt64 verticalWatchBoard = board.opponentBoard & 0x00FFFFFFFFFFFF00;//上下端の番人
            UInt64 allSideWatchBoard = board.opponentBoard & 0x007e7e7e7e7e7e00;//上下端の番人
            UInt64 blankBoard = ~(board.playerBoard | board.opponentBoard);//ブランクボード
            UInt64 tmp;//隣に相手の色があるかを一時保存する
            UInt64 legalBoard;//返り値

            //左
            tmp = horizontalWatchBoard & (board.playerBoard << 1);
            for (int i = 0; i < 6; i++)
            {
                tmp |= horizontalWatchBoard & (tmp << 1);
            }
            legalBoard = blankBoard & (tmp << 1);

            //右
            tmp = horizontalWatchBoard & (board.playerBoard >> 1);
            for (int i = 0; i < 6; i++)
            {
                tmp |= horizontalWatchBoard & (tmp >> 1);
            }
            legalBoard |= blankBoard & (tmp >> 1);

            //上
            tmp = verticalWatchBoard & (board.playerBoard << 8);
            for (int i = 0; i < 6; i++)
            {
                tmp |= verticalWatchBoard & (tmp << 8);
            }
            legalBoard |= blankBoard & (tmp << 8);

            //下
            tmp = verticalWatchBoard & (board.playerBoard >> 8);
            for (int i = 0; i < 6; i++)
            {
                tmp |= verticalWatchBoard & (tmp >> 8);
            }
            legalBoard |= blankBoard & (tmp >> 8);

            //右上
            tmp = allSideWatchBoard & (board.playerBoard << 7);
            for (int i = 0; i < 6; i++)
            {
                tmp |= allSideWatchBoard & (tmp << 7);
            }
            legalBoard |= blankBoard & (tmp << 7);

            //左上
            tmp = allSideWatchBoard & (board.playerBoard << 9);
            for (int i = 0; i < 6; i++)
            {
                tmp |= allSideWatchBoard & (tmp << 9);
            }
            legalBoard |= blankBoard & (tmp << 9);

            //右下
            tmp = allSideWatchBoard & (board.playerBoard >> 9);
            for (int i = 0; i < 6; i++)
            {
                tmp |= allSideWatchBoard & (tmp >> 9);
            }
            legalBoard |= blankBoard & (tmp >> 9);

            //左下
            tmp = allSideWatchBoard & (board.playerBoard >> 7);
            for (int i = 0; i < 6; i++)
            {
                tmp |= allSideWatchBoard & (tmp >> 7);
            }
            legalBoard |= blankBoard & (tmp >> 7);

            return legalBoard;
        }

        public void reverse(UInt64 put)
        {
            UInt64 rev = 0;
            for (int i = 0; i < 8; i++)
            {
                UInt64 rev_ = 0;
                UInt64 mask = transfer(put: put, ReverseDirection: i);
                //ブランクではない且つ相手の石が置かれている。
                while ((mask != 0) && ((mask & opponentBoard) != 0))
                {
                    rev_ |= mask;
                    mask = transfer(put: mask, ReverseDirection: i);
                }
                //自分の石が置かれていないこと
                if ((mask & playerBoard) != 0)
                {
                    rev |= rev_;
                }
            }

            //反転する
            playerBoard ^= put | rev;
            opponentBoard ^= rev;

            //現在何手目かを更新
            NowIndex = NowIndex + 1;
        }

        public UInt64 transfer(UInt64 put, int ReverseDirection)
        {
            switch (ReverseDirection)
            {
                case 0: //上
                    return (put << 8) & 0xffffffffffffff00;
                case 1: //右上
                    return (put << 7) & 0x7f7f7f7f7f7f7f00;
                case 2: //右
                    return (put >> 1) & 0x7f7f7f7f7f7f7f7f;
                case 3: //右下
                    return (put >> 9) & 0x007f7f7f7f7f7f7f;
                case 4: //下
                    return (put >> 8) & 0x00ffffffffffffff;
                case 5: //左下
                    return (put >> 7) & 0x00fefefefefefefe;
                case 6: //左
                    return (put << 1) & 0xfefefefefefefefe;
                case 7: //左上
                    return (put << 9) & 0xfefefefefefefe00;
                default:
                    return 0;
            }

        }

        public bool isPass()
        {
            // 手番側の合法手ボードを生成
            var playerLegalBoard = makeLegalBoard(board: this);

            // 相手側の合法手ボードを生成    
            var tmpBoard = new Board();
            tmpBoard.NowTurn = NowTurn;
            tmpBoard.NowIndex = NowIndex;
            tmpBoard.playerBoard = playerBoard;
            tmpBoard.opponentBoard = opponentBoard;
            var opponentLegalBoard = makeLegalBoard(board: tmpBoard);

            // 手番側だけがパスの場合    
            return playerLegalBoard == 0x0000000000000000 && opponentLegalBoard != 0x0000000000000000;
        }

        public bool isGameFinished()
        {
            // 手番側の合法手ボードを生成
            var playerLegalBoard = makeLegalBoard(board: this);

            // 相手側の合法手ボードを生成    
            var tmpBoard = new Board();
            tmpBoard.NowTurn = NowTurn;
            tmpBoard.NowIndex = NowIndex;
            tmpBoard.playerBoard = playerBoard;
            tmpBoard.opponentBoard = opponentBoard;
            var opponentLegalBoard = makeLegalBoard(board: tmpBoard);

            // 両手番とも置く場所がない場合    
            return playerLegalBoard == 0x0000000000000000 && opponentLegalBoard == 0x0000000000000000;
        }


        public void getResult()
        {

            //石数を取得
            var blackScore = bitCount(playerBoard);
            var whiteScore = bitCount(opponentBoard);

            if (NowTurn == WhiteTurn)
            {
                var tmp = blackScore;
                blackScore = whiteScore;
                whiteScore = tmp;
            }
        }

        public List<List<int>> UintToListInt(UInt64 prmBoard)
        {
            //var tmp = playerBoard;
            List<List<int>> retrunBoard = new List<List<int>>()
            {
                new List<int>(), new List<int>()
            }
            ;

            for (int i = 1; i < 64; i++)
            {
                if ((prmBoard >> 63 - i & 0b1) == 0b1)
                {
                    retrunBoard[0].Add(i / 8);
                    retrunBoard[1].Add(i % 8);
                }
            }
            return retrunBoard;///+-            
        }

        public List<List<int>> GetBlackBoard()
        {
            if (NowTurn == BlackTurn)
            {
                return UintToListInt(playerBoard);
            }
            else
            {
                return UintToListInt(opponentBoard);
            }

        }

        public List<List<int>> GetWhiteBoard()
        {
            if (NowTurn == WhiteTurn)
            {
                return UintToListInt(playerBoard);
            }
            else
            {
                return UintToListInt(opponentBoard);
            }
        }

        public int bitCount(UInt64 num)
        {
            UInt64 mask = 0x8000000000000000;
            int count = 0;


            for (int i = 0; i > 64; i++)
            {

                if ((mask & num) != 0)
                {
                    count += 1;
                }
                mask = mask >> 1;
            }
            return count;
        }
    }
}
