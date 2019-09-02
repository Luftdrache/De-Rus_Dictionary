using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourthLab
{
    public partial class DailyWord : Form
    {
        bool isBorderVisible;
        Word todayWord;

        public int countWordsInDict; //сколько всего слов в словаре
        private Random rand = new Random();

        public DailyWord(int count)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            countWordsInDict = count;
            panel1.BringToFront();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

        }

        private void DailyWord_Load(object sender, EventArgs e)
        {
            randNewWord();
        }

        private void randNewWord()
        {
           if((this.Owner as Form1).dict.Count != 0)
                    todayWord = (this.Owner as Form1).dict[rand.Next(countWordsInDict)];
            {
                if (todayWord.Article.ToString() == "none")
                    label1.Text = todayWord.GermanWord;
                else
                    label1.Text = todayWord.Article + " " + todayWord.GermanWord;

                label2.Text = todayWord.RussianWord[0];
            }
            return;
        }

        //скрывать/открывать рамку окна по клику
        private void DailyWord_Click(object sender, EventArgs e)
        {
            isBorderVisible = !isBorderVisible;
            if (isBorderVisible)
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            else
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }


        private void DailyWord_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.Owner as Form1).Show();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Opacity = (double)trackBar1.Value / trackBar1.Maximum;  
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel2.Show();
        }



        private void DailyWord_MouseLeave(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if ((this.Owner as Form1).dict.Count != 0)
            {
                randNewWord();

            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isBorderVisible = !isBorderVisible;
            if (isBorderVisible)
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            else
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

        }


        //Для перетаскивания мышкой, когда нет рамки:
        private void DailyWord_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(this.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Capture = false;
            Message m = Message.Create(this.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            label2.Capture = false;
            Message m = Message.Create(this.Handle, 161, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
    }
}
