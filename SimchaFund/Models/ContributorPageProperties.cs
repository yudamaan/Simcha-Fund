﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimchaFund.Data;

namespace SimchaFund.Models
{
    public class ContributorPageProperties
    {
        public Contributors Contributor { get; set; }
        public decimal Balance { get; set; }
    }
}