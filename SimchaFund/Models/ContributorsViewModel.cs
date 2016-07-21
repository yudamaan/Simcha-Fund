using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Data;
using SimchaFund.Models;

namespace SimchaFund.Models
{
    public class ContributorsViewModel
    {
        public IEnumerable<ContributorPageProperties> Contributer { get; set; }
        public double TotalBalance { get; set; }
        
    }
}