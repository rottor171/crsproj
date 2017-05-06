using System;
using System.Collections.Generic;

namespace WebApplicationWithPostgresSQL
{
    public partial class Staff
    {
        public Staff()
        {
            Deposit = new HashSet<Deposit>();
        }

        public int StaffNo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Salary { get; set; }
        public int StaffBranchNo { get; set; }

        public virtual ICollection<Deposit> Deposit { get; set; }
        public virtual Branch StaffBranchNoNavigation { get; set; }
    }
}
