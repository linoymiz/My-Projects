using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C18_Ex05
{
    public class GameInterface : Form
    { 
        private Button[] m_ColumnOfNumbers;
        private Button[,] m_BoardButtons;
        private Label[] m_Labels;
        private string[] m_Names;

        private readonly int  r_ButtonWidth = 70;
        private readonly int r_ButtonHeight = 40;
        private readonly int r_MarginBorder = 20;
        private readonly int r_NumericColumnTop = 10;
        private readonly int r_Columns;
        private readonly int r_Rows;

        public GameInterface(PreGameParameters i_Parameters, Game i_Game)
        {
            m_Names = i_Parameters.GetPlayersNames();
            base.Text = "4 In A Row";
            r_Columns = i_Parameters.Column;
            r_Rows = i_Parameters.Row;

            initializeNumericRow(r_Columns, i_Game);
            initializeBoard(r_Rows, r_Columns);
            initializeLabels(r_Columns, r_Rows);
            integrateGame(i_Game);
        }

        private void initializeNumericRow(int i_Columns,Game i_Game)
        {
            m_ColumnOfNumbers = new Button[i_Columns];

            initializeFirstButton(i_Game);
            for (int i = 1; i < i_Columns; i++)
            {
                m_ColumnOfNumbers[i] = new Button();
                m_ColumnOfNumbers[i].Top = m_ColumnOfNumbers[i - 1].Top;
                m_ColumnOfNumbers[i].Left = m_ColumnOfNumbers[i - 1].Right + 5;
                m_ColumnOfNumbers[i].Width = r_ButtonWidth;
                m_ColumnOfNumbers[i].Height = r_ButtonHeight;
                m_ColumnOfNumbers[i].Font = new System.Drawing.Font(m_ColumnOfNumbers[0].Font.Name, 15);
                m_ColumnOfNumbers[i].Text = string.Format("{0}", i + 1);
                m_ColumnOfNumbers[i].Click += i_Game.PassCoinToBoard;
                Controls.Add(m_ColumnOfNumbers[i]);
            }

            this.Width = m_ColumnOfNumbers[i_Columns - 1].Right + r_MarginBorder + 15;
        }

        private void initializeFirstButton(Game i_Game)
        {
            m_ColumnOfNumbers[0] = new Button();
            m_ColumnOfNumbers[0].Left = r_MarginBorder;
            m_ColumnOfNumbers[0].Top = r_NumericColumnTop;
            m_ColumnOfNumbers[0].Width = r_ButtonWidth;
            m_ColumnOfNumbers[0].Height = r_ButtonHeight;
            m_ColumnOfNumbers[0].Font = new System.Drawing.Font(m_ColumnOfNumbers[0].Font.Name, 15);
            m_ColumnOfNumbers[0].Text = "1";
            m_ColumnOfNumbers[0].Click += i_Game.PassCoinToBoard;
            Controls.Add(m_ColumnOfNumbers[0]);
        }

        private void initializeBoard(int i_Rows, int i_Columns)
        {
            m_BoardButtons = new Button[i_Rows, i_Columns];
            initializeFirstBoardRow();

            for (int i = 1; i < i_Rows; i++)
            {
                for (int j = 0; j < i_Columns; j++)
                {
                    m_BoardButtons[i, j] = new Button();
                    m_BoardButtons[i, j].Top = m_BoardButtons[i - 1, j].Bottom + 8;
                    m_BoardButtons[i, j].Left = m_BoardButtons[i - 1, j].Left;
                    m_BoardButtons[i, j].Width = m_BoardButtons[i - 1, j].Width;
                    m_BoardButtons[i, j].Height = r_ButtonHeight;
                    Controls.Add(m_BoardButtons[i, j]);
                }
            }
            this.Height = m_BoardButtons[i_Rows - 1, i_Columns - 1].Bottom + 70;
        }

        private void initializeFirstBoardRow()
        {
            for (int i = 0; i < m_ColumnOfNumbers.Length; i++)
            {
                m_BoardButtons[0, i] = new Button();
                m_BoardButtons[0, i].Top = m_ColumnOfNumbers[i].Bottom + 8;
                m_BoardButtons[0, i].Left = m_ColumnOfNumbers[i].Left;
                m_BoardButtons[0, i].Width = m_ColumnOfNumbers[i].Width;
                m_BoardButtons[0, i].Height = r_ButtonHeight;
                Controls.Add(m_BoardButtons[0, i]);
            }
        }

        private void initializeLabels(int i_Column, int i_Rows)
        {
            m_Labels = new Label[2];

            m_Labels[0] = new Label();
            m_Labels[0].Text = string.Format("{0}: {1}",m_Names[0], 0);
            m_Labels[0].Top = m_BoardButtons[i_Rows - 1, i_Column - 1].Bottom + 15;
            m_Labels[0].Left = r_MarginBorder + (i_Column/3) * r_ButtonWidth;
            m_Labels[0].Width = 50;
            Controls.Add(m_Labels[0]);

            m_Labels[1] = new Label();
            m_Labels[1].Text = string.Format("{0}: {1}", m_Names[1], 0);
            m_Labels[1].Top = m_Labels[0].Top;
            m_Labels[1].Left = m_Labels[0].Left + (i_Column / 3) * r_ButtonWidth;
            m_Labels[1].Width = 50;
            Controls.Add(m_Labels[1]);

            this.Height = m_Labels[0].Bottom + 50;
        }

        private void integrateGame(Game i_Game)
        {
            i_Game.m_UpdateBoardSequence += setCoinToButton;
            i_Game.m_DisableButton += disableButton;
            i_Game.m_WinAlert += popupWinningMessage;
            i_Game.m_TieAlert += popupTieMessage;
        }

        private void setCoinToButton(Point i_Point, char i_CoinSymbol)
        {
            m_BoardButtons[i_Point.Row, i_Point.Column].Text = i_CoinSymbol.ToString();
        }

        private void disableButton(int i_Column)
        {
            m_ColumnOfNumbers[i_Column].Enabled = false;
        }

        private void popupWinningMessage(int i_Winner, int i_Score)
        {           
            string endingLine = string.Format(@"{0} has won.
Would you like another round?", m_Names[i_Winner]);
            var result = MessageBox.Show(endingLine, "Win", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);

            m_Labels[i_Winner].Text = string.Format("{0}: {1}", m_Names[i_Winner], i_Score);
            actToMessage(result);
        }

        private void popupTieMessage()
        {
            string endingLine = string.Format(@"Tie.
Would you like another round?");
            var result = MessageBox.Show(endingLine, "Tie", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);

            actToMessage(result);
        }

        private void actToMessage(DialogResult i_Result)
        {
            if (i_Result == DialogResult.Yes)
            {
                resetButtons();
            }
            else
            {
                Close();
            }
        }

        private void resetButtons()
        {
            for (int i = 0; i < r_Columns; i++)
            {
                for (int j = 0; j < r_Rows; j++)
                {
                    m_BoardButtons[j, i].Text = null;
                }
                m_ColumnOfNumbers[i].Enabled = true;
            }

        }

    }
}
