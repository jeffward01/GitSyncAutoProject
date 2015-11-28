using System;
using CSharp.GitHub.Api.Impl;
using CSharp.GitHub.Api.Interfaces;
using Spring.Social.OAuth2;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSyncAutoProject.CORE.ServiceClasses
{

    /// <summary>
    /// GitHub <see cref="IServiceProvider"/> implementation.
    /// </summary>
    /// <author>Scott Smith</author>
    public class GitHubServiceProvider : AbstractOAuth2ServiceProvider<IGitHub>
    {
        /// <summary>
        /// Creates a new instance of <see cref="GitHubServiceProvider"/>.
        /// </summary>
        /// <param name="clientId">The application's API client id.</param>
        /// <param name="clientSecret">The application's API client secret.</param>
        public  GitHubServiceProvider(String clientId, String clientSecret)
            : base(new OAuth2Template(clientId, clientSecret,
                "https://github.com/login/oauth/authorize",
                "https://github.com/login/oauth/access_token"))
        {
        }

        /// <summary>
        /// Returns an API interface allowing the client application to access unprotected resources.
        /// </summary>
        /// <returns>A binding to the service provider's API.</returns>
        public IGitHub GetApi()
        {
            return new GitHubTemplate();
        }

        /// <summary>
        /// Returns an API interface allowing the client application to access protected resources on behalf of a user.
        /// </summary>
        /// <param name="accessToken">The API access token.</param>
        /// <returns>A binding to the service provider's API.</returns>
        public override IGitHub GetApi(string accessToken)
        {
            return new GitHubTemplate(accessToken);
        }
    }
}
