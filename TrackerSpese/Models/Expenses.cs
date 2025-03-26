using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerSpese.Models
{
    internal class Expenses
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
