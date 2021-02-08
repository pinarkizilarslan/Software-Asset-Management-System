using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_Department")]    //veri tabanındaki ismi
    public class Department
    {
        [Key]
        public int D_ID { get; set; }

        [Required, StringLength(50,ErrorMessage = "Can be a maximum of 50 characters!")]
        public string D_Name { get; set; }

        [Required, StringLength(100, ErrorMessage = "Can be a maximum of 100 characters!")]
        public string D_Address { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string D_Email { get; set; }

        [Required, StringLength(20, ErrorMessage = "Can be a maximum of 20 characters!")]
        public string D_TelNo { get; set; }


        public ICollection<Dependent> Dependents { get; set; }

        public ICollection<Usage> Usage { get; set; }
    }
}