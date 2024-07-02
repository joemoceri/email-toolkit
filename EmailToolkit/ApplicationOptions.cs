namespace EmailToolkit
{
    public class ApplicationOptions
    {
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string ImapHost { get; set; }
        public int ImapPort { get; set; }
	    public string Pop3Host { get; set; }
        public int Pop3Port { get; set; }
        public string Pop3DownloadedMessagesPath { get; set; }
        public string ImapDownloadedMessagesPath { get; set; }
        public string MailServerUserName { get; set; }
        public string MailServerPassword  { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
