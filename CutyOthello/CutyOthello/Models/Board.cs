using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CutyOthello.Models
{
    class Board
    {
        public bool NowTurn;
        int NowIndex;
        static readonly bool BlackTurn = true;
        static readonly bool WhiteTurn = false;
        public int SakiyomiNum = 1;
        public int BlankBoardNum = 0;
        UInt64 playerBoard;
        UInt64 opponentBoard;
        public UInt64 OptimalBoard;

        public Board()
        {
            NowTurn = BlackTurn;
            NowIndex = 1;
            playerBoard = 0x0000000810000000;
            opponentBoard = 0x0000001008000000;
        }

        public void setSakiyomiNum(int LevelInfo)
        {
            SakiyomiNum = LevelInfo;
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

        public UInt64[] reverse(UInt64 put, Board board)
        {
            UInt64 rev = 0;
            for (int i = 0; i < 8; i++)
            {
                UInt64 rev_ = 0;
                UInt64 mask = transfer(put: put, ReverseDirection: i);
                //ブランクではない且つ相手の石が置かれている。
                while ((mask != 0) && ((mask & board.opponentBoard) != 0))
                {
                    rev_ |= mask;
                    mask = transfer(put: mask, ReverseDirection: i);
                }
                //自分の石が置かれていないこと
                if ((mask & board.playerBoard) != 0)
                {
                    rev |= rev_;
                }
            }

            //反転した後のUint64を返す。
            return new UInt64[]
            {
                board.playerBoard ^ put | rev,
                board.opponentBoard ^ rev
            };
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
            tmpBoard.playerBoard = opponentBoard;
            tmpBoard.opponentBoard = playerBoard;
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
            tmpBoard.playerBoard = opponentBoard;
            tmpBoard.opponentBoard = playerBoard;
            var opponentLegalBoard = makeLegalBoard(board: tmpBoard);

            // 両手番とも置く場所がない場合    
            return playerLegalBoard == 0x0000000000000000 && opponentLegalBoard == 0x0000000000000000;
        }


        public int getResult(bool BlackOrWhite)
        {
            if (BlackOrWhite)
            {
                if (NowTurn == BlackTurn)
                    return bitCount(playerBoard);
                else
                    return bitCount(opponentBoard);
            }
            else
            {
                if (NowTurn == WhiteTurn)
                    return bitCount(playerBoard);
                else
                    return bitCount(opponentBoard);
            }
        }

        public List<List<int>> UintToListInt(UInt64 prmBoard)
        {
            List<List<int>> retrunBoard = new List<List<int>>()
            {
                new List<int>(), new List<int>()
            };

            for (int i = 0; i < 64; i++)
            {
                if ((prmBoard >> 63 - i & 0b1) == 0b1)
                {
                    retrunBoard[0].Add(i / 8);
                    retrunBoard[1].Add(i % 8);
                }
            }
            return retrunBoard;
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

        public List<List<int>> GetBlankBoard()
        {
            return UintToListInt(~(playerBoard | opponentBoard | makeLegalBoard(this)));
        }

        public int bitCount(UInt64 num)
        {
            UInt64 mask = 0x8000000000000000;
            int count = 0;

            for (int i = 0; i < 64; i++)
            {

                if ((mask & num) != 0)
                {
                    count += 1;
                }
                mask = mask >> 1;
            }
            return count;
        }

        /// <summary>
        /// 局面を評価し点数で返す。
        /// </summary>
        /// <param name="PlayerB"></param>
        /// <param name="OpponentB"></param>
        /// <returns></returns>
        public int assessBoad(UInt64 PlayerB, UInt64 OpponentB)
        {
            int Point = 0;

            UInt64 StaticEvaluation8Board = 0x8100000000000081;
            UInt64 StaticEvaluation3Board = 0x3c0081818181003c;
            UInt64 StaticEvaluation2Board = 0x003c424242423c00;
            UInt64 StaticEvaluation1Board = 0x00423c3c3c3c4200;
            UInt64 StaticEvaluationMinus2Board = 0x4281000000008142;

            Point += bitCount(PlayerB & StaticEvaluation8Board) * 8;
            Point += bitCount(PlayerB & StaticEvaluation3Board) * 3;
            Point += bitCount(PlayerB & StaticEvaluation2Board) * 2;
            Point += bitCount(PlayerB & StaticEvaluation1Board);
            Point += bitCount(PlayerB & StaticEvaluationMinus2Board) * -2;
            Point -= bitCount(OpponentB & StaticEvaluation8Board) * 8;
            Point -= bitCount(OpponentB & StaticEvaluation3Board) * 3;
            Point -= bitCount(OpponentB & StaticEvaluation2Board) * 2;
            Point -= bitCount(OpponentB & StaticEvaluation1Board);
            Point -= bitCount(OpponentB & StaticEvaluationMinus2Board) * -2;

            //Console.WriteLine("点数　：　"　+　Point);
            return Point;
        }

        public void StartUpMinMax()
        {
            OptimalBoard = 0;            
            BlankBoardNum = 64 - bitCount(playerBoard) - bitCount(opponentBoard);
            if (BlankBoardNum > SakiyomiNum) MinMax(playerBoard, opponentBoard, SakiyomiNum, false);
            else MinMax(playerBoard, opponentBoard, BlankBoardNum % 2 == 1 ? BlankBoardNum : BlankBoardNum -1, false);
        }


        /// <summary>
        /// ミニマックス法により最適な手を返す。
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Oppponent"></param>
        /// <param name="turn"></param>
        /// <param name="depth"></param>
        public int MinMax(UInt64 PlayerB, UInt64 OpponentB, int depth,bool SaikiHantei = true)
        {
            //葉の場合、評価値を返す。
            if (depth == 0) return assessBoad(PlayerB, OpponentB);

            // 相手側の合法手ボードを生成    
            var tmpBoard = new Board();
            if (SaikiHantei)
            {
                tmpBoard.playerBoard = OpponentB;
                tmpBoard.opponentBoard = PlayerB;
            }
            else
            {
                tmpBoard.playerBoard = PlayerB;
                tmpBoard.opponentBoard = OpponentB;
            }
            var opponentLegalBoard = makeLegalBoard(board: tmpBoard);

            int best = -3000;
            int worst = 3000;

            //x=1 y=1 の石をスタートとする。。
            UInt64 put = 0x8000000000000000;
            
            for (int i = 1; i < 65; i++)
            {
                if ((put & opponentLegalBoard) == put)
                {
                    var Board = this.reverse(put, tmpBoard);
                    //Console.WriteLine("リバース開始");
                    //ConsoleWriteLine(depth, Board[0]);
                    //ConsoleWriteLine(depth, Board[1]);
                    //Console.WriteLine("リバース終了");
                    var val = MinMax(Board[0], Board[1], depth - 1);
                    //ConsoleWriteLine(depth, put);
                    //Console.WriteLine("点数" + val);

                    if (depth % 2 == 1 && best < val)
                    {
                        best = val;
                        if (BlankBoardNum > SakiyomiNum)
                        {
                            if (depth == SakiyomiNum)
                                OptimalBoard = put;
                        }
                        else if(BlankBoardNum <= SakiyomiNum)
                        {
                            if (depth == BlankBoardNum)
                                OptimalBoard = put;
                        }
                    }
                    else if (depth % 2 == 0 && worst > val)
                    {
                        worst = val;
                    }
                }
                put = put >> 1;
            }

            return depth % 2 == 1 ? best : worst;
        }

        [Conditional("DEBUG")]
        private void ConsoleWriteLine(int depth, UInt64 put)
        {
            for (int i = 0; i < 64; i++)
            {
                if ((put >> 63 - i & 0b1) == 0b1)
                {
                    System.Diagnostics.Debug.WriteLine("現在の手数　：" + NowIndex + " 　　ターン：" + NowTurn.ToString() + "     深さ :" + depth + "    row :" + i / 8 + "　　col :" + i % 8); ;
                }
            }
        }
    }
}
