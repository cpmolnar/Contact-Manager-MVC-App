using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class AspNetGroupContact
    {
        public AspNetGroupContact() { }

        public AspNetGroupContact(int groupId, int contactId)
        {
            GroupId = groupId;
            ContactId = contactId;
        }

        [Key, Column(Order = 0)]
        public int GroupId { get; set; }
        [Key, Column(Order = 1)]
        public int ContactId { get; set; }
    }
}