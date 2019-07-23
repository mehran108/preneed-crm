﻿using netcore.Models.Invent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models.Crm
{
    public class Lead : INetcoreMasterChild, IBaseAddress
    {
        public Lead()
        {
            this.createdAt = DateTime.UtcNow;
            this.isQualified = false;
            this.isConverted = false;
        }

        [StringLength(38)]
        [Display(Name = "Lead Id")]
        public string leadId { get; set; }

        [StringLength(50)]
        [Display(Name = "Lead Name")]
        [Required]
        public string leadName { get; set; }

        [StringLength(50)]
        [Display(Name = "Description")]
        public string description { get; set; }

        //IBaseAddress
        [Display(Name = "Street Address 1")]
        [Required]
        [StringLength(50)]
        public string street1 { get; set; }

        [Display(Name = "Street Address 2")]
        [StringLength(50)]
        public string street2 { get; set; }

        [Display(Name = "City")]
        [StringLength(30)]
        public string city { get; set; }

        [Display(Name = "Country")]
        [StringLength(30)]
        public string country { get; set; }
        //IBaseAddress

        [Display(Name = "Is Qualified")]
        public bool isQualified { get; set; }

        [Display(Name = "Is Converted")]
        public bool isConverted { get; set; }

        [Display(Name = "Channel")]
        [StringLength(38)]
        public string channelId { get; set; }
        [Display(Name = "Channel")]
        public Channel channel { get; set; }
        
        [Display(Name = "Customer")]
        [StringLength(38)]
        public string customerId { get; set; }

        [Display(Name = "Account Executive")]
        [StringLength(38)]
        public string accountExecutiveId { get; set; }

        [Display(Name = "Account Executive")]
        public AccountExecutive accountExecutive { get; set; }

        [Display(Name = "Activities")]
        public List<LeadLine> leadLine { get; set; } = new List<LeadLine>();
        [Display(Name = "Zip Code")]
        [StringLength(30)]
        public string zipCode { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime birthDate { get; set; }
        [Display(Name = "State")]
        [StringLength(30)]
        public string state { get; set; }
    }
}
