using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization; //для бинарной
using System.Runtime.Serialization.Formatters.Binary;

namespace FourthLab
{
    public partial class Form1 : Form
    {
      public List<Word> dict = new List<Word>();
      public BindingSource bs = new BindingSource();
      public int index;
  
        public Form1()

        {
            InitializeComponent();

            try
            {
                string fullPath = Application.StartupPath.ToString() + "\\de_rus_dict.xml";
                Stream stream = new FileStream(fullPath, FileMode.Open);
                XmlSerializer xmlSer = new XmlSerializer(typeof(List<Word>));
                dict = (List<Word>)xmlSer.Deserialize(stream);
                stream.Close();
            } catch(IOException e)
            {
                Word w1 = new Word("Apfel", "яблоко", "Äpfel", article.der, partOfSpeech.Substantiv);
                Word w2 = new Word("Eichhörnchen", "белка", "Eichhörnchen", article.das, partOfSpeech.Substantiv);
                Word w3 = new Word("lesen", "читать", "-", article.none, partOfSpeech.Verb);
                Word w4 = new Word("Schmutz", "грязь", "Schmutzes", article.der, partOfSpeech.Substantiv);
                w4.RussianWord.Add("мусор");
                Word w5 = new Word("schnell", "быстрый", "-", article.none, partOfSpeech.Adjektiv);
                Word w6 = new Word("Sehenswürdigkeit", "достопримечательность", "Sehenswürdigkeiten", article.die, partOfSpeech.Substantiv);
                Word w7 = new Word("vor", "перед (во времени и в пространстве)", "-", article.none, partOfSpeech.Präposition);

                dict.Add(w1);
                dict.Add(w2);
                dict.Add(w3);
                dict.Add(w4);
                dict.Add(w5);
                dict.Add(w6);
                dict.Add(w7);
            }


            bs.DataSource = dict;
            listBox1.DataSource = bs;
            listBox1.DisplayMember = "GermanWord";
            
            listBox1.SelectedIndex = 0;//установка фокуса
            listBox1.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1_Reaction();
        }
      
        private void button8_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
              
                Form3 form3 = new Form3();
                form3.Owner = this;
                form3.ShowDialog();
            }
        }

        //удаление слов
        private void button_delete_word_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0) //если есть, что удалять
            {
                dict.Remove(dict[index]);
                // listBox1.Items.Clear();
                bs.ResetBindings(false);

                if (listBox1.Items.Count != 0)
                { 
                   // listBox1.SelectedIndex = 0;
                    listView1_Reaction();
                }
            } 
        }

        //вывод перевода по слову:
        public void listView1_Reaction()
        {
            listView1.Clear();
            string article = "";
            index = listBox1.SelectedIndex;

            if (index < 0) return;

            if (!dict[index].Article.ToString().Equals("none"))
            {
                article = dict[index].Article.ToString();
            }
            listView1.Items.Add(article + " " + dict[index].GermanWord).ForeColor = Color.DarkRed;
            listView1.Items.Add("");
            listView1.Items[0].Font = new Font("Times New Roman", 16F, FontStyle.Bold);

            for (int i = 0; i < dict[index].RussianWord.Count; i++)
            {
                if (dict[index].RussianWord.Count > 1)
                    listView1.Items.Add(i + 1 + ". " + dict[index].RussianWord[i]);
                else listView1.Items.Add("   " + dict[index].RussianWord[i]);
            }

            if (dict[index].PartOfSp.ToString().Equals("Substantiv"))
            {
                listView1.Items.Add("");
                listView1.Items.Add("       Мн. ч.: die " + dict[index].Plural).ForeColor = Color.DarkOrange;
            }
        }

        //Отправка статистики в Form4:
        private void button3_Click(object sender, EventArgs e)
        {
            int all = dict.Count;
            var subst = dict.Count(w => w.PartOfSp == partOfSpeech.Substantiv);
            var vrb = dict.Count(w => w.PartOfSp == partOfSpeech.Verb);
            var adj = dict.Count(w => w.PartOfSp == partOfSpeech.Adjektiv);
            var adv = dict.Count(w => w.PartOfSp == partOfSpeech.Adverb);
            var num = dict.Count(w => w.PartOfSp == partOfSpeech.Numerale);
            var prap = dict.Count(w => w.PartOfSp == partOfSpeech.Präposition);
            var pron = dict.Count(w => w.PartOfSp == partOfSpeech.Pronomen);

            Form4 form4 = new Form4(all, subst, vrb, adj, adv, num,  prap, pron);
            form4.Owner = this;
            form4.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        //поиск через comboBox1
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {

            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "GermanWord";
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            bs.ResetBindings(false);
        }



        private void button2_Click(object sender, EventArgs e)
        {          
            Test testform = new Test(dict.Count);
            testform.Owner = this;
            testform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DailyWord dwform = new DailyWord(dict.Count);
            dwform.Owner = this;        
            dwform.Show();
            Hide();

        }

        //Загрузить словарь:
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML Files (*.xml)| *.xml|Binary Files (*.bin) |*.bin";
            openFile.InitialDirectory = @"С:\";
            dict.Clear();
            if (openFile.ShowDialog() == DialogResult.OK)
            {

                string fileName = openFile.FileName;
                FileInfo info = new FileInfo(fileName);
                string extension = info.Extension;
                Stream stream = new FileStream(fileName, FileMode.Open);
                switch (extension)
                {
                    case ".xml":
                        XmlSerializer xmlSer = new XmlSerializer(typeof(List<Word>));
                        dict = (List<Word>)xmlSer.Deserialize(stream);

                        break;
                    case ".bin":
                        BinaryFormatter binFormatter = new BinaryFormatter();
                        dict = (List<Word>)binFormatter.Deserialize(stream);

                        break;
                }

                stream.Close();
                bs.DataSource = dict;
                listBox1.DataSource = bs;
                listBox1.DisplayMember = "GermanWord";
                listView1_Reaction();
            }
            else return;       
        }

        //Сохранить словарь (xml|binary):
        private void button7_Click(object sender, EventArgs e)
        {
            // string currentFolder = Directory.GetCurrentDirectory();
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML Files (*.xml)| *.xml|Binary Files (*.bin) |*.bin";
            saveFile.InitialDirectory = @"C:\";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
               
                string saveName = saveFile.FileName;
                FileInfo info = new FileInfo(saveName);
                string extension = info.Extension; //извлекает расширение
                Stream stream = new FileStream(saveName, FileMode.Create);

                switch (extension)
                {
                    case ".xml":
                       
                        XmlSerializer xmlSer = new XmlSerializer(typeof(List<Word>));
                        xmlSer.Serialize(stream, dict);
                       

                        break;
                    case ".bin":
                        BinaryFormatter binFormatter = new BinaryFormatter();
                        binFormatter.Serialize(stream, dict);
                        break;
                }
                stream.Close();
            }
            else return;
            
        }


    }
}
