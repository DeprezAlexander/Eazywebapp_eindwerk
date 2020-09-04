using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models
{
    public enum TypeOfSite
    {
        Wordpress,
        Drupal,
        [Display(Name ="Html & Css")]
        htmlcss,
        Php
    }
}
