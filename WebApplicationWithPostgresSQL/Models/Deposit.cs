using System;
using System.Collections.Generic;

namespace WebApplicationWithPostgresSQL
{
    public partial class Deposit
    {
        public int DepositNo { get; set; }
        public int Cash { get; set; }
        public double PercentPerYear { get; set; }
        public DateTime DateOfBegin { get; set; }
        public DateTime DateOfEnd { get; set; }
        public string Commentary { get; set; }
        public int DepStaffNo { get; set; }
        public int DepClientNo { get; set; }

        public virtual Client DepClientNoNavigation { get; set; }
        public virtual Staff DepStaffNoNavigation { get; set; }
    }
}
