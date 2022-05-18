using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{
    public partial class Form1 : Form
    {

        int pipeSpeed = 8;
        int gravity = 10;
        int score = 0;
        bool spacePressed = false;
        bool scoreIncrement = false;
        bool pipeMovement = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            FlappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score: " + score;

            if((pipeBottom.Right < 70 || pipeTop.Right < 70) && !scoreIncrement)
            {
                scoreIncrement = true;
                score++;
            }

            if(pipeBottom.Left < -150)
            {
                scoreIncrement = false;
                pipeBottom.Left = 600; 
                pipeTop.Left = 600;
                //Range for bottom 250 (highest) to 420 (lowest) // diff 85
                //Range for top is -220 (highest) to -80 (lowest) // diff 70

                Random rand = new Random();
                double numBottom = rand.Next(85);
                double numTop = numBottom * 70 / 85;

                // 335 and -150 is the starting top point for pipeBottom and pipeTop respectively
                if (pipeMovement)
                {
                    pipeBottom.Top = (int)numBottom + 335;
                    pipeTop.Top = (int)numTop - 150;
                }
                else
                {
                    pipeBottom.Top = 335 - (int)numBottom;
                    pipeTop.Top = -150 - (int)numTop;
                }
                pipeMovement = !pipeMovement;
            }

            if (FlappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) || 
                FlappyBird.Bounds.IntersectsWith(pipeTop.Bounds) || 
                FlappyBird.Bounds.IntersectsWith(Ground.Bounds) ||
                FlappyBird.Top < -25
                )
            {
                endGame();
            }

            if(score > 5)
            {
                pipeSpeed = 12;
            }

        }

        private void gameKeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
        }

        private void gameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (spacePressed == false)
                {
                    spacePressed = true;
                    gameTimer.Enabled = true;
                }
                if (gameTimer.Enabled == false)
                {
                    return;
                }
                gravity = -10;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over!";
        }
    }
}
