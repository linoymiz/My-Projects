using System;
using System.Windows.Forms;

namespace C18_Ex05
{
    class SettingForm : Form
    {
        PreGameParameters m_parameters;

        private Label m_PlayersTitle;
        private Label m_Player1Label;
        private Label m_BoardSizeLabel;
        private Label m_RowsLabel;
        private Label m_ColumnsLabel;
        private CheckBox m_Player2CheckBox;
        private TextBox m_Player1TextBox;
        private TextBox m_Player2TextBox;
        private NumericUpDown m_RowsNumericUpDown;
        private NumericUpDown m_ColsNumericUpDown;
        private Button m_StartButton;

        public SettingForm()
        {
            base.Text = "Game Settings";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initializePlayerTitle();
            initializePlayer1Label();
            initializePlayer1TextBox();
            initializePlayer2CheckBox();
            intializePlayer2TextBox();
            initializeBoardSizeLabel();
            initializeRowsLabel();
            initializeRowNumeric();
            initializeColumnsLabel();
            initializeColumnsNumeric();
            initializeStartButton();
            setForm();
        }

        private void initializePlayerTitle()
        {
            m_PlayersTitle = new System.Windows.Forms.Label();
            m_PlayersTitle.AutoSize = true;
            m_PlayersTitle.Location = new System.Drawing.Point(22, 21);
            m_PlayersTitle.Name = "playersTitle";
            m_PlayersTitle.Size = new System.Drawing.Size(59, 17);
            m_PlayersTitle.TabIndex = 0;
            m_PlayersTitle.Text = "Players:";
        }

        private void initializePlayer1Label()
        {
            m_Player1Label = new System.Windows.Forms.Label();
            m_Player1Label.AutoSize = true;
            m_Player1Label.Location = new System.Drawing.Point(41, 65);
            m_Player1Label.Name = "player1";
            m_Player1Label.Size = new System.Drawing.Size(64, 17);
            m_Player1Label.TabIndex = 1;
            m_Player1Label.Text = "Player 1:";
        }

        private void initializePlayer1TextBox()
        {
            m_Player1TextBox = new System.Windows.Forms.TextBox();
            m_Player1TextBox.Location = new System.Drawing.Point(162, 65);
            m_Player1TextBox.Name = "player1TextBox";
            m_Player1TextBox.Size = new System.Drawing.Size(115, 22);
            m_Player1TextBox.TabIndex = 7;
        }

        private void initializePlayer2CheckBox()
        {
            m_Player2CheckBox = new System.Windows.Forms.CheckBox();
            m_Player2CheckBox.AutoSize = true;
            m_Player2CheckBox.Location = new System.Drawing.Point(47, 106);
            m_Player2CheckBox.Name = "checkForSecondPlayer";
            m_Player2CheckBox.Size = new System.Drawing.Size(86, 21);
            m_Player2CheckBox.TabIndex = 6;
            m_Player2CheckBox.Text = "Player 2:";
            m_Player2CheckBox.UseVisualStyleBackColor = true;
            m_Player2CheckBox.Click += checkBoxPlayerTwo_CheckedChanged;
        }

        private void intializePlayer2TextBox()
        {
            m_Player2TextBox = new System.Windows.Forms.TextBox();
            m_Player2TextBox.Enabled = false;
            m_Player2TextBox.Location = new System.Drawing.Point(162, 106);
            m_Player2TextBox.Name = "player2TextBox";
            m_Player2TextBox.Size = new System.Drawing.Size(115, 22);
            m_Player2TextBox.TabIndex = 8;
        }

        private void initializeBoardSizeLabel()
        {
            m_BoardSizeLabel = new System.Windows.Forms.Label();
            m_BoardSizeLabel.AutoSize = true;
            m_BoardSizeLabel.Location = new System.Drawing.Point(22, 159);
            m_BoardSizeLabel.Name = "BoardSizeTitle";
            m_BoardSizeLabel.Size = new System.Drawing.Size(81, 17);
            m_BoardSizeLabel.TabIndex = 3;
            m_BoardSizeLabel.Text = "Board Size:";
        }

