using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C18_Ex05
{
    public class Game
    {
        private int m_NumberOfTurns = 0;
        private readonly char m_Player1Symbol = 'O';
        private readonly char m_Player2Symbol = 'X';
        private readonly int r_MinCoinSequenceToWin = 4;
        private Player[] m_Players;//Array of players
        // $G$ DSN-999 (-3) This kind of field should be readonly.
        private GameBoard m_Board;//Game board
        private Point m_LastCoinInserted = new Point();//Last coin inserted by any player

        // $G$ CSS-999 (-3) Event name should be PascaleCased
        public event Action<Point, char> m_UpdateBoardSequence;
        public event Action<int> m_DisableButton;
        public event Action<int, int> m_WinAlert;
        public event Action m_TieAlert;

        public Game(PreGameParameters i_Parameters)
        {
            m_Board = new GameBoard(i_Parameters.Row, i_Parameters.Column);
            m_Players = new Player[2];
            m_Players[0] = new Player(m_Player1Symbol);
            m_Players[1] = new Player(m_Player2Symbol,
                i_Parameters.CPU, i_Parameters.Column);
        }

        public void PassCoinToBoard(object sender, EventArgs e)
        {
            int column = int.Parse((sender as Button).Text) - 1;
            m_Board.SetCoinToBoard(column, m_Players[m_NumberOfTurns % 2].Coin,ref m_LastCoinInserted);
            // $G$ NTT-999 (-5) You should make sure a delegate is not null before calling it.  
            m_UpdateBoardSequence(m_LastCoinInserted, m_Players[m_NumberOfTurns % 2].Coin);
            postTurnChecking(column);
            m_NumberOfTurns++;
            insertCoinCPU();
        }

        private void insertCoinCPU()
        {
            if (m_Players[m_NumberOfTurns % 2].CPU)
            {
                int chosenColumn = m_Players[m_NumberOfTurns % 2].
                    ColumnByCPU(m_Board,ref m_LastCoinInserted);
                m_UpdateBoardSequence(m_LastCoinInserted, m_Players[m_NumberOfTurns % 2].Coin);
                postTurnChecking(chosenColumn);
                m_NumberOfTurns++;
            }
        }

        private void postTurnChecking(int i_ChosenColumn)
        {
            if (hasPlayerWon(m_Players[m_NumberOfTurns % 2].Coin))
            {
                m_Board.ClearBoard();
                // $G$ NTT-999 (-3) You should make sure a delegate is not null before calling it.  
                m_WinAlert(m_NumberOfTurns % 2, ++m_Players[m_NumberOfTurns % 2].Score);
            }
            else if (m_Board.IsColumnFull(i_ChosenColumn))
            {
                if (m_Board.IsBoardGameFull())
                {
                    m_Board.ClearBoard();
                    // $G$ NTT-999 (-3) You should make sure a delegate is not null before calling it.  
                    m_TieAlert();
                }
                else
                {
                    // $G$ NTT-999 (-3) You should make sure a delegate is not null before calling it.  
                    m_DisableButton(i_ChosenColumn);
                }
            }
        }

        private bool hasPlayerWon(char i_CoinSymbol)
        {
            return checkColumnWin(i_CoinSymbol) ||
                checkRowWin(i_CoinSymbol) ||
                CheckDiagonalWin(i_CoinSymbol) ||
                checkSecondaryDiagonalWin(i_CoinSymbol);
        }

        //Calls 4 methods each turn to check if player has won

        private bool checkColumnWin(char i_CoinSymbol)
        {
            int counterOfCoins = 1;
            int howManySquaresToCheck =
                claculateSquareToCheckRightBottomBoundary(m_Board.Row - 1, m_LastCoinInserted.Row);

            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row + i, m_LastCoinInserted.Column)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins >= r_MinCoinSequenceToWin;
        }

        //Checks maximum of 3 square below last inserted coin, checks for a column of 4 coins

        private bool checkRowWin(char i_CoinSymbol)
        {
            int counterOfCoins = 1;

            counterOfCoins += checkRowToTheRight(i_CoinSymbol);
            counterOfCoins += checkRowToTheLeft(i_CoinSymbol);

            return counterOfCoins >= r_MinCoinSequenceToWin;
        }

        //Calls two methods, one checks to the right of last coin, and one to the left 

        private int checkRowToTheRight(char i_CoinSymbol)
        {
            int counterOfCoins = 0;
            int howManySquaresToCheck =
                claculateSquareToCheckRightBottomBoundary(m_Board.Column - 1, m_LastCoinInserted.Column);

            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row, m_LastCoinInserted.Column + i)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins;
        }

        //Checks maximum of 3 squares to the right, counts how many coins to the right

        private int checkRowToTheLeft(char i_CoinSymbol)
        {
            int counterOfCoins = 0;
            int howManySquaresToCheck = claculateSquareToCheckLeftTopBoundary(m_LastCoinInserted.Column);

            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row, m_LastCoinInserted.Column - i)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins;
        }

        //Checks maximum of 3 squares to the left, counts how many coins to the left

        private bool CheckDiagonalWin(char i_CoinSymbol)
        {
            int counterOfCoins = 1;

            counterOfCoins +=
                checkPointToTheRightBottom(i_CoinSymbol);
            counterOfCoins +=
                checkPointToTheLeftTop(i_CoinSymbol);

            return counterOfCoins >= r_MinCoinSequenceToWin;
        }

        //Calls two methods, one counts coin to the bottom-right, the other top-left 

        private int checkPointToTheRightBottom(char i_CoinSymbol)
        {
            int counterOfCoins = 0;

            int howManySquaresToCheck =
                calculateHowManySquaresDiagonal(
                    claculateSquareToCheckRightBottomBoundary(m_Board.Column - 1, m_LastCoinInserted.Column),
                    claculateSquareToCheckRightBottomBoundary(m_Board.Row - 1, m_LastCoinInserted.Row));
            //Calculates how many squares needs to be checked in each direction (in case square is close to boundaries)
            //Chooses the smaller number 

            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row + i, m_LastCoinInserted.Column + i)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins;
        }

        //Checks maximum of 3 squares to the bottom-right and counts coins

        private int calculateHowManySquaresDiagonal(int i_HowManySquaresHorizonalAxis,
            int i_HowManySquaresVerticalAxis)
        {
            int howManySquaresToCheck = i_HowManySquaresHorizonalAxis < i_HowManySquaresVerticalAxis ?
                i_HowManySquaresHorizonalAxis : i_HowManySquaresVerticalAxis;

            return howManySquaresToCheck;
        }

        //Checks how many squares needed to be check in diagonal direction

        private int checkPointToTheLeftTop(char i_CoinSymbol)
        {
            int counterOfCoins = 0;

            int howManySquaresToCheck =
                calculateHowManySquaresDiagonal(
                    claculateSquareToCheckLeftTopBoundary(m_LastCoinInserted.Column),
                    claculateSquareToCheckLeftTopBoundary(m_LastCoinInserted.Row));
            //Calculates how many squares needs to be checked in each direction (in case square is close to boundaries)
            //Chooses the smaller number
            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row - i, m_LastCoinInserted.Column - i)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins;
        }

        //Checks maximum of 3 squares to the top-left and counts coins

        private bool checkSecondaryDiagonalWin(char i_CoinSymbol)
        {
            int counterOfCoins = 1;

            counterOfCoins += checkPointToTheRightTop(i_CoinSymbol);
            counterOfCoins += checkPointToTheLeftBottom(i_CoinSymbol);

            return counterOfCoins >= r_MinCoinSequenceToWin;
        }

        //Calls two methods which checks Top-Right and bottom-left directions

        private int checkPointToTheRightTop(char i_CoinSymbol)
        {
            int counterOfCoins = 0;

            int howManySquaresToCheck =
                calculateHowManySquaresDiagonal(
                    claculateSquareToCheckRightBottomBoundary(m_Board.Column - 1, m_LastCoinInserted.Column),
                    claculateSquareToCheckLeftTopBoundary(m_LastCoinInserted.Row));
            //Calculates how many squares needs to be checked in each direction (in case square is close to boundaries)
            //Chooses the smaller number


            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row - i, m_LastCoinInserted.Column + i)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins;
        }

        //Checks maximum of 3 squares to the top-right and counts coins

        private int checkPointToTheLeftBottom(char i_CoinSymbol)
        {
            int counterOfCoins = 0;

            int howManySquaresToCheck =
                calculateHowManySquaresDiagonal
                (
                    claculateSquareToCheckRightBottomBoundary(m_Board.Row - 1, m_LastCoinInserted.Row),
                    claculateSquareToCheckLeftTopBoundary(m_LastCoinInserted.Column));
            //Calculates how many squares needs to be checked in each direction (in case square is close to boundaries)
            //Chooses the smaller number

            for (int i = 1; i <= howManySquaresToCheck; i++)
            {
                if (m_Board.GetSquare(m_LastCoinInserted.Row + i, m_LastCoinInserted.Column - i)
                    == i_CoinSymbol)
                {
                    counterOfCoins++;
                }
                else
                {
                    break;
                }
            }

            return counterOfCoins;
        }

        private int claculateSquareToCheckRightBottomBoundary(int i_BoardBoundary, int i_CurrentAxisChosen)
        {
            return (i_BoardBoundary - i_CurrentAxisChosen) > 3 ? 3 : (i_BoardBoundary - i_CurrentAxisChosen);
        }

        private int claculateSquareToCheckLeftTopBoundary(int i_CurrentAxisChosen)
        {
            return i_CurrentAxisChosen > 3 ? 3 : i_CurrentAxisChosen;
        }
    }
}