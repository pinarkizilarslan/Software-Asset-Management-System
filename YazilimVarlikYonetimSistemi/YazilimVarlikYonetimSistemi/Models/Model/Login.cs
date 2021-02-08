using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_Login")]
    public class Login
    {
        [Key]
        public int Login_ID { get; set; }

        [Required, StringLength(100, ErrorMessage = "Can be a maximum of 100 characters!")]
        public string L_Email { get; set; }

        [Required, StringLength(100, ErrorMessage = "Can be a maximum of 100 characters!")]
        public string L_Password { get; set; }        
    }
}