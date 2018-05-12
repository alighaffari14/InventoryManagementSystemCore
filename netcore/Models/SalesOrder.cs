using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class SalesOrder : INetcoreMasterChild
    {
        public SalesOrder()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        public string salesOrderId { get; set; }
        [StringLength(50)]
        [Display(Name = "Sales Order Name")]
        [Required]
        public string salesOrderName { get; set; }

        public List<SalesOrderLine> salesOrderLine { get; set; } = new List<SalesOrderLine>();
    }
}
