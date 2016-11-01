using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuzinLines
{
    public partial class Form1 : Form
    {
        Board board;
        Graphics gr;
        Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(450, 450);
            pictureBox1.Image = bitmap;
            gr = Graphics.FromImage(bitmap);
            board = new Board();
            board.DrawFirst(gr);
            pictureBox1.Refresh();
            board.pb = pictureBox1;


            board.gr = gr;
        }

        private void newGameBtn_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(450, 450);
            pictureBox1.Image = bitmap;
            gr = Graphics.FromImage(bitmap);
            board = new Board();
            board.DrawFirst(gr);
            pictureBox1.Refresh();
            board.pb = pictureBox1;
            board.gr = gr;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(board.EmptyCells.Count == 0)
            {
                MessageBox.Show("Game Over");
            }
            MouseEventArgs newE = e as MouseEventArgs;            
            board.checkClick(new Point(newE.X, newE.Y));
            board.Draw(gr);
            pictureBox1.Refresh();
        }
    }
}
