using System;
using System.Collections.Generic;

namespace WebApplicationWithPostgresSQL
{
    public partial class Client
    {
        public Client()
        {
            Deposit = new HashSet<Deposit>();
        }

        public int ClientNo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Deposit> Deposit { get; set; }
    }
}
