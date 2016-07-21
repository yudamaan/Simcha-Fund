using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Data;

namespace SimchaFund.Models
{
    public class ShowHistoryProperties
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; } 
    }
}