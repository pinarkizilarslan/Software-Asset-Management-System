using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YazilimVarlikYonetimSistemi.Models.Model
{
    [Table("tbl_User")]
    public class User
    {
        [Key]
        public int U_ID { get; set; }

        [DisplayName("Açıklama deneme (models.model.user.cs)")]
        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string U_Type { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string First_Name { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string Last_Name { get; set; }

        [Required, StringLength(50, ErrorMessage = "Can be a maximum of 50 characters!")]
        public string U_Email { get; set; }

        [Required, StringLength(20, ErrorMessage = "Can be a maximum of 20 characters!")]
        public string U_TelNo { get; set; }


        public ICollection<Dependent> Dependents { get; set; }
    }
}