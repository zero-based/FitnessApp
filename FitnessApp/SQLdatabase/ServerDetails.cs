namespace FitnessApp.SQLdatabase
{
    public static class ServerDetails
    {

        // Enter Server Name and Database Name Here
        public const string server   = @"Server_Name_Here";
        public const string database = "FITNESSAPP";
        public const string security = "SSPI";

        // Initialize Connection String
        public const string ConnectionString =  "data source = "         + server   + "; " +
                                                "database = "            + database + "; " +
                                                "integrated security = " + security;
    }
}
