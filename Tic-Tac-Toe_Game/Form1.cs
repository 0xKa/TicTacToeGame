using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        enum enWinner { Player1, Player2, Draw, GameInProgress }

        struct stGameStats
        {
            public enWinner Winner;
            public bool isGameOver;
            public short Moves;
        }

        bool isPlayer1Turn = true;
        stGameStats GameStats;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.White;

            Pen pen = new Pen(white, 10); 

            //Start and End Cap
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 610, 58, 610, 350);
            e.Graphics.DrawLine(pen, 465, 58, 465, 350);

            e.Graphics.DrawLine(pen, 350, 135, 720, 135);
            e.Graphics.DrawLine(pen, 350, 250, 720, 250);

        }

        void ChooseX(object sender)
        {
            ((PictureBox)sender).Tag = "X";
            ((PictureBox)sender).Image = Properties.Resources.x;
            isPlayer1Turn = false;
            lblTurnPlayer.Text = "Player 2";
        }
        void ChooseO(object sender)
        {
            ((PictureBox)sender).Tag = "O";
            ((PictureBox)sender).Image = Properties.Resources.o;
            isPlayer1Turn = true;
            lblTurnPlayer.Text = "Player 1";
        }

        void EndGame()
        {
            lblTurnPlayer.Text = "Game Over";

            switch (GameStats.Winner) 
            {
                case enWinner.Player1:
                    lblWinnerPlayer.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lblWinnerPlayer.Text = "Player 2";
                    break;

                default:
                    lblWinnerPlayer.Text = "Draw";
                    MessageBox.Show("It is a Draw!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
            }

            MessageBox.Show(lblWinnerPlayer.Text + " is The Winner!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public bool isWin(PictureBox pb1, PictureBox pb2, PictureBox pb3)
        {
            if (pb1.Tag.ToString() != "?" && pb1.Tag.ToString() == pb2.Tag.ToString() && pb1.Tag.ToString() == pb3.Tag.ToString())
            {
                pb1.BackColor = Color.White;
                pb2.BackColor = Color.White;
                pb3.BackColor = Color.White;

                if (pb1.Tag.ToString() == "X")
                {
                    GameStats.Winner = enWinner.Player1;
                    GameStats.isGameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStats.Winner = enWinner.Player2;
                    GameStats.isGameOver = true;
                    EndGame();
                    return true;
                }
            }

            GameStats.isGameOver = false;
            return false;
        }

        void CheckWinner()
        {
            //check rows
            if (isWin(pictureBox1, pictureBox2, pictureBox3))
                return;
            if (isWin(pictureBox4, pictureBox5, pictureBox6))
                return;
            if (isWin(pictureBox7, pictureBox8, pictureBox9))
                return;

            //check cols
            if (isWin(pictureBox1, pictureBox4, pictureBox7))
                return;
            if (isWin(pictureBox2, pictureBox5, pictureBox8))
                return;
            if (isWin(pictureBox3, pictureBox6, pictureBox9))
                return;

            //check diagonls
            if (isWin(pictureBox1, pictureBox5, pictureBox9))
                return;
            if (isWin(pictureBox3, pictureBox5, pictureBox7))
                return;
        }

        void PlayMove(object sender)
        {
            if (GameStats.isGameOver) return;

            if (((PictureBox)sender).Tag.ToString() == "?")
            {
                GameStats.Moves++;
                switch (isPlayer1Turn)
                {
                    case true:
                        ChooseX(sender);
                        CheckWinner();
                        break;
                        
                    case false:
                        ChooseO(sender);
                        CheckWinner();
                        break;

                }
            }
            else
            {
                MessageBox.Show("Wrong Choice, Choose Again.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStats.Moves == 9 && !GameStats.isGameOver)
            {
                GameStats.isGameOver = true;
                GameStats.Winner = enWinner.Draw;
                EndGame();
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PlayMove(sender);
        }

        void ResetPictureBox(PictureBox pb)
        {
            pb.Image = Properties.Resources.question_sign;
            pb.Tag = "?";
            pb.BackColor = Color.Transparent;
        }
        void RestartGame()
        {
            ResetPictureBox(pictureBox1);
            ResetPictureBox(pictureBox2);
            ResetPictureBox(pictureBox3);
            ResetPictureBox(pictureBox4);
            ResetPictureBox(pictureBox5);
            ResetPictureBox(pictureBox6);
            ResetPictureBox(pictureBox7);
            ResetPictureBox(pictureBox8);
            ResetPictureBox(pictureBox9);

            isPlayer1Turn = true;
            lblTurnPlayer.Text = "Player 1";
            GameStats.Moves = 0;
            GameStats.isGameOver = false;
            GameStats.Winner = enWinner.GameInProgress;
            lblWinnerPlayer.Text = "...";

        }
        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
