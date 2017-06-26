using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class AspNetGroupUser
    {
        private SqlDataReader reader;

        public AspNetGroupUser() { }
        public AspNetGroupUser(SqlDataReader reader)
        {

            GroupId = reader.GetInt32(0);
            UserId = reader.GetString(1);
        }

        /*public AspNetGroupUser(int groupId, string userId)
{
   GroupId = groupId;
   UserId = userId;
}*/

        [Key, Column(Order=0)]
        public int GroupId { get; set; }
        [Key, Column(Order=1)]
        public string UserId { get; set; }
    }
}