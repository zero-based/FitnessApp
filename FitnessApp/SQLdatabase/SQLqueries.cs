using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System;
using FitnessApp.Models;
using System.Collections.Generic;

namespace FitnessApp.SQLdatabase
{
    class SQLqueries
    {

        ////////// SQL Connection string //////////
        // [IMPORTANT] Add your server name to ServerDetails Class.
        SqlConnection Connection = new SqlConnection(ServerDetails.ConnectionString);



        ////////// Local Fields //////////
        public int accountID;
        public string accountType;




        ////////// Helper Functions //////////

        // Encrypt given password
        private string PasswordEncryption(string password)
        {
            string hash = "f0le@rn";
            string encryptedPassword;
            byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider triDes = new TripleDESCryptoServiceProvider()
                { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = triDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encryptedPassword = Convert.ToBase64String(results, 0, results.Length);

                }
                return encryptedPassword;
            }
        }

        // Send email to user function (gmail only)
        private void SendEmail(string email, string name)
        {
            MailMessage message = new MailMessage();

            // Reciever's Email
            message.To.Add(email);

            // Email Subject
            message.Subject = "Welcome To Our Humble Fitness Application ";

            // Sender's Email
            message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");

            // Email Body
            message.IsBodyHtml = true;
            string htmlBody = "<h3>Hi '" + name + "'</h3><br><h3>Welcome&nbsp;to <strong><u>FitnessApp</u>,</strong></h3><br>" +
                              "<ul><li>Point 1</li><br><li>Point 2</li><br><li>Point 3</li><br></ul><br><p>Please contact us</p><br>";
            message.Body = htmlBody;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
            smtp.Send(message);
        }




        ////////// Queries and Main Functions //////////

