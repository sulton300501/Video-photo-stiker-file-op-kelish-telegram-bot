
namespace WTelegram
{
    internal class Client
    {
        private Func<string, string?> getEnvironmentVariable;

        public Client(Func<string, string?> getEnvironmentVariable)
        {
            this.getEnvironmentVariable = getEnvironmentVariable;
        }
    }
}