using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class Category : BaseEntity
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage ="Bu alanı doldurmak zorunludur.")]
        [StringLength(200)]
        public string Name { get; set; }


        public virtual ICollection<ToDoItem> ToDoItems { get; set; }

    }
}