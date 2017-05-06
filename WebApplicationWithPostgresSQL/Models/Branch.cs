using System;
using System.Collections.Generic;

namespace WebApplicationWithPostgresSQL
{
    public partial class Branch
    {
        public Branch()
        {
            Staff = new HashSet<Staff>();
        }

        public int BranchNo { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Director { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
