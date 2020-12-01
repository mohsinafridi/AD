using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace AD
{
    public class Program
    {
        static void Main(string[] args)
        {
            var users = ADUsers("asim.");
        }
        public static List<string> ADUsers(string filter)
        {
            if (string.IsNullOrEmpty(filter)) throw new ArgumentNullException(nameof(filter));

            List<string> users = new List<string>();


           DirectoryEntry de = new DirectoryEntry("LDAP://systemsltd.local");

            var ds = new DirectorySearcher(de, $"(&(objectClass=user)(anr={filter}))",new[] { "givenName", }) //attributes to load
            {
                SizeLimit = 100
            };

            var results = ds.FindAll();
            if (results.Count > 0)
            {
                foreach (SearchResult result in results)
                {
                    users.Add($"{result.Properties["givenName"][0]} ");
                }
            }           
            return users;
        }

    }

}
