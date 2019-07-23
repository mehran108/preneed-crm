using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace netcore.Models.Crm
{
    public class OpportunityService : INetcoreBasic
    {
        public OpportunityService()
        {
            this.createdAt = DateTime.UtcNow;
        }
        [Key]
        [StringLength(38)]
        [Display(Name = "Service Id")]
        public string serviceId { get; set; }

        [StringLength(50)]
        [Display(Name = "Service Name")]
        [Required]
        public string serviceName { get; set; }
    }
}