        private void initializeRowsLabel()
        {
            m_RowsLabel = new System.Windows.Forms.Label();
            m_RowsLabel.AutoSize = true;
            m_RowsLabel.Location = new System.Drawing.Point(41, 210);
            m_RowsLabel.Name = "rows";
            m_RowsLabel.Size = new System.Drawing.Size(46, 17);
            m_RowsLabel.TabIndex = 4;
            m_RowsLabel.Text = "Rows:";
            m_RowsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
        }

        private void initializeRowNumeric()
        {
            m_RowsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            m_RowsNumericUpDown.Location = new System.Drawing.Point(105, 210);
            m_RowsNumericUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            m_RowsNumericUpDown.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            m_RowsNumericUpDown.Name = "rowsNumericUpDown";
            m_RowsNumericUpDown.Size = new System.Drawing.Size(73, 22);
            m_RowsNumericUpDown.TabIndex = 9;
            m_RowsNumericUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            ((System.ComponentModel.ISupportInitialize)(this.m_RowsNumericUpDown)).BeginInit();
        }

        private void initializeColumnsLabel()
        {
            m_ColumnsLabel = new System.Windows.Forms.Label();
            m_ColumnsLabel.AutoSize = true;
            m_ColumnsLabel.Location = new System.Drawing.Point(213, 210);
            m_ColumnsLabel.Name = "cols";
            m_ColumnsLabel.Size = new System.Drawing.Size(39, 17);
            m_ColumnsLabel.TabIndex = 5;
            m_ColumnsLabel.Text = "Cols:";
        }

        private void initializeColumnsNumeric()
        {
            m_ColsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            m_ColsNumericUpDown.Location = new System.Drawing.Point(274, 210);
            m_ColsNumericUpDown.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            m_ColsNumericUpDown.Minimum = new decimal(new int[] { 4, 0, 0, 0 });
            m_ColsNumericUpDown.Name = "colsNumericUpDown";
            m_ColsNumericUpDown.Size = new System.Drawing.Size(71, 22);
            m_ColsNumericUpDown.TabIndex = 10;
            m_ColsNumericUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            ((System.ComponentModel.ISupportInitialize)(this.m_ColsNumericUpDown)).BeginInit();
        }

        private void initializeStartButton()
        {
            m_StartButton = new System.Windows.Forms.Button();
            m_StartButton.Location = new System.Drawing.Point(25, 260);
            m_StartButton.Name = "startButton";
            m_StartButton.Size = new System.Drawing.Size(320, 56);
            m_StartButton.TabIndex = 11;
            m_StartButton.Text = "Start!";
            m_StartButton.UseVisualStyleBackColor = true;
            m_StartButton.Click += StartGameButton_Clicked;
        }

        private void setForm()
        {
            ClientSize = new System.Drawing.Size(370, 339);
            Controls.Add(this.m_StartButton);
            Controls.Add(this.m_ColsNumericUpDown);
            Controls.Add(this.m_RowsNumericUpDown);
            Controls.Add(this.m_Player2TextBox);
            Controls.Add(this.m_Player1TextBox);
            Controls.Add(this.m_Player2CheckBox);
            Controls.Add(this.m_ColumnsLabel);
            Controls.Add(this.m_RowsLabel);
            Controls.Add(this.m_BoardSizeLabel);
            Controls.Add(this.m_Player1Label);
            Controls.Add(this.m_PlayersTitle);
            Name = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_RowsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ColsNumericUpDown)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void checkBoxPlayerTwo_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox PlayerTwo = sender as CheckBox;
            if (PlayerTwo.Checked)
            {
                m_Player2TextBox.Enabled = true;
            }
            else
            {
                m_Player2TextBox.Enabled = !true;
            }
        }

        private void StartGameButton_Clicked(object sender, EventArgs e)
        {
            string[] playerNames = { m_Player1TextBox.Text, m_Player2TextBox.Text };
            decimal[] dimensions = { m_RowsNumericUpDown.Value, m_ColsNumericUpDown.Value };
            m_parameters = new PreGameParameters(playerNames, !m_Player2CheckBox.Checked, dimensions);
            Close();
        }

        public PreGameParameters getParameters()
        {
            return m_parameters;
        }
    }
}
