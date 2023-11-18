using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BT_wo_EF.Models
{
    public class ExpenseModel
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Amount { get; set;}

    }
    
}