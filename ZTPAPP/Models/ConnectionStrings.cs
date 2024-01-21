namespace projekt.Models
{
    public class ConnectionStrings
    {
        private static ConnectionStrings instance;
        private Dictionary<string, string> connectionStringsDic = new Dictionary<string, string>();

        private ConnectionStrings()
        {
            connectionStringsDic.Add("Default", "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Projekt_ZTP;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        public static ConnectionStrings GetInstance()
        {
            if (instance == null)
            {
                instance = new ConnectionStrings();
            }
            return instance;
        }
        public string GetDefault()
        {
            if (connectionStringsDic.ContainsKey("Default"))
                return connectionStringsDic["Default"];

            return "";
        }
    }
}