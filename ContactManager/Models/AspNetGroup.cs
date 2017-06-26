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
    public class AspNetGroup
    {
        public AspNetGroup()
        {

        }

        public AspNetGroup(SqlDataReader reader)
        {
            GroupId = reader.GetInt32(0);
            GroupName = reader.GetString(2);
            StatusMessage = reader.GetString(3);

            string queryString = "SELECT UserName "
                            + "FROM dbo.AspNetUsers "
                            + "WHERE Id = '" + reader.GetString(1) + "'";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            List<AspNetGroup> columnData = new List<AspNetGroup>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                r.Read();
                UserId = r.GetString(0);
                r.Close();
            }
        }

        [Key]
        public int GroupId { get; set; }
        public string UserId { get; set; }
        public string GroupName { get; set; }
        public string StatusMessage { get; set; }
    }
}