using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Data;

namespace SimchaFund.Models
{
    public class SimchaPageData
    {
        public Simchas Simchas { get; set; }
        public int ContributorCount { get; set; }
        public int ContributedCount { get; set; }
        public decimal TotalContribution { get; set; }
    }
}