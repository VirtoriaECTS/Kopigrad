using System;
using System.Collections.Generic;

namespace Kopigrad.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string NameUser { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashPassworld { get; set; } = null!;
        public string Salt { get; set; } = null!;
    }
}
