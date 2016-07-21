using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Data;

namespace SimchaFund.Models
{
    public class SimchaViewModel
    {
        public IEnumerable<SimchaPageData> Simchas { get; set; }
    }
}