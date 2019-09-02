using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace FourthLab
{
    
    public enum article { none, der, die, das }; //род 
    public enum partOfSpeech { Substantiv, Verb, Adjektiv, Adverb, Numerale, Präposition, Pronomen };

    [Serializable]
   public class Word
    { 
        private string german;
        private List<string> russian = new List<string>(); //м.б. несколько значений перевода
        private string plural; //множественная форма числа 
        private article art;// артикль
        private partOfSpeech partOfSp; //часть речи
        private bool ShownAsDailyWord; //показывать как слово дня или нет
        public static int countShownWords;//сколько слов для показа


        public Word() //пустой для сериализации xml
        {

        }

        public Word (string german, string russian, string plural, article art, partOfSpeech partOfSp)
        {
           this.german = german;
           this.russian.Add(russian);
           this.plural = plural;
           this.art = art;
           this.PartOfSp = partOfSp;
           ShownAsDailyWord = true;
            countShownWords++;
        }


        public bool isShownAsDailyWord
        {
            get { return ShownAsDailyWord; }
            set { ShownAsDailyWord = value; }
        }
        public string GermanWord
        {
            get { return german; }
            set { german = value; }
        }

        public List<string> RussianWord
        {
            get { return russian; }
            set { russian = value; }
        }

        public string Plural
        {
            get { return plural; }
            set { plural = value; }
        }
  

        public article Article
        {
            get { return art; }
            set { art = value; }
        }

        public partOfSpeech PartOfSp
        {
            get { return partOfSp; }
            set { partOfSp = value; }
        }
    }
}
