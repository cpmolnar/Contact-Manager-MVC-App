using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class Groups
    {
        public Groups()
        {

        }

        public Groups(SqlDataReader reader)
        {
            string groupOwner;
            string queryString = "SELECT DISTINCT dbo.AspNetUsers.UserName "
                            + "FROM dbo.AspNetGroupUsers "
                            + "JOIN dbo.AspNetGroups ON dbo.AspNetGroupUsers.GroupId=dbo.AspNetGroups.GroupId "
                            + "JOIN dbo.AspNetUsers ON dbo.AspNetGroups.UserId=dbo.AspNetUsers.Id "
                            + "WHERE dbo.AspNetGroupUsers.GroupId= '" + reader.GetInt32(0) + "'";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                r.Read();
                groupOwner = r.GetString(0);
                r.Close();
            }

            Group = new AspNetGroup()
            {
                GroupId = reader.GetInt32(0),
                UserId = groupOwner,
                GroupName = reader.GetString(2),
                StatusMessage = reader.GetString(3)
                
                
            };

            queryString = "SELECT dbo.AspNetUsers.UserName "
                            + "FROM dbo.AspNetGroupUsers "
                            + "JOIN dbo.AspNetUsers ON dbo.AspNetGroupUsers.UserId=dbo.AspNetUsers.Id "
                            + "WHERE dbo.AspNetGroupUsers.GroupId= '" + Group.GroupId + "'";
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                try
                {
                    while (r.Read())
                    {
                        Users.Add(new ApplicationUser(r));
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    r.Close();
                }
            }

            queryString = "SELECT DISTINCT dbo.AspNetContacts.* "
                            + "FROM dbo.AspNetGroupUsers "
                            + "JOIN dbo.AspNetGroups ON dbo.AspNetGroupUsers.GroupId=dbo.AspNetGroups.GroupId "
                            + "JOIN dbo.AspNetGroupContacts ON dbo.AspNetGroups.GroupId=dbo.AspNetGroupContacts.GroupId "
                            + "JOIN dbo.AspNetContacts ON dbo.AspNetGroupContacts.ContactId=dbo.AspNetContacts.ContactId "
                            + "WHERE dbo.AspNetGroupUsers.GroupId= '" + Group.GroupId + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                try
                {
                    while (r.Read())
                    {
                        Contacts.Add(new AspNetContact(r));
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    r.Close();
                }
            }
        }

        public AspNetGroup Group { get; set; }
        public List<AspNetContact> Contacts = new List<AspNetContact>();
        public List<ApplicationUser> Users = new List<ApplicationUser>();
    }
}