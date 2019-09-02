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
    public partial class Form4 : Form
    {
        public Form4(int all, int subst, int verb, int adj, int adv, int num, int prap, int pron)
        {
            InitializeComponent();
            listView1.Items.Add("СТАТИСТИКА:");
            listView1.Items.Add("");
            listView1.Items.Add("Общее количество слов: " + all);
            listView1.Items.Add("Имен существительных: " + subst);
            listView1.Items.Add("Глаголов: " + verb);
            listView1.Items.Add("Имен прилагательных: " + adj);
            listView1.Items.Add("Наречий: " + adv);
            listView1.Items.Add("Предлогов: " + prap);
            listView1.Items.Add("Местоимений: " + pron);
            listView1.Items.Add("Числительных: " + num);
        }
    }
}
