using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimchaFund.Data;

namespace SimchaFund.Models
{
    public class ContributionPageProperties
    {
        public Contributors Contributer { get; set; }
        public decimal Balance { get; set; }
        public decimal AmountContributed { get; set; }
        public bool Contributed { get; set; }       
    }
}
