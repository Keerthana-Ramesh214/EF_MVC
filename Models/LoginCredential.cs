﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFramewor_MVC.Models
{
    public partial class LoginCredential
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserId { get; set; }
    }
}
