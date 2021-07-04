using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        int cols = 50, rows = 25, score = 0, dx = 0, dy = 0, front = 0, back = 0;
        
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        Piece[] snake = new Piece[1250];
        List<int> available = new List<int>();
        bool[,] visit;

        Random rand = new Random();

        public Form1()
        {
           
            



            InitializeComponent();
            Intial();
            launchTimer();
        }

        private void launchTimer()
        {
            timer1.Interval = 100;
            timer1.Tick += move;
            timer1.Start();
        }

        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            dx = dy = 0;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    dx = 20;
                    break;
                case Keys.Left:
                    dx = -20;
                    break;
                case Keys.Up:
                    dy = -20;
                    break;
                case Keys.Down:
                    dy = 20;
                    break;
            }
            if (e.KeyCode == Keys.Q)
            {
                Environment.Exit(0);
            }
            if(e.KeyCode == Keys.F)
            {
                Application.Restart();
            }
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Environment.Exit(0);
        }

        private void labelScore_Click(object sender, EventArgs e)
        {

        }

        private void move(object sender, EventArgs e)
        {
            int x = snake[front].Location.X, y = snake[front].Location.Y;
                 if (dx == 0 && dy == 0) return;
                 if(game_over(x + dx, y + dy))
                  {
                         timer1.Stop();
                         MessageBox.Show("Game Over");
                label1.Visible = true;
                   return;
                  }
                 if(collisionFood(x + dx, y + dy))
            {
                        score += 1;
                        labelScore.Text = "Score:" + score.ToString();

                   if(hits((y + dy) / 20, (x + dx) / 20)) return;
                  Piece head = new Piece(x + dx, y + dy);
                      front = (front - 1 + 1000) % 1000;
                         snake[front] = head;
                          visit[head.Location.Y / 20, head.Location.X / 20] = true;
                          Controls.Add(head);
                RandomFood();
            }
            else
            {
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                   visit[snake[back].Location.Y / 20, snake[back].Location.X / 20] = false;
                  front = (front - 1 + 1000) % 1000;
                  snake[front] = snake[back];
                  snake[front].Location = new Point(x + dx, y + dy);
                  back = (back - 1 + 1000) % 1000;
                  visit[(y + dy) / 20, (x + dx) / 20] = true;

            }
        }

        private void RandomFood()
        {
            available.Clear();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if(!visit[i, j])
                    {
                        available.Add(i * cols + j);
                    }

                }
                int idx = rand.Next(available.Count) % available.Count;
                    labelFood.Left = (available[idx] * 20) % Width;
                 labelFood.Top = (available[idx] * 20) / Width * 20;

            }
        }

        private bool hits(int x, int y)
        {
            if(visit[x, y])
            {
                timer1.Stop();
                MessageBox.Show("Game Over");
                return true;
            }
            return false;
        }

        private bool collisionFood(int x, int y)
        {
            return x == labelFood.Location.X && y == labelFood.Location.Y;
        }

        private bool game_over(int x, int y)
        {
            return x < 0 || y < 0 || x > 980 || y > 480;
        }

        private void Intial()
        {
            visit = new bool[rows, cols];
            Piece head 
                = new Piece((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);
             labelFood.Location 
                = new Point((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    visit[i, j] = false;
                    available.Add(i * cols + j);

                }
                visit[head.Location.Y / 20, head.Location.X / 20] = true;
                available.Remove(head.Location.Y / 20 * cols + head.Location.X / 20);
                Controls.Add(head); snake[front] = head;
            }
        }

           
        
    }
}