        // Sign in query and function.
        public bool SignIn(string email, string password)
        {
            // Encrypt Password
            string encryptedPassword = PasswordEncryption(password);

            // Create Command
            SqlCommand cmd = new SqlCommand("SELECT * FROM AdminAndUserAccount WHERE Email = @email AND Password = @password", Connection);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", encryptedPassword);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    // Assigns the account ID and account type to the global variables.
                    accountID = (int)dr["ID"];
                    accountType = (string)dr["Type"];
                    Connection.Close();

                    // If user exists, return true.
                    return true;
                }
            }

            Connection.Close();

            // If user doesn't exist or any other case, return false.
            return false; 
        }

        // Check if the Username is taken or not; because Usernames must be unique
        public bool IsUsernameTaken(string username)
        {
            // Create Command
            SqlCommand cmd = new SqlCommand("SELECT Username FROM [User] WHERE Username = @username ;", Connection);
            cmd.Parameters.AddWithValue("@username", username);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    Connection.Close();

                    // If the Username is taken exists, return true.
                    return true;
                }

            }

            Connection.Close();

            // If the Username is not taken, return false.
            return false;
        }

        // Check if the Entered Email while signing up is taken or not
        public bool IsEmailTaken(string email)
        {
            // Create Command
            SqlCommand cmd = new SqlCommand("SELECT Email FROM AdminAndUserAccount WHERE Email = @email ;", Connection);
            cmd.Parameters.AddWithValue("@email", email);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    Connection.Close();

                    // If the email is taken exists, return true.
                    return true;
                }

            }

            Connection.Close();

            // If the email is not taken, return false.
            return false;
        }

        // Sign up function
        public void SignUp(ref byte[] profilePhoto, string firstName,   string lastName,
                           string username,         string email,       string password,
                           string gender,           string birthDate,   double weight,          double height,
                           double targetWeight,     double kilosToLose, double workoutsPerWeek, double workoutHoursPerDay)
        {
            // Password Encryption
            string encryptedPassword = PasswordEncryption(password);

            // Create Query amd Command
            string query1 = "INSERT INTO [User] (Photo, FirstName, LastName, Username, BirthDate, Gender, " +
                            "TargetWeight, Height, KilosToLosePerWeek, WorkoutsPerWeek, WorkoutHoursPerDay)" +
                            "VALUES('" + profilePhoto + "','" + firstName + "','" + lastName + "', '" + username + "','" + birthDate + "','" + gender + "','" + 
                            targetWeight + "','" + height + "','" + kilosToLose + "','" + workoutsPerWeek + "', '" + workoutHoursPerDay + "') ;";
            SqlCommand cmd1 = new SqlCommand(query1, Connection);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
            }

            Connection.Close();


            // Get User's ID
            // Create Query amd Command
            string query2 = "Select ID FROM [User] WHERE Username = @username";
            SqlCommand cmd2 = new SqlCommand(query2, Connection);
            cmd2.Parameters.AddWithValue("@username", username);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                if (dr2.HasRows == true)
                {
                    accountID = (int)dr2["ID"];
                }

            }

            Connection.Close();


            // Insert User's email and password
            // Create Query amd Command
            string query3 = "INSERT INTO AdminAndUserAccount(ID, Email, Password, Type) " +
                            "VALUES('" + accountID + "','" + email + "','" + encryptedPassword + "','User');";
            SqlCommand cmd3 = new SqlCommand(query3, Connection);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr3;
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
            }

            Connection.Close();


            // Insert User's weight into multiValued weight table
            // Create Query amd Command
            string query4 = "INSERT INTO UserWeight(UserId, Weight, Date) " +
                            "VALUES('" + accountID + "','" + weight + "', GETDATE());";
            SqlCommand cmd4 = new SqlCommand(query4, Connection);

            // Open Connection and Start Reading
            Connection.Open();
            SqlDataReader dr4;
            dr4 = cmd4.ExecuteReader();

            while (dr4.Read())
            {
            }

            Connection.Close();


            // Sending email to gmails only
            if (email.Contains("gmail"))
            {
                SendEmail(email, firstName);
            }
        }




        // Load all User's Data
        public UserModel LoadUserData(int userID)
        {
            UserModel currentUser = new UserModel();

            Connection.Open();

            //  Info from Usser Table
            string query = "SELECT * FROM [User] WHERE ID = @userID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    currentUser.FirstName          = (string)dr["FirstName"];
                    currentUser.LastName           = (string)dr["LastName"];
                    currentUser.Username           = (string)dr["Username"];
                    currentUser.Gender             = (string)dr["Gender"];
                    currentUser.BirthDate          = (string)dr["BirthDate"];
                    currentUser.Height             = (double)dr["Height"];
                    currentUser.TargetWeight       = (double)dr["TargetWeight"];
                    currentUser.KilosToLose        = (double)dr["KilosToLosePerWeek"];
                    currentUser.WorkoutsPerWeek    = (double)dr["WorkoutsPerWeek"];
                    currentUser.WorkoutHoursPerDay = (double)dr["WorkoutHoursPerDay"];
                }
            }

            dr.Close();


            // Info from Weight Table
            string query2 = "SELECT Weight FROM [UserWeight] WHERE UserId = @userID";

            SqlCommand cmd2 = new SqlCommand(query2, Connection);
            cmd2.Parameters.AddWithValue("@userID", userID);
            SqlDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                if (dr.HasRows == true)
                {
                    currentUser.Weight = (double)dr2["Weight"];
                }
            }

            dr2.Close();


            // Info from Accounts Table
            string query3 = "SELECT Email, Password FROM AdminAndUserAccount WHERE ID = @userID";
            SqlCommand cmd3 = new SqlCommand(query3, Connection);
            cmd3.Parameters.AddWithValue("@userID", userID);
            SqlDataReader dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                if (dr.HasRows == true)
                {
                    currentUser.Email    = (string)dr3["Email"];
                    currentUser.Password = (string)dr3["Password"];
                }
            }

            Connection.Close();

            return currentUser;
        }




        // Challenges queries and functions.
        public List<ChallengeModel> LoadAllChallenges(int accountID)
        {
            List<ChallengeModel> allChallengeModels = new List<ChallengeModel>();

            Connection.Open();

            string query = "SELECT [Challenge].*,[UserChallenge].UserId " +
                           "FROM [Challenge] Left JOIN [UserChallenge] " +
                           "ON [Challenge].ID = [UserChallenge].ChallengeId";

            
            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ChallengeModel temp = new ChallengeModel();
                temp.ID          = (int)reader["ID"];
                //temp.image
                temp.Name        = reader["Name"].ToString();
                temp.Description = reader["Description"].ToString();
                temp.Target      = reader["Target"].ToString();
                temp.Reward      = reader["Reward"].ToString();
                temp.DueDate     = reader["DueDate"].ToString();
                temp.WorkoutType = (int)reader["WorkoutId"];

                if (reader["UserId"] != DBNull.Value)
                    temp.IsJoined = true;
                else
                    temp.IsJoined = false;
               
                allChallengeModels.Add(temp);
            }
            Connection.Close();

            return allChallengeModels;
        }

        public List<ChallengeModel> LoadJoinedChallenges(int accountID)
        {
            Connection.Open();
            string query = "SELECT [Challenge].*,[UserChallenge].UserId " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].ID = [UserChallenge].ChallengeId";

            List<ChallengeModel> joinedChallengeModels = new List<ChallengeModel>();
            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ChallengeModel temp = new ChallengeModel();

                temp.ID          = (int)reader["ID"];
                //temp.image
                temp.Name        = reader["Name"].ToString();
                temp.Description = reader["Description"].ToString();
                temp.Target      = reader["Target"].ToString();
                temp.Reward      = reader["Reward"].ToString();
                temp.DueDate     = reader["DueDate"].ToString();
                temp.WorkoutType = (int)reader["WorkoutId"];
                
                if (reader["UserId"] != DBNull.Value)
                    temp.IsJoined = true;
                else
                    temp.IsJoined = false;
                joinedChallengeModels.Add(temp);
            }
            Connection.Close();

            return joinedChallengeModels;
        }

        public void JoinChallenge(int accountID, int ChallengeID)
        {
            Connection.Open();
            string query = "INSERT INTO [UserChallenge] " +
                           "(UserId, ChallengeId, JoiningDate) " +
                            "Values (" + accountID + ", " + ChallengeID + ", Convert(date, getdate()))";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();

            Connection.Close();
        }

        public void UnjoinChallenge(int accountID, int ChallengeID)
        {
            Connection.Open();
            string query = "DELETE [UserChallenge] " +
                           "WHERE [UserChallenge].UserId = " + accountID + " " +
                           "AND [UserChallenge].ChallengeId = " + ChallengeID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();

            Connection.Close();
        }



        // Plans queries and functions.
        public List<PlanModel> LoadPlans(int accountID)
        {
            Connection.Open();
            string query = "SELECT [Plan].*,[User].ID " + 
                           "FROM [Plan] Left JOIN [User] " +
                           "ON [Plan].ID = [User].PlanId";

            List<PlanModel> plansModels = new List<PlanModel>();
            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                PlanModel temp = new PlanModel();

                temp.ID = (int)reader[0];
                //temp.image
                temp.Name = reader["Name"].ToString();
                temp.Description = reader["Description"].ToString();
                temp.Duration = reader["Duration"].ToString();
                temp.Hardness = reader["Hardness"].ToString();

                if (reader[6] != DBNull.Value)
                    temp.IsJoined = true;
                else
                    temp.IsJoined = false;

                plansModels.Add(temp);
            }
            Connection.Close();

            return plansModels;
        }

        public List<DayModel> LoadPlanDays(int planID)
        {
            Connection.Open();
            string query = "SELECT * FROM[PlanDayDescription] " +
                           "WHERE[PlanDayDescription].PlanId = " + planID + " " +
                           "ORDER BY[PlanDayDescription].DayNumber";

            List<DayModel> dayModels = new List<DayModel>();
            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DayModel temp = new DayModel();

                temp.DayNumber = (int)reader["DayNumber"];
                //temp.image
                temp.BreakfastDescription = reader["BreakfastDescription"].ToString();
                temp.LunchDescription = reader["LunchDescription"].ToString();
                temp.DinnerDescription = reader["DinnerDescription"].ToString();
                temp.WorkoutDescription = reader["WorkoutDescription"].ToString();

                dayModels.Add(temp);
            }
            Connection.Close();

            return dayModels;
        }

        public bool isInPlan(int accountID)
        {
            Connection.Open();
            string query = "SELECT PlanId " +
                           "FROM [USER] " +
                           "WHERE ID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);

            if (cmd.ExecuteScalar() != DBNull.Value)
            {
                Connection.Close();
                return true;
            }
            else
            {
                Connection.Close();
                return false;
            }

        }

        public void JoinPlan(int accountID, int planID)
        {
            Connection.Open();
            string query = "UPDATE [User] " +
                           "SET PlanId = " + planID + ", " +
                           "PlanJoiningDate = Convert(date, getdate()) " +
                           "WHERE [User].ID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();
            Connection.Close();
        }

        public void UnjoinPlan(int accountID)
        {
            Connection.Open();
            string query = "UPDATE [User] " +
                           "SET PlanId = NULL, " +
                           "PlanJoiningDate = NULL " +
                           "WHERE [User].ID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();
            Connection.Close();
        }

        



    }
}
