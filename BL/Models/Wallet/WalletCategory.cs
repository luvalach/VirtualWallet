﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class WalletCategory : IDto
    {
        public int Id { get; set; }

        public int WalletId { get; set; }
        
        public Wallet Wallet { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
}
