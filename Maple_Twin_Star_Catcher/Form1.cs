using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace magnifier
{
    public partial class Form1 : Form
    {
        int moniter_width;
        int moniter_height;
        int box_width = 0;
        int box_height = 0;
        double rate_width = 1.0f;
        double rate_height = 1.0f;
        int SIZE_W = 100;
        int SIZE_H = 100;

        Size Capture_size;
        Size Copy_size;

        Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();

            button2_Click(null, null);

        }

       

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '`')
            {
                int mouse_x = MousePosition.X;
                int mouse_y = MousePosition.Y;


                Bitmap Capture_bitmap = new Bitmap(moniter_width, moniter_height);
                Graphics graphics = Graphics.FromImage(Capture_bitmap);

                graphics.CopyFromScreen(0, 0, 0, 0, Capture_size, CopyPixelOperation.SourceCopy);

                Size resize = Copy_size;
                Bitmap resizeImage = new Bitmap(Capture_bitmap, resize);

                int x = (mouse_x - SIZE_W / 2) * box_width / moniter_width;
                if (x < 0)
                    x = 0;
                int y = (mouse_y - SIZE_H / 2) * box_height / moniter_height;
                if (y < 0)
                    y = 0;

                for (int i = x; i < x + SIZE_W && i < Copy_size.Width; i++)
                {
                    for (int j = y; j < y + SIZE_H && j < Copy_size.Height; j++)
                    {
                        bitmap.SetPixel(i, j, resizeImage.GetPixel(i, j));
                    }
                }


                pictureBox1.Image = bitmap;



            }
            else if (e.KeyChar == 27)
            {
                button2_Click(null, null);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            moniter_width = Screen.PrimaryScreen.Bounds.Width; 
            moniter_height = Screen.PrimaryScreen.Bounds.Height; 

            box_width = pictureBox1.Width;
            box_height = pictureBox1.Height;

            rate_width = (double)box_width / moniter_width;
            rate_height = (double)box_height / moniter_height;

            Capture_size = new Size(moniter_width, moniter_height);


            Copy_size = new Size(box_width, box_height);

            bitmap = new Bitmap(box_width, box_height);


            for (int i = 0; i < box_width; i++)
            {
                for (int j = 0; j < box_height; j++)
                {
                    bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
            }
            pictureBox1.Image = bitmap;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))   
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))   
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SIZE_W = Convert.ToInt32(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SIZE_H = Convert.ToInt32(textBox2.Text);
        }


    }
}