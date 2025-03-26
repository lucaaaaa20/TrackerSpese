using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerSpese.Models
{
    internal class ExpenseList
    {
        public List<Expenses> Expenses { get; set; }
        public Double TotalAmount {  get; set; }
    }
}
