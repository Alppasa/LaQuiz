using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace LaQuiz.Items
{
    public class SpielerItem
    {
        [PrimaryKey]
        public string SpielerName { get; set; }
        public string Score { get; set; }
    }
}
