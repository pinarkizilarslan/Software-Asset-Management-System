using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_Dependent")]
    public class Dependent
    {
        [Key]
        public int Dependent_ID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }


        public int U_ID { get; set; }
        public User User { get; set; }
        
        public int D_ID { get; set; }
        public Department Department { get; set; }

    }
}