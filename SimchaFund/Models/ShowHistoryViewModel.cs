using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Data;



namespace SimchaFund.Models
{
    public class ShowHistoryViewModel
    {
        public string ContributorName { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<ShowHistoryProperties> History { get; set; }       
    }
}