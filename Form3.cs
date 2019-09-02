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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (textBox1.Text != "")
            {
                (this.Owner as Form1).dict[(this.Owner as Form1).index].RussianWord.Add(textBox1.Text);
                textBox1.Text = "";
                button1.Visible = button2.Visible = label2.Visible = false;
                flowLayoutPanel1.Visible = true;
                this.ActiveControl = label1;
                (this.Owner as Form1).listView1_Reaction();
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
    
           
            if ((e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space
                && e.KeyChar != ',')
            {
               e.Handled = true;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
            button1.Visible = button2.Visible = label2.Visible = true;
            flowLayoutPanel1.Visible = false;
        }
    }
}
