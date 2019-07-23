using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace netcore.Models.Crm
{
    public class Policy : INetcoreBasic
    {
        public Policy()
        {
            this.createdAt = DateTime.UtcNow;
        }

        [StringLength(38)]
        [Display(Name = "Policy Id")]
        public string policyId { get; set; }

        [StringLength(50)]
        [Display(Name = "Policy Name")]
        [Required]
        public string policyName { get; set; }
    }
}