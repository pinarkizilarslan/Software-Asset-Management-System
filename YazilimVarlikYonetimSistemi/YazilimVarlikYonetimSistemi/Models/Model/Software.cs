using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_Software")]
    public class Software
    {
        [Key]
        public int S_ID { get; set; }

        [Required, StringLength(100, ErrorMessage = "Can be a maximum of 100 characters!")]
        public string S_Name { get; set; }

        [Required]
        public int Agreement_ID { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string License_Type { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string License_Option { get; set; }

        [Required]
        public decimal Software_Cost { get; set; }

        [Required]
        public decimal Maintenance_Cost { get; set; }

        [Required]
        public bool In_House { get; set; }

        [Required]
        public bool Open_Source_Code { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string S_Version { get; set; }
        

        public int I_ID { get; set; }
        public Infrastructure Infrastructure { get; set; }
        
        public ICollection<Usage> Usage { get; set; }
    }
}