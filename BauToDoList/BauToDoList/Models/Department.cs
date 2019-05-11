using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BauToDoList.Models
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(100)]
        [DisplayName("Departman Adı")]
        public string Name { get; set; }


        public virtual ICollection<ToDoItem> ToDoItems { get; set; }

    }
}