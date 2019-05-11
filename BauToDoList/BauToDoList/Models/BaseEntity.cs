using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }


        [DisplayName("Oluşturan Kişi")]
        [StringLength(50)]
        public string CreatedBy { get; set; }


        [DisplayName("Oluşturulma Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }


        [DisplayName("Düzenleyen Kişi")]
        [StringLength(50)]
        public string UpdatedBy { get; set; }


        [DisplayName("Düzenleme Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; }
    }
}