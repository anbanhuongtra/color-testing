using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameChonMau
{
    public partial class Form1 : Form
    {
      
        int point = 0;
        int miss = 0;
        int time = 15;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            RenderGrid();
        }

       
        

        public int GetGridLevel(int level)
        {
            if (level < 2) return 2;
            if (level < 4) return 3;
            if (level < 8) return 4;
            if (level < 13) return 5;
            if (level < 22) return 6;
            if (level < 32) return 7;
            if (level < 36) return 8;
            if (level < 40) return 9;
            if (level < 44) return 10;
            if (level < 48) return 11;
            return 12;
        }
        int level = 1;
        int correct_tile = -1;

        Random color_random = new Random();
        Random tile_random = new Random();

        public void RenderGrid()
        {
            int grid = GetGridLevel(level);

            //set position of correct tile
            int position = -1;
            do
            {
                position = tile_random.Next(0, grid * grid -1);
            } while (correct_tile == position);

            //set color 
            var colordiff = getLevelColorDiff(level);
            var r = color_random.Next(0, (255 - colordiff));
            var g = color_random.Next(0, (255 - colordiff));
            var b = color_random.Next(0, (255 - colordiff));

            //render grid
            for (int i = 0; i < grid * grid; i++)
            {
                Button tile = new Button();
                tile.Width = (flowLayoutPanel1.Width / grid) - 10; // error at higher level
                tile.Height = (flowLayoutPanel1.Height / grid) - 10; // error at higher level
                tile.Tag = i;
                
                if (i != position)
                {
                    tile.BackColor = Color.FromArgb(255, r, g, b);
                    tile.Click += tile_Click_miss;
                }
                else
                {
                    tile.BackColor = Color.FromArgb(255, r+colordiff, g+colordiff, b+colordiff);
                    tile.Click += tile_Click_correct;
                }
                flowLayoutPanel1.Controls.Add(tile);
            }

            


        }


      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        //tile click event
        void tile_Click_correct(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            level++;
            flowLayoutPanel1.Controls.Clear();
            RenderGrid();
            point++;
            lbPoint.Text = point.ToString();
            time = 16;
            timer1.Start();
            

        }

        void tile_Click_miss(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            miss++;
            lbMiss.Text = miss.ToString();
            time -= 3;

        }


        //  set difficult level
        int[] col = { 105, 75, 60, 45, 30, 20, 18, 16, 15, 15, 15, 14, 14, 14, 13, 13, 13, 12, 12, 12, 11, 11, 11, 10, 10, 9, 9, 8, 8, 7, 7, 7, 7, 6, 6, 6, 6, 5, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1 };
        public int getLevelColorDiff(int level)
        {
            if (level <= 58)
            {
                return col[level - 1];
            }
            return 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            lbTime.Text = time.ToString();

            if (time <= 0) { timer1.Stop(); lbTime.Text = "0"; }

        }
    }
}
