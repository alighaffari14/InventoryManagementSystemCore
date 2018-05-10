using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class TodoTask
    {
        public Guid todoTaskId { get; set; }
        [StringLength(50)]
        [Display(Name = "Task Name")]
        public string todoTaskName { get; set; }
        [StringLength(100)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Todo")]
        public Guid todoId { get; set; }
        [Display(Name = "Todo")]
        public Todo todo { get; set; }
    }
}
