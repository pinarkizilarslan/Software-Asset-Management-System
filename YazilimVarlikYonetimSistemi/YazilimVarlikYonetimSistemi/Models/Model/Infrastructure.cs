using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_Infrastructure")]
    public class Infrastructure
    {
        [Key]
        public int I_ID { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string OS { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string Min_RAM { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string Min_Storeage { get; set; }


        public int S_ID { get; set; }
        public Software Software { get; set; }

       
    }
}