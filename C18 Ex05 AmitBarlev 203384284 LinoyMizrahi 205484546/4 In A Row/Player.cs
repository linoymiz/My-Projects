using System;

namespace C18_Ex05
{
    public class Player
    {
        private readonly int r_HowManyColumns;//For CPU to have limit of Random.Next
        private char m_CoinSymbol;//Coin symbol - 'X' or 'O'
        private int m_PlayerScore;//Current score of player in the current session
        // $G$ CSS-999 (-3) variable name should be starting with r_PascaleCased
        readonly bool r_isACPU;//boolean whever player is a CPU or not

        public Player(char i_CoinSymbol, bool i_CPU = !true, int i_Columns = 0)
        {
            Coin = i_CoinSymbol;
            r_isACPU = i_CPU;
            r_HowManyColumns = i_Columns;
        }

        public int ColumnByCPU(GameBoard i_Board,ref Point i_LastCoinInserted)
        {
            System.Random randomColumn = new Random();
            int chosenColumn = randomColumn.Next(1, Column) - 1;
            while (i_Board.IsColumnFull(chosenColumn))
            {
                chosenColumn = randomColumn.Next(1, Column);
            }
            i_Board.SetCoinToBoard(chosenColumn, Coin, ref i_LastCoinInserted);

            return chosenColumn;
        }

        public char Coin
        {
            set { m_CoinSymbol = value; }
            get { return m_CoinSymbol; }
        }

        public int Score
        {
            set { m_PlayerScore = value; }
            get { return m_PlayerScore; }
        }

        public bool CPU
        {
            get { return r_isACPU; }
        }

        public int Column
        {
            get { return r_HowManyColumns; }
        }
    }
}