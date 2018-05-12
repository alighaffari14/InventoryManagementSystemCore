using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class SalesOrderLine
    {
        [StringLength(38)]
        public string salesOrderLineId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Sales Order Line Name")]
        public string salesOrderLineName { get; set; }

        [StringLength(38)]
        [Display(Name = "Sales Order")]
        public string salesOrderId { get; set; }

        [Display(Name = "Sales Order")]
        public SalesOrder salesOrder { get; set; }
    }
}
