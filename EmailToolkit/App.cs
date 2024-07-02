using EmailToolkit.Google;
using MailKit.Search;

namespace EmailToolkit
{
    public class App
    {
        private readonly IGoogleClient googleClient;

        public App(IGoogleClient googleClient)
        {
            this.googleClient = googleClient;
        }

        public void Run()
        {
            var authType = AuthType.AppPassword;
            var recent = false;
            var deleteMessages = false;

            //// pop3

            //// app password
            //var messages = googleClient.GetPop3Messages(authType, recent, deleteMessages);

            //// oauth
            //authType = AuthType.OAuth;
            //var messages = googleClient.GetPop3Messages(authType, recent, deleteMessages);

            //// imap

            //// app password
            //authType = AuthType.AppPassword;
            //var messages = googleClient.GetImapMessages(authType, recent, deleteMessages);

            //// oauth
            //authType = AuthType.OAuth;
            //var messages = googleClient.GetImapMessages(authType, recent, deleteMessages);

            //// use an imap listener to listen for when new messages arrive and handle them immediately
            //googleClient.ActivateImapListener(authType, CountChangedCallback);

            //foreach (var message in messages)
            //{
            //    Console.WriteLine($"{message.Key}: {message.Value.Subject}");
            //}

            Console.Read();
        }


        void CountChangedCallback(object sender, EventArgs e, AuthType authType)
        {
            CheckMailbox(authType);
        }

        private void CheckMailbox(AuthType authType)
        {
            try
            {
                // Once the count has changed, get all messages in a separate imap client
                var searchQuery = SearchQuery.All;
                var newImapClient = googleClient.GetImapClient(authType, false);

                var messages = newImapClient.Inbox.Search(searchQuery);

                foreach (var message in messages)
                {
                    // whatever you want to do
                }

                newImapClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // keep listening for new mailboxes
            }
        }
    }
}