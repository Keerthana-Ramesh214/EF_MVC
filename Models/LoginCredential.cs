using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace EntityFramewor_MVC.Models
{
    public partial class LoginCredential
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [DisplayName("Password")]
        public string PassWord { get; set; }

        [NotMapped]
        [Compare("PassWord",ErrorMessage="Password and Compare Password mush match")]
        public  string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime? Dob { get; set; }
    }
}
