using SQLite;

namespace LaQuiz.Items
{
    [Table("questions")]
    public class QuizItem
    {
        public QuizItem()
        {
            
        }

        [PrimaryKey, AutoIncrement]
        public int questionID { get; set;}

        public string body { get; set;}
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public int correct { get; set;}
        public int level { get; set; }
    }
}