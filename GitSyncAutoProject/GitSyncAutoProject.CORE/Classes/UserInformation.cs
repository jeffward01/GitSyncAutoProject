using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSyncAutoProject.CORE
{
   public class UserInformation
    {
        //constructor
        public UserInformation()
        {
        }
        
        //Properties
        public string login { get; set; }
        public string name { get; set; }
        public decimal id { get; set; }
        public string html_url { get; set; }
        public string avatar_url { get; set; }
        public string repos_url { get; set; }
        public string followers_url { get; set; }
        public string followers { get; set; }
        public string following { get; set; }
        public string location { get; set; }
        public string public_repos { get; set; }
        public DateTime created_at { get; set; }


    }
}
