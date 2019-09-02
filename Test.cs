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
    public partial class Test : Form
    {
        //Немецкое слово всегда находится в testingWords[0] 

        private List<Word> testingWords = new List<Word>();//список карточек для проверки
        private String[] translation = new string[4]; //перевод слов
        public int countWordsInDict; //сколько всего слов в словаре
        private Random rand = new Random();

        private Button[] buttons = new Button[4];

        int wrongAnswersCount = 0;
        int attempt = 0; //количество попыток


        //для визуализации прогресса:
        private int x = 20;
        private int y = 350;
        private int width = 20;
        private int height = 20;
        private List<Rectangle> rects = new List<Rectangle>();
        


        public Test(int count)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            countWordsInDict = count;

            //кнопки
            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
        }


        //При первой загрузке формы:
        private void Test_Load(object sender, EventArgs e)
        {
            if ((this.Owner as Form1).dict.Count > 3)
                startTest();
            else
            {//Если в словаре нет/мало слов
                button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled  = button5.Enabled = false;
            }
        }


        //Заполняем список слов для теста и пр. действия:
        private void startTest()
        {
            //y -= 25;
           // rects.Add(new Rectangle(x, y, width, height));
           // Refresh(); //перерисовка 


            button1.Text = button2.Text = button3.Text = button4.Text = "";//сброс предыдущих вариантов ответа

            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = true; //можно давать ответ

            button1.UseVisualStyleBackColor = button2.UseVisualStyleBackColor =
                button3.UseVisualStyleBackColor = button4.UseVisualStyleBackColor = true; //вернуть обычный кнопочный стиль

            button5.Enabled = false;//нельзя продолжить, пока не дан ответ

            //Наполняем список, повторы исключены
            Word newWord;

            testingWords.Add((this.Owner as Form1).dict[rand.Next(countWordsInDict)]); //заносится первое слово
            for (int i = 0; i < 3; i++) //оставшиеся три
            {
                newWord = (this.Owner as Form1).dict[rand.Next(countWordsInDict)];
                for (int j = 0; j < testingWords.Count; j++)
                {

                    if (newWord.GermanWord == testingWords[j].GermanWord)
                    {
                        newWord = (this.Owner as Form1).dict[rand.Next(countWordsInDict)];
                        j = -1;
                    }
                }
                testingWords.Add(newWord);
            }

            //Что переводить. Всегда первый элемент списка!
            if (testingWords[0].Article.ToString() == "none")
                label1.Text = testingWords[0].GermanWord;
            else
            label1.Text = testingWords[0].Article + " " + testingWords[0].GermanWord; 

            //распределение перевода по кнопкам(рандомно):        
            for (int i = 0; i < buttons.Length; i++)
            {
                int buttonNum = rand.Next(0, 4);
                if (buttons[buttonNum].Text == "")
                {
                    buttons[buttonNum].Text = testingWords[i].RussianWord[0];
                }
                else
                {
                    i--;
                }
            }

            label2.Text = "Количество ошибок: " + wrongAnswersCount + "/10";
        }

        //вспомогательная
        private void paintCircle()
        {
            y -= 25;
            rects.Add(new Rectangle(x, y, width, height));
            Refresh(); //перерисовка 
        }

        //вспомогательная
        private void Info()
        {
            label2.Text = "Количество ошибок: " + wrongAnswersCount + "/10";
            setAllButtonsInactive();
            if (attempt == 9)
            {
                button5.Text = "Завершить";
                button5.BackColor = Color.Gold;
            }
            button5.Enabled = true;
        }

        //Реакция на правильность ответов:
        private void button1_Click(object sender, EventArgs e)
        {
            paintCircle();
            if (button1.Text != testingWords[0].RussianWord[0]) //если ответ неверный
            {
                button1.BackColor = Color.FromArgb(204, 78, 92);
                wrongAnswersCount++;
                if (button2.Text == testingWords[0].RussianWord[0])
                    button2.BackColor = Color.SeaGreen;
                else if (button3.Text == testingWords[0].RussianWord[0])
                    button3.BackColor = Color.SeaGreen;
                else if (button4.Text == testingWords[0].RussianWord[0])
                    button4.BackColor = Color.SeaGreen;
            }
            else //если ответ верный
            {
                button1.BackColor = Color.SeaGreen;
            }
            Info();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paintCircle();
            if (button2.Text != testingWords[0].RussianWord[0])
            {
                button2.BackColor = Color.FromArgb(204, 78, 92);
                wrongAnswersCount++;
                if (button1.Text == testingWords[0].RussianWord[0])
                    button1.BackColor = Color.SeaGreen;
                else if (button3.Text == testingWords[0].RussianWord[0])
                    button3.BackColor = Color.SeaGreen;
                else if (button4.Text == testingWords[0].RussianWord[0])
                    button4.BackColor = Color.SeaGreen;
            }
            else
            {
                button2.BackColor = Color.SeaGreen;
            }
            Info();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            paintCircle();
            if (button3.Text != testingWords[0].RussianWord[0])
            {
                button3.BackColor = Color.FromArgb(204, 78, 92);
                wrongAnswersCount++;
                if (button1.Text == testingWords[0].RussianWord[0])
                    button1.BackColor = Color.SeaGreen;
                else if (button2.Text == testingWords[0].RussianWord[0])
                    button2.BackColor = Color.SeaGreen;
                else if (button4.Text == testingWords[0].RussianWord[0])
                    button4.BackColor = Color.SeaGreen;
            }
            else
            {
                button3.BackColor = Color.SeaGreen;
            }
            Info();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            paintCircle();
            if (button4.Text != testingWords[0].RussianWord[0])
            {
                button4.BackColor = Color.FromArgb(204, 78, 92);
                wrongAnswersCount++;
                if (button1.Text == testingWords[0].RussianWord[0])
                    button1.BackColor = Color.SeaGreen;
                else if (button2.Text == testingWords[0].RussianWord[0])
                    button2.BackColor = Color.SeaGreen;
                else if (button3.Text == testingWords[0].RussianWord[0])
                    button3.BackColor = Color.SeaGreen;
            }
            else
            {
                button4.BackColor = Color.SeaGreen;
            }
            Info();
        }

        //переход к следующему слову в списке:
        private void button5_Click(object sender, EventArgs e)
        {
            attempt++;
            if (attempt < 10)
            {
                testingWords.Clear(); //чтобы гоняло по всему словарю, а не по одним и тем же словам.
                startTest();

            }
            else 
            {

                this.Close();
            }
        }

        void setAllButtonsInactive() //чтобы кнопки не нажимались после ответа на вопрос
        {
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = false;
        }

        //кружочки:
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.White);

            g.DrawRectangle(pen, 15, 95, 30, 255);
            SolidBrush br = new SolidBrush(Color.Gold);
            for (int i = 0; i < rects.Count; i++)
            {
                g.FillEllipse(br, rects[i]);
            }
        }


    }
}

