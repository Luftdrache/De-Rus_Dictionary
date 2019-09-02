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
    public partial class Form2 : Form
    {
        private int textBoxNum; //чтобы понимать, в какой текстбокс ставить умлаут
        private string germanWord;
        private string russianWord;
        private string pluralForm;
        private article art;
        private partOfSpeech part;

        //focus№ = нужна ли перерисовка
        bool focus1 = false;
        bool focus2 = false;
        bool focus3 = false;
        bool isArticle = false;


        public Form2()
        {
            InitializeComponent();//Метод, который инициализирует все компоненты, расположенные на форме
            this.DoubleBuffered = true; //чтобы не мерцала картинка при перерисовке
            pluralForm = "-";

          //  comboBox1.Items.Add("-"); //если нет артикля
            for (int i = 1; i < Enum.GetValues(typeof(article)).Length; i++) //список артиклей
            {
                comboBox1.Items.Add((article)i);
            }
            for (int i = 0; i < Enum.GetValues(typeof(partOfSpeech)).Length; i++) //список частей речи
            {
                comboBox2.Items.Add((partOfSpeech)i);
            }

        }

        //Кнопка "Сохранить"
        private void button5_Click(object sender, EventArgs e)
        {
            germanWord = textBox1.Text;
            russianWord = textBox2.Text;
            pluralForm = textBox3.Text;
            if (comboBox1.SelectedItem != null && isArticle == true) //если есть артикль
            art = (article)comboBox1.SelectedItem;
            else art = article.none;

            part = (partOfSpeech)comboBox2.SelectedItem;

            Word w = new Word(germanWord, russianWord, pluralForm, art, part);
            (this.Owner as Form1).dict.Add(w);
            //  (this.Owner as Form1).clearListGermanWords();
            (this.Owner as Form1).bs.ResetBindings(false);

            (this.Owner as Form1).listView1_Reaction ();
            button5.Visible = false;
            button6.Visible = false;
            flowLayoutPanel2.Visible = true;

        }


        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //проверка на заполненость всех полей. Если заполнены - активировать кнопку "Сохранить"
        private void checkButtonEble()
        {
            if (comboBox1.Visible == true)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != ""
                    && comboBox2.Text != "")
                    button5.Enabled = true;
                else button5.Enabled = false;
            }
            else
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox2.Text != "")
                    button5.Enabled = true;
                else button5.Enabled = false;
            }
        }

        //добавить новое слово по клику на любом поле
        private void addAgain()
        {
            if(flowLayoutPanel2.Visible == true)
            {
                flowLayoutPanel2.Visible = false;
                button5.Visible = true;
                button6.Visible = true;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                comboBox2.Text = "";
                comboBox1.Visible = false;
                label4.Visible = false;
                button5.Enabled = false;
            }
        }

        #region Умлауты

        private void textBox1_Enter(object sender, EventArgs e)
        {
            addAgain();
            textBoxNum = 1;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            addAgain();
            textBoxNum = 2;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            addAgain();
            textBoxNum = 3;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxNum == 1)
                if (textBox1.Text == "")
                {
                    textBox1.Paste("Ä");
                    textBox1.Focus();
                }
                else
                {
                    textBox1.Paste("ä");
                    textBox1.Focus();
                }
            else if(textBoxNum ==2)
            {
                textBox2.Focus();
            }
            else if (textBoxNum == 3)
                if (textBox3.Text == "")
                {
                    textBox3.Paste("Ä");
                    textBox3.Focus();
                }
                else
                {
                    textBox3.Paste("ä");
                    textBox3.Focus();
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxNum == 1)
                if (textBox1.Text == "")
                {
                    textBox1.Paste("Ö");
                    textBox1.Focus();
                }
                else
                {
                    textBox1.Paste("ö");
                    textBox1.Focus();
                }
            else if (textBoxNum == 2)
            {
                textBox2.Focus();
            }
            else if (textBoxNum == 3)
                if (textBox3.Text == "")
                {
                    textBox3.Paste("Ö");
                    textBox3.Focus();
                }
                else
                {
                    textBox3.Paste("ö");
                    textBox3.Focus();
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxNum == 1)
                if (textBox1.Text == "")
                {
                    textBox1.Paste("Ü");
                    textBox1.Focus();
                }
                else
                {
                    textBox1.Paste("ü");
                    textBox1.Focus();
                }
            else if (textBoxNum == 2)
            {
                textBox2.Focus();
            }
            else if (textBoxNum == 3)
                if (textBox3.Text == "")
                {
                    textBox3.Paste("Ü");
                    textBox3.Focus();
                }
                else
                {
                    textBox3.Paste("ü");
                    textBox3.Focus();
                }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxNum == 1)
            {
                textBox1.Paste("ß");
                textBox1.Focus();
            }
            else if (textBoxNum == 2)
            {
                textBox2.Focus();
            }
            else if (textBoxNum == 3)
            {
                textBox3.Paste("ß");
                textBox3.Focus();
            }
        }
        #endregion

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //запрет ввода кириллицы и пр. в немецкое слово
            if ((e.KeyChar < 'A' || e.KeyChar > 'z') && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)  
            {
                focus1 = true;
                e.Handled = true;
            }
            else
             focus1 = false;
            Refresh();
        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) 
        {
            //Запрет ввода латиницы в переводе
            if ((e.KeyChar < 'А' || e.KeyChar > 'я') && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space
               && e.KeyChar != ',')
            {
                focus2 = true;
                e.Handled = true;
            }
            else
                focus2 = false;
            Refresh();
        }


        //запрет ввода кириллицы и пр. в немецкое слово
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 'A' || e.KeyChar > 'z') && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                focus3 = true;
                e.Handled = true;
            }
            else
                focus3 = false;
            Refresh();
        }




        // Form2_Load - событие перед первым отображением формы
        private void Form2_Load(object sender, EventArgs e) 
        {
            //кнопка "Сохранить" неактивна, пока не заполнены все поля
            button5.Enabled = false;
            //нет артикля по умолчанию
            art = article.der;
            textBox3.Text = "-";
            //comboBox1.Text = "-";
        }



        //раскраска полей при ошибочном вводе:
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (focus1)
            {
                textBox1.BorderStyle = BorderStyle.FixedSingle;
                textBox1.BackColor = Color.IndianRed;
            }
            else
            {
                textBox1.BorderStyle = BorderStyle.Fixed3D;
                textBox1.BackColor = Color.White;
            }
            if (focus2)
            {
                textBox2.BorderStyle = BorderStyle.FixedSingle;
                textBox2.BackColor = Color.IndianRed;
            }
            else 
            {
                textBox2.BorderStyle = BorderStyle.Fixed3D;
                textBox2.BackColor = Color.White;
            }
            if (focus3)
            {
                textBox3.BorderStyle = BorderStyle.FixedSingle;
                textBox3.BackColor = Color.IndianRed;
            }
            else 
            {
                textBox3.BorderStyle = BorderStyle.Fixed3D;
                textBox3.BackColor = Color.White;
            }

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           checkButtonEble();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkButtonEble();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkButtonEble();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            checkButtonEble();
        }


        //Если слово - существительное, то указать артикль:
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            checkButtonEble();
            if (comboBox2.SelectedItem.ToString() == "Substantiv")
            {
                comboBox1.Text = "der";
                isArticle = true;
                comboBox1.Visible = true;
                label4.Visible = true;
            }
            else
            {
                art = article.none;
                isArticle = false;
                comboBox1.Visible = false;
                label4.Visible = false;
                
            }
            
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            addAgain();
        }


    }
}
