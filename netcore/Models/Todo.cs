using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class Todo
    {
        public Guid todoId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Todo Name")]
        public string todoName { get; set; }
        [StringLength(100)]
        [Display(Name = "Todo Description")]
        public string description { get; set; }

        public ICollection<TodoTask> todoTasks { get; set; } = new HashSet<TodoTask>();
    }
}
