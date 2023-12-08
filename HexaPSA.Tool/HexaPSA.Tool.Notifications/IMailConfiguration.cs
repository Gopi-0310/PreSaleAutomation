
namespace HexaPSA.Tool.Notifications
{
    public interface IMailConfiguration
    {
        public string Host { get; }
        public int Port { get; }
        public bool UseSSL { get; }

        public string Name { get; }
        public string Username { get; }
        public string EmailAddress { get; }
        public string Password { get; }
        public string AppUrl { get; }
    }
}
