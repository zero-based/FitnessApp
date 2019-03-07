namespace FitnessApp.SQLserver
{
    public static class ServerDetails
    {

        // Enter Server Name and Database Name Here
        public const string server = @"server_name_here";
        public const string database = "FITNESSAPP";
        public const string security = "SSPI";

        // Initialize Connection string
        public static string ConnectionString
        {
            get { return "data source = " + server + "; " + "database = " + database + "; " + "integrated security = " + security; }
            set { ConnectionString = value; }
        }

    }
}
