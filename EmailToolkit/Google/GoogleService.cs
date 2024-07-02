using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using MailKit.Security;

namespace EmailToolkit.Google
{
    public interface IGoogleService
    {
        SaslMechanismOAuth2 GetOAuthCredentials(string username, string clientId, string clientSecret);
    }

    public class GoogleService : IGoogleService
    {
        public SaslMechanismOAuth2 GetOAuthCredentials(string username, string clientId, string clientSecret)
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            };

            // we need access to gmail so set the scope for it
            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore("CredentialCacheFolder", false),
                Scopes = new[] { "https://mail.google.com/" },
                ClientSecrets = clientSecrets
            });

            var codeReceiver = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);

            // authorize with the username and client secrets
            var credential = authCode.AuthorizeAsync(username, CancellationToken.None).Result;

            // if it's stale, go ahead and refresh it
            if (credential.Token.IsStale)
            {
                credential.RefreshTokenAsync(CancellationToken.None).ConfigureAwait(false);
            }

            // create the credentials
            var credentials = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);

            return credentials;
        }
    }
}
