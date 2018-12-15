namespace FitnessApp.SQLdatabase
{
    public static class ServerDetails
    {

        // Enter Server Name and Database Name Here
        public const string server = @"Server_Name_Here";
        public const string database = "FITNESSAPP";
        public const string security = "SSPI";

        // Initialize Connection String
        public static string ConnectionString
        {
            get { return "data source = " + server + "; " + "database = " + database + "; " + "integrated security = " + security; }
            set { ConnectionString = value; }
        }

    }
}
