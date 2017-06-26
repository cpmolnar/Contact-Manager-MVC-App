using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class AspNetGroupContact
    {
        [Key]
        public int GroupId { get; set; }
        public int ContactId { get; set; }
    }
}