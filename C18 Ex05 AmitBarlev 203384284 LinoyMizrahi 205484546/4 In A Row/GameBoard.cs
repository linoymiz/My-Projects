using System;

namespace C18_Ex05
{
    public class GameBoard
    {
        private readonly int r_NumOfRows;//How many rows, chosen by user
        private readonly int r_NumOfCols;//How many column, chosen by user
        // $G$ DSN-999 (-3) This array should be readonly.
        private char[,] m_Board;//Matrix that represents board

        public GameBoard(int i_NumOfRows, int i_NumOfCols)
        {
            r_NumOfRows = i_NumOfRows;
            r_NumOfCols = i_NumOfCols;
            m_Board = new char[r_NumOfRows, r_NumOfCols];
        }

        public int Column
        {
            get { return r_NumOfCols; }
        }

        public int Row
        {
            get { return r_NumOfRows; }
        }

        public bool IsBoardGameFull()
        {
            bool hasGameEnded = true;

            for (int i = 0; i < r_NumOfCols; i++)
            {
                if (!IsColumnFull(i))
                {
                    hasGameEnded = !true;
                    break;
                }
            }
            return hasGameEnded;
        }

        public char GetSquare(int i_Row, int i_Column)
        {
            return m_Board[i_Row, i_Column];
        }

        public bool IsColumnFull(int i_CurrentColumnChosen)
        {
            return !(m_Board[0, i_CurrentColumnChosen] == '\0');
        }

        //Checks if first cell in certain column is '\0', if so, column is not full

        public void SetCoinToBoard(int i_ChosenColumn, char i_CoinSymbol,ref Point i_LastCoin)
        {
            setCoinToBoard(i_ChosenColumn, i_CoinSymbol,ref i_LastCoin);
        }

        //Calls private method

        private void setCoinToBoard(int i_ChosenColumn, char i_CoinSymbol,ref Point i_LastCoin)
        {
            int i = findHighestPointInColumn(i_ChosenColumn);
            if (!IsColumnFull(i_ChosenColumn))
            {
                i_LastCoin.Row = i - 1;
                i_LastCoin.Column = i_ChosenColumn;
                m_Board[i_LastCoin.Row, i_LastCoin.Column] = i_CoinSymbol;
            }
        }

        //Checks maximum of 3 squares to the bottom-left and counts coins
        private int findHighestPointInColumn(int i_LastColumnInserted)
        {
            int i = 0;

            while (i < r_NumOfRows && m_Board[i, i_LastColumnInserted] == '\0')
            {
                i++;
            }

            return i;
        }

        public void ClearBoard()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    m_Board[i, j] = '\0';
                }
            }
        }
    }
}