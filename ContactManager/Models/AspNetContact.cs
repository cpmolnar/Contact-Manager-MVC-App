using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNet.Identity;

namespace ContactManager.Models
{
    public class AspNetContact
    {
        public AspNetContact()
        {

        }

        public AspNetContact(SqlDataReader reader)
        {
            ContactId = reader.GetInt32(0);
            Name = reader.GetString(1);
            Address = reader.GetString(2);
            City = reader.GetString(3);
            State = reader.GetString(4);
            Zip = reader.GetString(5);
            Email = reader.GetString(6);

            string queryString = "SELECT UserName "
                            + "FROM dbo.AspNetUsers "
                            + "WHERE Id = '" + reader.GetString(7) + "'";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            List<AspNetContact> columnData = new List<AspNetContact>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                r.Read();
                OwnerID = r.GetString(0);
                r.Close();
            }
        }

        [Key]
        public int ContactId { get; set; }

        // user ID from AspNetUser table
        public string OwnerID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}