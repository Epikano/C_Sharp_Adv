using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class AccountDB
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public int Days { get; set; }
        public decimal CurrentSum { get; set; }
    }
}
