using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Net;
using Octokit;

namespace GitSyncAutoProject.CORE.ServiceClasses
{
    public class GithubService
    {
        //Not Working
        public void LoginOAuth()
        {
            string GithubClientID = "0968687a1c7f3d919fa3";
            string GithubClientSecret = "f5cf634e8f009052815a37baf5b86871a0f5554e";
            var client = new GitHubClient(new ProductHeaderValue("GitSyncAutoProject"));

            // NOTE: user must be navigated to this URL
            //  var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);

        }

        public static UserInformation GetGithubUser(string username)
        {

            //Validate user input
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Instatize UserInforamtion
                    UserInformation myInfo = new UserInformation();

                    //Say hello to GitHub
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    //Grab userInformation file from Github
                    string json = webClient.DownloadString("https://api.github.com/users/" + username);

                    //Convert JSON to user-able variables
                    myInfo = JsonConvert.DeserializeObject<UserInformation>(json);
                    

                    return myInfo;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("SUser could not be found.  Please check the username and try again.");
                return null;
            }
        } //End Get User Info Method

    }
}
