using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_Usage")]
    public class Usage
    {
        [Key]
        public int Usage_ID { get; set; }

        [Required, StringLength(100, ErrorMessage = "Can be a maximum of 100 characters!")]
        public string Software_Key { get; set; }
        
        public DateTime Usage_Time { get; set; }

        
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime Acquisition_Date { get; set; }

        [Required]
        public DateTime Update_Start_Date { get; set; }

        
        public DateTime Update_Finish_Date { get; set; }
        

        public int D_ID { get; set; }
        public Department Department { get; set; }
        
        public int S_ID { get; set; }
        public Software Software { get; set; }
    }
}