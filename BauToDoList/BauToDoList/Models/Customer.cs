using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class Customer : BaseEntity
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(100)]
        [DisplayName("Cari Adı")]
        public string Name { get; set; }


        [StringLength(100)]
        [DisplayName("E-Posta")]
        public string Email { get; set; }


        [StringLength(25)]
        [DisplayName("Telefon")]
        public string Phone { get; set; }


        [StringLength(25)]
        [DisplayName("Faks")]
        public string Fax { get; set; }


        [StringLength(100)]
        [DisplayName("Web Sayfası")]
        public string WebSite { get; set; }


        [StringLength(1000)]
        [DisplayName("Adres")]
        public string Address { get; set; }


        public virtual ICollection<ToDoItem> ToDoItems { get; set; }

    }
}