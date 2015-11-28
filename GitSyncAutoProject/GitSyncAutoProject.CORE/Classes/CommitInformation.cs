using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitSyncAutoProject.CORE;
using System.Net;
using System.IO;

namespace GitSyncAutoProject.CORE.Classes
{
    public class CommitInformation
    {
        //Properties
        public string sha { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }
}
