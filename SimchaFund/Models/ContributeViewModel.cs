using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Models;
using SimchaFund.Data;

namespace SimchaFund.Models
{
    public class ContributeViewModel
    {
        public Simchas Simcha { get; set; }
        public IEnumerable<ContributionPageProperties> Contributors { get; set; }
    }
}