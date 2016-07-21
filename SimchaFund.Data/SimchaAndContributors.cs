using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimchaFund.Data
{
   public class SimchaAndContributors
    {
        public int SimchaId { get; set; }
        public int ContributorId { get; set; }
        public decimal Contribution { get; set; }
        public DateTime Date { get; set; }
    }
}
