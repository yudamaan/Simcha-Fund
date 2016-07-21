using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimchaFund.Models
{
    public class ContributeForm
    {
        public int ContributorId { get; set; }
        public double Contribution { get; set; }
        public bool Contribute { get; set; }
    }
}