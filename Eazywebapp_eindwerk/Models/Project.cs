﻿using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models
{
    public class Project
    {
        //PROJECT ID
        [Key]
        public int ProjectID { get; set; }

        //PROJECT NAME
        [Required(ErrorMessage = "Project naam is verplicht.")]
        [Display(Name = "Project naam")]
        public string Name { get; set; }

        //PROJECT IMAGE
        [Display(Name = "Project afbeelding")]
        public string ProjectImage { get; set; }

        //PROJECT DESCRIPTION
        [Required(ErrorMessage = "Beschrijving is verplicht.")]
        [Display(Name = "Beschrijving")]
        public string ProjectDescription { get; set; }

        //START DATE
        [Display(Name = "Start datum")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        //END DATE
        [Display(Name = "Eind datum")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        //HOSTING
        [Display(Name = "Hosting in-house (J/N)")]   
        public bool Hosting { get; set; }

        //HOSTING PRICE 
        [Display(Name = "Hosting prijs (EUR)")]
        public double HostingPrice { get; set; }

        //RENEWAL DATE
        [Display(Name = "Verval datum hosting")]
        [DataType(DataType.Date)]
        public DateTime RenewalDate { get; set; }

        //OFFERTE PRICE
        [Display(Name = "Prijs offerte")]
        public double OffertePrice { get; set; }

        //TIME SPENT 
        [Display(Name = "Tijdsduur (U)")]
        public int TimeSpent { get; set; }

        //URL
        [Display(Name = "URL")]
        public string url { get; set; }

        //IS ACTIVE
        [Display(Name = "Actief (J/N)")]
        public bool IsActive { get; set; }

        //ADDITIONAL FEES (tekstvak)
        [Display(Name = "Bijkomende kosten")]
        public string AdditionalFees { get; set; }

        //TYPE OF SITE 
        [Display(Name = "Type site")]
        public string TypeOfSite { get; set; }

        public int ClientID { get; set; }

        //FOREIGN KEYS

        //CLIENTID
        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
    }
}
