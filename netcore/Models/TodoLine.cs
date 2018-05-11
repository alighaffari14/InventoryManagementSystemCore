using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class TodoLine : INetcoreBasic
    {
        public TodoLine()
        {
            this.todoLineId = Guid.NewGuid().ToString();
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        public string todoLineId { get; set; }
        [StringLength(50)]
        [Display(Name = "Todo Line Name")]
        public string todoLineName { get; set; }
        [StringLength(100)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Todo")]
        [StringLength(38)]
        public string todoId { get; set; }
        [Display(Name = "Todo")]
        public Todo todo { get; set; }
    }
}
