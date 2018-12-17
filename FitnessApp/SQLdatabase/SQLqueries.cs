using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.SQLdatabase
{
    public class SQLqueries
    {

        ////////// SQL Connection string //////////
        // [IMPORTANT] Add your server name to ServerDetails Class.
        SqlConnection Connection = new SqlConnection(ServerDetails.ConnectionString);



        ////////// Local Fields //////////
        public int accountID;
        public string accountType;




        ////////// Helper Functions //////////

        // Encrypt given password
        public string EncryptPassword(string password)
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

        // Generate a Random Password: Used When adding a new Admin 
        private string GenerateRandomPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            int length = 7;
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();

        }

        // Send Email to a recently added user
        public void SendAdminEmail(string email, string randomPass)
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
            string htmlBody = "<h3>Hi</h3><br><h3>Welcome&nbsp;to <strong><u>FitnessApp</u>,</strong></h3><br>" +
                              "<ul><li>your password :'" + randomPass + "' </li><br><li>Change it as early as possible </li><br><li>Point 3</li><br></ul><br><p>Please contact us</p><br>";
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
            string encryptedPassword = EncryptPassword(password);

            // Create Command
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Account] WHERE Email = @email AND Password = @password", Connection);
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
                    accountID = (int)dr["AccountID"];
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
            SqlCommand cmd = new SqlCommand("SELECT Email FROM [Account] WHERE Email = @email ;", Connection);
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
        public void SignUp(byte[] profilePhoto, string firstName,          string lastName,
                           string username,     string email,              string password,
                           string gender,       string birthDate,          double weight,          double height,
                           double targetWeight, double kilosToLosePerWeek, double workoutsPerWeek, double workoutHoursPerDay)
        {
            // Password Encryption
            string encryptedPassword = EncryptPassword(password);

            // Create Query amd Command
            string query1 = "INSERT INTO [User] (Photo, FirstName, LastName, Username, BirthDate, Gender, " +
                            "TargetWeight, Height, KilosToLosePerWeek, WorkoutsPerWeek, WorkoutHoursPerDay)" +
                            "VALUES(@Photo ,'" + firstName + "','" + lastName + "', '" + username + "','" + birthDate + "','" + gender + "','" + 
                            targetWeight + "','" + height + "','" + kilosToLosePerWeek + "','" + workoutsPerWeek + "', '" + workoutHoursPerDay + "') ;";
            SqlCommand cmd1 = new SqlCommand(query1, Connection);

            if (profilePhoto == null)
                cmd1.Parameters.Add("@Photo", SqlDbType.Image).Value = DBNull.Value;
            else
                cmd1.Parameters.AddWithValue("@Photo", profilePhoto);

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
            string query2 = "Select PK_UserID FROM [User] WHERE Username = @username";
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
                    accountID = (int)dr2["PK_UserID"];
                }

            }

            Connection.Close();


            // Insert User's email and password
            // Create Query amd Command
            string query3 = "INSERT INTO [Account](AccountID, Email, Password, Type) " +
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
            string query4 = "INSERT INTO UserWeight(FK_UserWeight_UserID, Weight, Date) " +
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

            // Info from User Table
            string query = "SELECT * FROM [User] WHERE PK_UserID = @userID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            if (dr["Photo"] != DBNull.Value)
                currentUser.ProfilePhoto.ByteArray = (byte[])dr["Photo"];

            currentUser.FirstName              = dr["FirstName"].ToString();
            currentUser.LastName               = dr["LastName"] .ToString();
            currentUser.Username               = dr["Username"] .ToString();
            currentUser.Gender                 = dr["Gender"]   .ToString();
            currentUser.BirthDate              = dr["BirthDate"].ToString();
            currentUser.Height                 = (double) dr["Height"];
            currentUser.TargetWeight           = (double) dr["TargetWeight"];
            currentUser.KilosToLosePerWeek     = (double) dr["KilosToLosePerWeek"];
            currentUser.WorkoutsPerWeek        = (double) dr["WorkoutsPerWeek"];
            currentUser.WorkoutHoursPerDay     = (double) dr["WorkoutHoursPerDay"];

            dr.Close();

            // Info from Weight Table
            string query2 = "select Weight from UserWeight where FK_UserWeight_UserID = @UserId order by Date DESC";
            SqlCommand cmd2 = new SqlCommand(query2, Connection);
            cmd2.Parameters.AddWithValue("@userID", userID);
            currentUser.Weight = (double) cmd2.ExecuteScalar(); 


            // Info from Accounts Table
            string query3 = "SELECT Email, Password FROM [Account] WHERE AccountID = @userID";
            SqlCommand cmd3 = new SqlCommand(query3, Connection);
            cmd3.Parameters.AddWithValue("@userID", userID);
            SqlDataReader dr3 = cmd3.ExecuteReader();

            dr3.Read();
            currentUser.Email    = dr3["Email"]   .ToString();
            currentUser.Password = dr3["Password"].ToString();
            dr3.Close();

            // Get User Age
            string query4 = "SELECT FLOOR (DATEDIFF (DAY, BirthDate, GETDATE()) / 365.25) " +
                            "FROM [User] WHERE PK_UserID = @userID";
            SqlCommand cmd4 = new SqlCommand(query4, Connection);
            cmd4.Parameters.AddWithValue("@userID", userID);

            currentUser.Age = Convert.ToInt16(cmd4.ExecuteScalar());

            Connection.Close();

            return currentUser;
        }

        // Update User Profile
        public void UpdateUserProfile(UserModel currentUser)
        {
            Connection.Open();

            string query = "UPDATE [User] " +
                           "SET Photo = @Photo " +
                           "WHERE PK_UserID = @UserId";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserId", currentUser.ID);
            cmd.Parameters.AddWithValue("@Photo", currentUser.ProfilePhoto.ByteArray);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            SqlCommand cmd1 = new SqlCommand("AddNewWeight", Connection);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd1.Parameters.Add(new SqlParameter("@AddedWeight", currentUser.Weight));
            SqlDataReader reader1 = cmd1.ExecuteReader();
            reader1.Close();

            SqlCommand cmd2 = new SqlCommand("ChangeHeight", Connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd2.Parameters.Add(new SqlParameter("@Height", currentUser.Height));
            SqlDataReader reader2 = cmd2.ExecuteReader();
            reader2.Close();

            SqlCommand cmd3 = new SqlCommand("ChangeTargetWeight", Connection);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd3.Parameters.Add(new SqlParameter("@TargetWeight", currentUser.TargetWeight));
            SqlDataReader reader3 = cmd3.ExecuteReader();
            reader3.Close();

            SqlCommand cmd4 = new SqlCommand("ChangeKilosToLosePerWeek", Connection);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd4.Parameters.Add(new SqlParameter("@KilosToLosePerWeek", currentUser.KilosToLosePerWeek));
            SqlDataReader reader4 = cmd4.ExecuteReader();
            reader4.Close();

            SqlCommand cmd5 = new SqlCommand("ChangeWorkoutsDaysPerWeek", Connection);
            cmd5.CommandType = CommandType.StoredProcedure;
            cmd5.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd5.Parameters.Add(new SqlParameter("@Days", currentUser.WorkoutsPerWeek));
            SqlDataReader reader5 = cmd5.ExecuteReader();
            reader5.Close();

            SqlCommand cmd6 = new SqlCommand("ChangeWorkoutsHoursPerDay", Connection);
            cmd6.CommandType = CommandType.StoredProcedure;
            cmd6.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd6.Parameters.Add(new SqlParameter("@Hours", currentUser.WorkoutHoursPerDay));
            SqlDataReader reader6 = cmd6.ExecuteReader();
            reader6.Close();

            Connection.Close();
        }

        // Update User Account
        public void UpdateUserAccount(UserModel currentUser)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("ChangeFirstName", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd.Parameters.Add(new SqlParameter("@FirstName", currentUser.FirstName));
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            SqlCommand cmd2 = new SqlCommand("ChangeLastName", Connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd2.Parameters.Add(new SqlParameter("@LastNmae", currentUser.LastName));
            SqlDataReader reader2 = cmd2.ExecuteReader();
            reader2.Close();

            SqlCommand cmd3 = new SqlCommand("ChangeUserName", Connection);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd3.Parameters.Add(new SqlParameter("@UserName", currentUser.Username));
            SqlDataReader reader3 = cmd3.ExecuteReader();
            reader3.Close();

            SqlCommand cmd4 = new SqlCommand("ChangeEmail", Connection);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd4.Parameters.Add(new SqlParameter("@Email", currentUser.Email));
            SqlDataReader reader4 = cmd4.ExecuteReader();
            reader4.Close();

            Connection.Close();
        }

        // Update User Password
        public void UpdateUserPassword(UserModel currentUser)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("ChangePassword", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd.Parameters.Add(new SqlParameter("@Password", currentUser.Password));
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            Connection.Close();
        }

        // Save Feedback
        public void SaveFeedback(int userID, int rating, string feedback)
        {
            Connection.Open();

            string query = "INSERT INTO [Feedback] (FK_Feedback_UserID, Rating, Feedback) " +
                           "VALUES('" + userID + "','" + rating + "','" + feedback + "')";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();

            Connection.Close();
        }




        // Challenges queries and functions.
        public List<ChallengeModel> LoadAllChallenges(int accountID)
        {

            // Remove All Overdue Challenges before reading data
            RemoveOverdueChallenges();

            List<ChallengeModel> allChallengeModels = new List<ChallengeModel>();

            Connection.Open();

            string query = "SELECT [Challenge].*,[UserChallenge].* " +
                           "FROM [Challenge] Left JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "AND FK_UserChallenge_UserID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ChallengeModel temp = new ChallengeModel();
                temp.ID             = (int)reader["PK_ChallengeID"];

                if (reader["Photo"] != DBNull.Value)
                    temp.Photo.ByteArray = (byte[])reader["Photo"];

                temp.Name           = reader["Name"].ToString();
                temp.Description    = reader["Description"].ToString();
                temp.TargetMinutes  = (int)reader["TargetMinutes"];
                temp.Reward         = reader["Reward"].ToString();
                temp.DueDate        = reader["DueDate"].ToString().ToString().Split(' ')[0];
                temp.WorkoutType    = (int)reader["FK_Challenge_WorkoutID"];

                if (reader["FK_UserChallenge_UserID"] != DBNull.Value)
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
            // Remove All Overdue Challenges before reading data
            RemoveOverdueChallenges();

            List<ChallengeModel> joinedChallengeModels = new List<ChallengeModel>();

            Connection.Open();

            string query = "SELECT [Challenge].*,[UserChallenge].* " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE FK_UserChallenge_UserID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ChallengeModel temp = new ChallengeModel();

                temp.ID             = (int)reader["PK_ChallengeID"];
                temp.Name           = reader["Name"].ToString();
                temp.Description    = reader["Description"].ToString();
                temp.TargetMinutes  = (int)reader["TargetMinutes"];
                temp.Reward         = reader["Reward"].ToString();
                temp.DueDate        = reader["DueDate"].ToString().Split(' ')[0];
                temp.WorkoutType    = (int)reader["FK_Challenge_WorkoutID"];
                temp.Progress       = (int)reader["Progress"];
                temp.IsJoined       = true;

                joinedChallengeModels.Add(temp);
            }

            Connection.Close();

            return joinedChallengeModels;
        }

        public void JoinChallenge(int accountID, int ChallengeID)
        {
            Connection.Open();
            string query = "INSERT INTO [UserChallenge] " +
                           "(FK_UserChallenge_UserID, FK_UserChallenge_ChallengeID, JoiningDate, Progress) " +
                            "Values (" + accountID + ", " + ChallengeID + ", getdate(), 0)";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();

            Connection.Close();
        }

        public void UnjoinChallenge(int accountID, int ChallengeID)
        {
            Connection.Open();
            string query = "DELETE [UserChallenge] " +
                           "WHERE [UserChallenge].FK_UserChallenge_UserID = " + accountID + " " +
                           "AND [UserChallenge].FK_UserChallenge_ChallengeID = " + ChallengeID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.ExecuteReader();

            Connection.Close();
        }

        public void RemoveOverdueChallenges()
        {

            Connection.Open();

            string query = "DELETE [UserChallenge] " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE [Challenge].DueDate <= getdate()";

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            query = "DELETE FROM[Challenge] WHERE[Challenge].DueDate <= getdate()";

            cmd = new SqlCommand(query, Connection);
            reader = cmd.ExecuteReader();

            Connection.Close();

        }

        public void UpdateChallengesProgress(int accountID, string workout, double duration)
        {
            Connection.Open();

            string query = "UPDATE [UserChallenge] " +
                           "SET progress += @workoutDuration " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "RIGHT JOIN [Workout] " +
                           "ON [Challenge].Fk_Challenge_WorkoutID = [Workout].PK_WorkoutID " +
                           "WHERE FK_UserChallenge_UserID = @userID " +
                           "AND getdate() Between JoiningDate and DueDate " +
                           "AND [Workout].[Name] = @workoutName";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@workoutDuration", duration);
            cmd.Parameters.AddWithValue("@userID", accountID);
            cmd.Parameters.AddWithValue("@workoutName", workout);
            cmd.ExecuteReader();

            Connection.Close();
        }



        // Plans queries and functions.
        public List<PlanModel> LoadPlans(int accountID)
        {
            Connection.Open();
            string query = "SELECT [Plan].*,[User].PK_UserID " + 
                           "FROM [Plan] Left JOIN [User] " +
                           "ON [Plan].PK_PlanID = [User].FK_User_PlanID " +
                           "AND PK_UserID = @userID";

            List<PlanModel> plansModels = new List<PlanModel>();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@userID", accountID);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                PlanModel temp = new PlanModel();

                temp.ID = (int)reader["PK_PlanID"];

                if (reader["Photo"] != DBNull.Value)
                    temp.Photo.ByteArray = (byte[])reader["Photo"];

                temp.Name = reader["Name"].ToString();
                temp.Description = reader["Description"].ToString();
                temp.Duration = reader["Duration"].ToString();
                temp.Hardness = reader["Hardness"].ToString();

                if (reader["PK_UserID"] != DBNull.Value)
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
                           "WHERE[PlanDayDescription].FK_PlanDayDescription_PlanID = " + planID + " " +
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

        public bool IsInPlan(int accountID)
        {
            Connection.Open();
            string query = "SELECT FK_User_PlanID " +
                           "FROM [USER] " +
                           "WHERE PK_UserID = " + accountID;

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
                           "SET FK_User_PlanID = " + planID + ", " +
                           "PlanJoiningDate = Convert(date, getdate()) " +
                           "WHERE [User].PK_UserID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            query = "INSERT INTO UserPlanDay (FK_UserPlanDay_UserID, DayNumber) VALUES (@userID, 1)";
            cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@userID", accountID);
            reader = cmd.ExecuteReader();

            Connection.Close();
        }

        public void UnjoinPlan(int accountID)
        {
            Connection.Open();
            string query = "UPDATE [User] " +
                           "SET FK_User_PlanID = NULL, " +
                           "PlanJoiningDate = NULL " +
                           "WHERE [User].PK_UserID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            query = "DELETE FROM UserPlanDay WHERE FK_UserPlanDay_UserID = @userID";
            cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@userID", accountID);
            reader = cmd.ExecuteReader();

            Connection.Close();
        }



        //////// Weight ////////

        // Weight chart
        public List<double> GetWeightValues(int accountID)
        {
            List<double> weightValues = new List<double>();

            Connection.Open();

            SqlCommand CommandString = new SqlCommand("select Weight from UserWeight where FK_UserWeight_UserID = @UserId order by Date DESC", Connection);
            CommandString.CommandType = CommandType.Text;
            CommandString.Parameters.AddWithValue("@UserId", accountID);
            SqlDataReader ReaderString = CommandString.ExecuteReader();

            for (int i = 0; ReaderString.Read() && i < 10; i++)
            {
                weightValues.Add((double)ReaderString["Weight"]);
            }

            Connection.Close();

            // Reverse List
            weightValues.Reverse();

            return weightValues;
        }

        public List<string> GetWeightDateValues(int accountID)
        {
            List<string> dateValues = new List<string>();

            Connection.Open();

            SqlCommand CommandString = new SqlCommand("select FORMAT(Date, 'MMM yy') AS [Date] from UserWeight where FK_UserWeight_UserID = @UserId order by Date DESC", Connection);
            CommandString.CommandType = CommandType.Text;
            CommandString.Parameters.AddWithValue("@UserId", accountID);

            SqlDataReader ReaderString = CommandString.ExecuteReader();

            for (int i = 0; ReaderString.Read() && i < 10; i++)
            {
                dateValues.Add((string)ReaderString["Date"]);
            }

            Connection.Close();

            // Reverse List
            dateValues.Reverse();

            return dateValues;
        }

        public void AddNewWeight(double NewWeight, int accountID)
        {
            Connection.Open();

            string query = "INSERT INTO [UserWeight] VALUES (@UserId, @AddedWeight, getdate())";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.Add(new SqlParameter("@UserId", accountID));
            cmd.Parameters.Add(new SqlParameter("@AddedWeight", NewWeight));
            cmd.ExecuteReader();

            Connection.Close();
        }

        // Total Weight Lost
        public double GetTotalWeightLostPerWeek(int accountID)
        {

            double WeekWeightLost = 0;
            List<double> WeekWeight = new List<double>();

            Connection.Open();

            SqlCommand cmd = new SqlCommand("select Weight From UserWeight Where FK_UserWeight_UserID=@id AND DATEPART(WEEK,Date) = DATEPART(WEEK,GETDATE()) Order by Date", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                WeekWeight.Add((double)(rd["Weight"]));
            }

            Connection.Close();

            for (int i = 0; i < (WeekWeight.Count - 1); i++)
            {
                WeekWeightLost += (WeekWeight[i] - WeekWeight[i + 1]);
            }

            return Math.Round(WeekWeightLost, 2);

        }

        public double GetTotalWeightLostPerMonth(int accountID)
        {

            double MonthWeightLost = 0;
            List<double> MonthWeight = new List<double>();

            Connection.Open();

            SqlCommand cmd1 = new SqlCommand("select Weight From UserWeight Where FK_UserWeight_UserID=@id AND DATEPART(MONTH,Date) = DATEPART(MONTH,GETDATE()) Order by Date", Connection);
            cmd1.Parameters.AddWithValue("@id", accountID);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            while (rd1.Read())
            {
                MonthWeight.Add((double)(rd1["Weight"]));
            }

            Connection.Close();

            for (int i = 0; i < (MonthWeight.Count - 1); i++)
            {
                MonthWeightLost += (MonthWeight[i] - MonthWeight[i + 1]);
            }

            return Math.Round(MonthWeightLost, 2);

        }

        public double GetTotalWeightLostPerYear(int accountID)
        {

            double YearWeightLost = 0;
            List<double> YearWeight = new List<double>();

            Connection.Open();

            SqlCommand cmd2 = new SqlCommand("select Weight From UserWeight Where FK_UserWeight_UserID=@id AND DATEPART(YEAR,Date) = DATEPART(YEAR,GETDATE()) Order by Date", Connection);
            cmd2.Parameters.AddWithValue("@id", accountID);
            SqlDataReader rd2 = cmd2.ExecuteReader();

            while (rd2.Read())
            {
                YearWeight.Add((double)(rd2["Weight"]));
            }

            Connection.Close();

            for (int i = 0; i < (YearWeight.Count - 1); i++)
            {
                YearWeightLost += (YearWeight[i] - YearWeight[i + 1]);
            }

            return Math.Round(YearWeightLost, 2);

        }

        // Average Weight Lost
        public double GetAverageWeightLostPerWeek(int accountID)
        {
            double WeekWeightLost = 0;
            List<double> WeekWeight = new List<double>();

            Connection.Open();

            SqlCommand cmd = new SqlCommand("select Weight From UserWeight Where FK_UserWeight_UserID=@id AND DATEPART(WEEK,Date) = DATEPART(WEEK,GETDATE()) Order by Date", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);
            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                WeekWeight.Add((double)(rd["Weight"]));
            }

            Connection.Close();

            for (int i = 0; i < (WeekWeight.Count - 1); i++)
            {
                WeekWeightLost += (WeekWeight[i] - WeekWeight[i + 1]);
            }

            return Math.Round((WeekWeightLost / WeekWeight.Count), 2);

        }

        public double GetAverageWeightLostPerMonth(int accountID)
        {
            double MonthWeightLost = 0;
            List<double> MonthWeight = new List<double>();

            Connection.Open();

            SqlCommand cmd1 = new SqlCommand("select Weight From UserWeight Where FK_UserWeight_UserID=@id AND DATEPART(MONTH,Date) = DATEPART(MONTH,GETDATE()) Order by Date", Connection);
            cmd1.Parameters.AddWithValue("@id", accountID);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            while (rd1.Read())
            {
                MonthWeight.Add((double)(rd1["Weight"]));
            }

            Connection.Close();

            for (int i = 0; i < (MonthWeight.Count - 1); i++)
            {
                MonthWeightLost += (MonthWeight[i] - MonthWeight[i + 1]);
            }

            return Math.Round((MonthWeightLost / MonthWeight.Count), 2);

        }

        public double GetAverageWeightLostPerYear(int accountID)
        {

            double YearWeightLost = 0;
            List<double> YearWeight = new List<double>();

            Connection.Open();

            SqlCommand cmd2 = new SqlCommand("select Weight From UserWeight Where FK_UserWeight_UserID=@id AND DATEPART(YEAR,Date) = DATEPART(YEAR,GETDATE()) Order by Date", Connection);
            cmd2.Parameters.AddWithValue("@id", accountID);
            SqlDataReader rd2 = cmd2.ExecuteReader();

            while (rd2.Read())
            {
                YearWeight.Add((double)(rd2["Weight"]));
            }

            Connection.Close();

            for (int i = 0; i < (YearWeight.Count - 1); i++)
            {
                YearWeightLost += (YearWeight[i] - YearWeight[i + 1]);
            }

            return Math.Round((YearWeightLost / YearWeight.Count), 2);

        }



        //////// Motivational Quote ////////

        public string GetMotivationalQuote()
        {
            Connection.Open();

            string query = "SELECT Quote FROM MotivationalQuote " +
                           "WHERE PK_MotivationalQuoteID = DATEPART(DAY,GETDATE())";
            SqlCommand CMD = new SqlCommand(query, Connection);

            string Quote = CMD.ExecuteScalar().ToString();

            Connection.Close();

            return Quote;
        }



        //////// Food/Workout ////////

        public List<String> GetAllFood()
        {
            Connection.Open();

            List<String> food = new List<string>();

            SqlCommand cmd = new SqlCommand("select Name from Food", Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string FoodName = reader["Name"].ToString();
                food.Add(FoodName);
            }
            reader.Close();

            Connection.Close();

            return food;
        }

        public List<String> GetAllWorkouts()
        {
            Connection.Open();

            List<String> workouts = new List<string>();

            SqlCommand cmd = new SqlCommand("select Name from Workout", Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string WorkoutName = reader["Name"].ToString();
                workouts.Add(WorkoutName);
            }
            reader.Close();

            Connection.Close();

            return workouts;
        }

        public void AddFood(string food, double quantity, int accountID)
        {
            Connection.Open();

            int foodID = 0;
            double totalCaloriesGained = 0;

            // Calculate Total Calories gained
            SqlCommand cmd = new SqlCommand("select Type from Food where Name=@food", Connection);
            cmd.Parameters.AddWithValue("@food", food);
            string foodType = cmd.ExecuteScalar().ToString();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (foodType == "Protein")
                {
                    totalCaloriesGained += 4 * quantity;
                }
                else if (foodType == "Fats")
                {
                    totalCaloriesGained += 9 * quantity;
                }
                else if (foodType == "Carbohydrates")
                {
                    totalCaloriesGained += 4 * quantity;
                }
            }
            reader.Close();

            // Get Food ID
            SqlCommand cmd2 = new SqlCommand("select PK_FoodID from Food where Name=@food", Connection);
            cmd2.Parameters.AddWithValue("@food", food);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                foodID = (int)(reader2["PK_FoodID"]);
            }
            reader2.Close();

            // Insert Food in UserFood Table
            SqlCommand cmd3 = new SqlCommand("insert into [UserFood] (FK_UserFood_UserID, FK_UserFood_FoodID, CaloriesGained,DateOfToday) Values (@UserId, @FoodId, @Calories, getdate())", Connection);
            cmd3.Parameters.AddWithValue("@UserId", accountID);
            cmd3.Parameters.AddWithValue("@FoodId", foodID);
            cmd3.Parameters.AddWithValue("@Calories", totalCaloriesGained);
            cmd3.ExecuteReader();

            Connection.Close();

        }

        public void AddWorkout(string workout, double duration, UserModel currentUser)
        {
            int workoutID = 0;
            double totalCaloriesLost = 0;

            if (currentUser.Gender == "Female")
            {
                totalCaloriesLost = ((currentUser.Age * 0.074) - (currentUser.Weight * 0.05741) + (100 * 0.4472) - 20.4022) * (duration / 4.184);
            }
            else
            {
                totalCaloriesLost = ((currentUser.Age * 0.2017) - (currentUser.Weight * 0.09036) + (100 * 0.6309) - 55.0969) * (duration / 4.184);
            }

            Connection.Open();

            // Get Workout ID
            SqlCommand cmd4 = new SqlCommand("select PK_WorkoutID from Workout where Name=@name", Connection);
            cmd4.Parameters.AddWithValue("@name", workout);
            SqlDataReader dr = cmd4.ExecuteReader();
            while (dr.Read())
            {
                workoutID = (int)(dr["PK_WorkoutID"]);
            }
            dr.Close();

            // Insert Workout in UserWorkout Table
            SqlCommand Cmd3 = new SqlCommand("Insert into UserWorkout (FK_UserWorkout_UserID,FK_UserWorkout_WorkoutID,MinutesOfWork,CaloriesLost,DateOfToday) Values(@userid, @workoutid, @duration, @calories, getdate())", Connection);
            Cmd3.Parameters.AddWithValue("@userid", currentUser.ID);
            Cmd3.Parameters.AddWithValue("@workoutid", workoutID);
            Cmd3.Parameters.AddWithValue("@duration", duration);
            Cmd3.Parameters.AddWithValue("@calories", Math.Round(totalCaloriesLost, 2));
            Cmd3.ExecuteNonQuery();

            Connection.Close();
        }

        public int GetWorkoutID(string workoutName)
        {
            int id = -1;

            Connection.Open();
            string query = "select PK_WorkoutID from Workout where Name=@name;";
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@name", workoutName);
            id = (int)cmd.ExecuteScalar();
            Connection.Close();

            return id;
        }


        //////// Joined Plan ////////

        // Get Joined Plan ID and Name 
        public int GetJoinedPlanID(int accountID)
        {
            int SQLplanID = 0;
            string query = "select FK_User_PlanID from [User] where PK_UserID= @accountID;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {

                    SQLplanID = (int)dr["FK_User_PlanID"];

                }
            }
            dr.Close();
            Connection.Close();

            return SQLplanID;

        }

        public string GetJoinedPlanName(int accountID)
        {
            int SQLplanID = GetJoinedPlanID(accountID);
            string SQLplanName = "";
            string query = "select Name from [Plan] where PK_PlanID=@SQLplanID;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            SQLplanName = (string)dr["Name"];
            Connection.Close();
            return SQLplanName;
        }

        // Get Joined Plan Day Number
        public int GetJoinedPlanDayNumber(int accountID)
        {
            Connection.Open();

            string dateDiff = "select DATEDIFF(day, PlanJoiningDate , getdate()) from [user] where PK_UserID = @userID";
            SqlCommand cmd = new SqlCommand(dateDiff, Connection);
            cmd.Parameters.AddWithValue("@userID", accountID);

            int dayNumber = 0;
            
            dayNumber = (int)cmd.ExecuteScalar();

            Connection.Close();

            // to make it begin with 1 instead of zero
            int SQLplanDay = dayNumber + 1;

            return SQLplanDay;
        }

        // Get Joined Plan items' Descriptions
        public string GetDayBreakfastDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string breakfastDiscription = "";
            string query = "select BreakfastDescription from PlanDayDescription where FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    breakfastDiscription = (string)dr["BreakfastDescription"];

                }
            }

            Connection.Close();
            return breakfastDiscription;
        }

        public string GetDayLucnchDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string lucnchDiscription = "";
            string query = "select LunchDescription from PlanDayDescription where FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    lucnchDiscription = (string)dr["LunchDescription"];

                }
            }

            Connection.Close();
            return lucnchDiscription;
        }

        public string GetDayDinnerDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string dinnerDiscription = "";
            string query = "select DinnerDescription from PlanDayDescription where FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    dinnerDiscription = (string)dr["DinnerDescription"];

                }
            }

            Connection.Close();
            return dinnerDiscription;
        }

        public string GetDayWorkoutDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string workoutDiscription = "";
            string query = "select WorkoutDescription from PlanDayDescription where FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    workoutDiscription = (string)dr["WorkoutDescription"];

                }
            }

            Connection.Close();
            return workoutDiscription;
        }


        // Get Joined Plan Checkboxes' Status
        public bool GetDayBreakfastStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlBreakfast = false;
            string query = " select BreakfastIsDone from UserPlanDay where FK_UserPlanDay_UserID = @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    SqlBreakfast = (bool)dr["BreakfastIsDone"];

                }
            }
            Connection.Close();

            return SqlBreakfast;
        }

        public bool GetDayLunchStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlLunch = false;
            string query = " select LunchIsDone from UserPlanDay where FK_UserPlanDay_UserID = @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    SqlLunch = (bool)dr["LunchIsDone"];

                }
            }
            Connection.Close();

            return SqlLunch;
        }

        public bool GetDayDinnerStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlDinner = false;
            string query = " select DinnerIsDone from UserPlanDay where FK_UserPlanDay_UserID=@accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    SqlDinner = (bool)dr["DinnerIsDone"];

                }
            }
            Connection.Close();

            return SqlDinner;
        }

        public bool GetDayWorkoutStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlWorkout = false;
            string query = " select WorkoutsIsDone from UserPlanDay where FK_UserPlanDay_UserID= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    SqlWorkout = (bool)dr["WorkoutsIsDone"];

                }
            }
            Connection.Close();

            return SqlWorkout;
        }


        //Modify Joined Plan Checkboxes

        public void UpdatePlanDayNumber(int accountID, int dayNumber)
        {
            Connection.Open();
            string query = "SELECT DayNumber " +
                           "FROM UserPlanDay " +
                           "Where FK_UserPlanDay_UserID = @userID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@userID", accountID);

            if ((int)cmd.ExecuteScalar() != dayNumber)
            {
                query = "UPDATE [UserPlanDay] "          +
                        "SET DayNumber   = @dayNumber, " +
                        "BreakfastIsDone = 0, "          +
                        "LunchIsDone     = 0, "          +
                        "DinnerIsDone    = 0, "          +
                        "WorkoutsIsDone  = 0 "           +
                        "WHERE FK_UserPlanDay_UserID = @userID";

                cmd = new SqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("@dayNumber", dayNumber);
                cmd.Parameters.AddWithValue("@userID", accountID);

                SqlDataReader reader = cmd.ExecuteReader();
            }

            Connection.Close();
        }

        public void UpdateDayBreakfastStatus(bool checkedBreakfast, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            string query = "Update UserPlanDay SET  BreakfastIsDone=@checkedBreakfast where  FK_UserPlanDay_UserID= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedBreakfast", checkedBreakfast);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.ExecuteNonQuery();

            Connection.Close();
        }

        public void UpdateDayLunchStatus(bool checkedLunch, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            string query = "Update UserPlanDay SET  LunchIsDone=@checkedLunch where  FK_UserPlanDay_UserID= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedLunch", checkedLunch);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.ExecuteNonQuery();

            Connection.Close();
        }

        public void UpdateDayDinnerStatus(bool checkedDinner, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            string query = "Update UserPlanDay SET  DinnerIsDone=@checkedDinner where  FK_UserPlanDay_UserID= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedDinner", checkedDinner);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.ExecuteNonQuery();

            Connection.Close();
        }

        public void UpdateDayWorkoutStatus(bool checkedWorkout, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            string query = "Update UserPlanDay SET  WorkoutsIsDone=@checkedWorkout where  FK_UserPlanDay_UserID= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedWorkout", checkedWorkout);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            cmd.ExecuteNonQuery();


            Connection.Close();
        }



        ///////////// Admin's Queries and Functions /////////////
        

        // Load all Admin's Data
        public AdminModel LoadAdminData(int adminID)
        {
            AdminModel currentAdmin = new AdminModel();

            Connection.Open();

            // Info from Admin Table
            string query = "SELECT * FROM [Admin] WHERE PK_AdminID = @adminID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@adminID", adminID);
            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            currentAdmin.FirstName = dr["FirstName"].ToString();
            currentAdmin.LastName = dr["LastName"].ToString();
            dr.Close();

            // Info from Accounts Table
            query = "SELECT Email, Password FROM [Account] WHERE AccountID = @adminID";

            SqlCommand cmd2 = new SqlCommand(query, Connection);
            cmd2.Parameters.AddWithValue("@adminID", adminID);
            SqlDataReader dr2 = cmd2.ExecuteReader();

            dr2.Read();
            currentAdmin.Email = dr2["Email"].ToString();
            currentAdmin.Password = dr2["Password"].ToString();
            dr2.Close();

            Connection.Close();

            return currentAdmin;
        }

        // Update User Account
        public void UpdateAdminAccount(AdminModel currentAdmin)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("ModifyFirstName", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AdminId", currentAdmin.ID));
            cmd.Parameters.Add(new SqlParameter("@FirstName", currentAdmin.FirstName));
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            SqlCommand cmd2 = new SqlCommand("ModifyLastName", Connection);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add(new SqlParameter("@AdminId", currentAdmin.ID));
            cmd2.Parameters.Add(new SqlParameter("@LastName", currentAdmin.LastName));
            SqlDataReader reader2 = cmd2.ExecuteReader();
            reader2.Close();

            SqlCommand cmd4 = new SqlCommand("ModifyEmail", Connection);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add(new SqlParameter("@AdminId", currentAdmin.ID));
            cmd4.Parameters.Add(new SqlParameter("@Email", currentAdmin.Email));
            SqlDataReader reader4 = cmd4.ExecuteReader();
            reader4.Close();

            Connection.Close();
        }

        public void UpdateAdminPassword(AdminModel currentAdmin)
        {
            Connection.Open();

            string query = "UPDATE Account SET Password = @newPassword, Type = @type WHERE AccountID = @accountID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@newPassword", currentAdmin.Password);
            cmd.Parameters.AddWithValue("@type", "Admin");
            cmd.Parameters.AddWithValue("@accountID", currentAdmin.ID);
            cmd.ExecuteNonQuery();

            Connection.Close();

        }

        public void AddNewAdmin(string email, string firstName, string lastName)
        {
            Connection.Open();
            string query = "INSERT INTO Admin(FirstName,LastName) VALUES (@firstName,@lastName)";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.ExecuteReader();

            Connection.Close();

            string password = GenerateRandomPassword();
            string encryptedPassword = EncryptPassword(password);
            InsertNewAdminAccount(email, encryptedPassword);

            // Sending email to gmails only
            if (email.Contains("gmail"))
                SendAdminEmail(email, password);
        }

        public void InsertNewAdminAccount(string email, string password)
        {
            Connection.Open();

            string query = "SELECT MIN(PK_AdminID) FROM Admin ";
            SqlCommand cmd = new SqlCommand(query, Connection);
            int adminId = (int)cmd.ExecuteScalar();

            query = "INSERT INTO Account VALUES(@adminId , @email, @password , @type);";

            SqlCommand cmd2 = new SqlCommand(query, Connection);
            cmd2.Parameters.AddWithValue("@adminId", adminId);
            cmd2.Parameters.AddWithValue("@email", email);
            cmd2.Parameters.AddWithValue("@password", password);
            cmd2.Parameters.AddWithValue("@type", "Admin*");
            cmd2.ExecuteReader();

            Connection.Close();
        }

        public List<int> GetAppRatingValues()
        {
            Connection.Open();
            List<int> ratingList = new List<int>();

            for (int i = 1; i <= 5; i++)
            {
                string query = "SELECT COUNT(FK_Feedback_UserID) FROM Feedback WHERE Rating = @ratingValue;";

                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.Parameters.AddWithValue("@ratingValue", i);

                ratingList.Add((int)cmd.ExecuteScalar());
            }

            Connection.Close();

            return ratingList;
        }

        public List<FeedbackModel> LoadAllFeedbacks()
        {

            List<FeedbackModel> allFeedbackModels = new List<FeedbackModel>();

            Connection.Open();

            string query = "SELECT [user].FirstName, [user].LastName, [Feedback].Feedback " +
                           "FROM [User] RIGHT JOIN [Feedback] " +
                           "ON [User].PK_UserID  = Feedback.FK_Feedback_UserID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                FeedbackModel temp = new FeedbackModel();

                temp.FirstName = reader["FirstName"].ToString();
                temp.LastName  = reader["LastName"] .ToString();
                temp.Feedback  = reader["Feedback"] .ToString();

                allFeedbackModels.Add(temp);
            }

            Connection.Close();

            return allFeedbackModels;
        }

        public void DeleteFeedback(string feedbackBody)
        {
            Connection.Open();

            string query = "DELETE FROM [Feedback] WHERE [Feedback].Feedback like @feedbackBody";
            
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@feedbackBody", feedbackBody);
            cmd.ExecuteNonQuery();

            Connection.Close();
        }

        public int GetAppUsersNumber()
        {
            int appUsersNumber;

            Connection.Open();
            SqlCommand cmd = new SqlCommand("GetNumberOfAppUsers", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            appUsersNumber = (int)cmd.ExecuteScalar();

            Connection.Close();

            return appUsersNumber;

        }

        public bool IsNewAdmin(int accountID)
        {
            Connection.Open();

            string query = "SELECT Type FROM Account WHERE AccountID = @accountID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string type = (string)dr["Type"];

            Connection.Close();

            if (type == "Admin*")
                return true;
            else
                return false;
        }


        // Challenges Managing
        public void AddNewChallenge(byte[] photo, string name, string description, int targetMinutes,
                                    string reward, DateTime? dueDate, int workoutID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("AddChallenge", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            if (photo == null)
                cmd.Parameters.Add("@Photo", SqlDbType.Image).Value = DBNull.Value;
            else
                cmd.Parameters.AddWithValue("@Photo", photo);

            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@TargetMinutes", targetMinutes);
            cmd.Parameters.AddWithValue("@Reward", reward);
            cmd.Parameters.AddWithValue("@DueDate", dueDate);
            cmd.Parameters.AddWithValue("@WorkoutID", workoutID);
            cmd.ExecuteNonQuery();

            Connection.Close();

        }

        public void DeleteChallenge(int challengeID)
        {

            Connection.Open();

            string query = "DELETE [UserChallenge] " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE [Challenge].PK_ChallengeID = @challengeID";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@challengeID", challengeID);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

            
            query = "DELETE FROM[Challenge] WHERE[Challenge].PK_ChallengeID = @challengeID";
            cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@challengeID", challengeID);
            reader = cmd.ExecuteReader();

            Connection.Close();

        }



        public void DeleteUser(int accountID)
        {

            string feedbackDelete = "delete from Feedback where FK_Feedback_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(feedbackDelete, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Close();
            Connection.Close();

            string userWorkoutDelete = "delete from UserWorkout where  FK_UserWorkout_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd2 = new SqlCommand(userWorkoutDelete, Connection);
            cmd2.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd2.ExecuteReader();
            dr.Close();
            Connection.Close();

            string accountDelete = "delete from  Account where  AccountID=@accountID;";
            Connection.Open();
            SqlCommand cmd3 = new SqlCommand(accountDelete, Connection);
            cmd3.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd3.ExecuteReader();
            dr.Close();
            Connection.Close();

            string challengeDelete = "delete from  UserChallenge where FK_UserChallenge_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd4 = new SqlCommand(challengeDelete, Connection);
            cmd4.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd4.ExecuteReader();
            dr.Close();
            Connection.Close();

            string planDelete = "delete from  UserPlanDay where FK_UserPlanDay_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd5 = new SqlCommand(planDelete, Connection);
            cmd5.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd5.ExecuteReader();
            dr.Close();
            Connection.Close();

            string weightDelete = "delete from  UserWeight where  FK_UserWeight_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd6 = new SqlCommand(weightDelete, Connection);
            cmd6.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd6.ExecuteReader();
            dr.Close();
            Connection.Close();

            string foodDelete = "delete from  UserFood where FK_UserFood_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd7 = new SqlCommand(foodDelete, Connection);
            cmd7.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd7.ExecuteReader();
            dr.Close();
            Connection.Close();

            string userDelete = "delete from [User] where PK_UserID=@accountID;";
            Connection.Open();
            SqlCommand cmd8 = new SqlCommand(userDelete, Connection);
            cmd8.Parameters.AddWithValue("@accountID", accountID);
            dr = cmd8.ExecuteReader();
            dr.Close();
            Connection.Close();

        }

        public List<UserModel> SearchForUser(string search)
        {
            List<UserModel> AllUsers = new List<UserModel>();

            Connection.Open();
            string query = "select PK_UserID, Photo , FirstName , LastName , Username, Email " +
                            "from [User] inner join Account on PK_UserID = AccountID " +
                            "where FirstName like '%' + @search + '%' " +
                            "or LastName like '%' + @search + '%' " +
                            "or Username like '%' + @search + '%' " +
                            "or Email like '%' + @search + '%'";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@search", search);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserModel temp = new UserModel();

                temp.ID = (int)dr["PK_UserID"];

                if (dr["Photo"] != DBNull.Value)
                    temp.ProfilePhoto.ByteArray = (byte[])dr["Photo"];

                temp.FirstName = dr["FirstName"].ToString();
                temp.LastName = dr["LastName"].ToString();
                temp.Email = dr["Email"].ToString();

                AllUsers.Add(temp);

            }

            Connection.Close();

            return AllUsers;
        }
       

        // Calories Card Modified

        public string GetTodayDate()
        {
            Connection.Open();

            // Using convert to get the day only from the getdate function
            SqlCommand cmd = new SqlCommand("select Convert(date, getdate())", Connection);
            string dateOfToday = cmd.ExecuteScalar().ToString();
            Connection.Close();
            return dateOfToday;
        }


        public string GetLastWeightDate(int accountID)
        {
            Connection.Open();
            string dateTime = "";
            SqlCommand cmd10 = new SqlCommand("select convert(Date,[Date]) as date  from UserWeight where FK_UserWeight_UserID = @id Order by [Date] ", Connection);
            cmd10.Parameters.AddWithValue("@id", accountID);
            SqlDataReader Reader1 = cmd10.ExecuteReader();
            Reader1.Read();
            dateTime = Reader1["date"].ToString();
            Reader1.Close();
            Connection.Close();

            return dateTime;

        }


        public double CalroiesNeeded(UserModel currentUser)
        {
            double SWeight = currentUser.Weight;
            double SHeight = currentUser.Height;
            double SAge = currentUser.Age;

            if (currentUser.Gender == "Female")
            {
                double Femalecalculate = 665 + (9.6 * (SWeight)) + (1.8 * (SHeight)) - (4.7 * (SAge));
                return Femalecalculate;
            }
            else
            {
                double Malecalculate = 66 + (13.7 * (SWeight)) + (1.8 * (SHeight)) - (4.7 * (SAge));
                return Malecalculate;
            }
        }

        public double CalroiesGainedToday(int accountID)
        {
            Connection.Open();
            double SumOfCaloriesGained = 0;
            SqlCommand cmd5 = new SqlCommand("select SUM(CaloriesGained)[0] from UserFood Where FK_UserFood_UserID=@id AND convert(date,DateOfToday) = convert(date ,getdate());", Connection);
            cmd5.Parameters.AddWithValue("@id", accountID);
            if (cmd5.ExecuteScalar().ToString() != "")
            {
                double Calorie = (double)cmd5.ExecuteScalar();
                SumOfCaloriesGained = Calorie;
                Connection.Close();
                return SumOfCaloriesGained;

            }

            else
            {
                Connection.Close();
                return SumOfCaloriesGained;
            }
        }

        public double CalroiesLostToday(int accountID)
        {
            Connection.Open();
            double SumOfCaloriesLost = 0;
            SqlCommand cmd6 = new SqlCommand("select SUM(CaloriesLost)[0] from UserWorkout Where FK_UserWorkout_UserID =@id AND convert(date,DateOfToday)=convert (date ,getdate())", Connection);
            cmd6.Parameters.AddWithValue("@id", accountID);
            if (cmd6.ExecuteScalar().ToString() != "")
            {

                double calorie = (double)cmd6.ExecuteScalar();
                SumOfCaloriesLost = calorie;
                Connection.Close();
                return SumOfCaloriesLost;

            }

            else
            {
                Connection.Close();
                return SumOfCaloriesLost;
            }

        }



        private string CalroiesGainedDuetoLastMeal(int accountID)
        {
            // Get last meal date
            DateTime? date = null;
            Connection.Open();
            SqlCommand cmd21 = new SqlCommand("select DateOfToday from UserFood Where FK_UserFood_UserID = @id Order by DateOfToday Desc", Connection);
            cmd21.Parameters.AddWithValue("@id", accountID);
            SqlDataReader READER = cmd21.ExecuteReader();
            while (READER.Read())
            {
                date = (DateTime?)READER["DateOfToday"];
                break;
            }
            READER.Close();
            Connection.Close();

            // Get last meal calories
            string caloriesgain = "0";
            Connection.Open();
            SqlCommand cmd4 = new SqlCommand("select SUM(CaloriesGained)[0] from UserFood Where FK_UserFood_UserID =@id AND DateOfToday=@date", Connection);
            cmd4.Parameters.AddWithValue("@id", accountID);
            cmd4.Parameters.AddWithValue("@date", date);
            double Calorie = (double)cmd4.ExecuteScalar();
            SqlDataReader dr = cmd4.ExecuteReader();
            while (dr.Read())
            {
                caloriesgain = Calorie.ToString();
            }
            dr.Close();
            Connection.Close();
            return caloriesgain;
        }


        private string CaloriesLostInpreviousWorkout(int accountID)
        {
            // Get date of the previous workout
            DateTime? workoutLastDate = null;
            Connection.Open();
            SqlCommand cmd22 = new SqlCommand("select DateOfToday from UserWorkout Where FK_UserWorkout_UserID=@id Order by DateOfToday Desc", Connection);
            cmd22.Parameters.AddWithValue("@id", accountID);
            SqlDataReader READER1 = cmd22.ExecuteReader();
            while (READER1.Read())
            {
                workoutLastDate = (DateTime)READER1["DateOfToday"];
                break;
            }
            READER1.Close();
            Connection.Close();


            // Get it's calories
            string calorieslost = "0";
            Connection.Open();
            SqlCommand cmd5 = new SqlCommand("select SUM(CaloriesLost)[0] from UserWorkout Where FK_UserWorkout_UserID=@id And DateOfToday=@date", Connection);
            cmd5.Parameters.AddWithValue("@date", workoutLastDate);
            cmd5.Parameters.AddWithValue("@id", accountID);
            double Calorie1 = (double)cmd5.ExecuteScalar();
            SqlDataReader dr2 = cmd5.ExecuteReader();
            while (dr2.Read())
            {
                calorieslost = Calorie1.ToString();
            }
            dr2.Close();
            Connection.Close();

            return calorieslost;
        }


        public void WeightCalc(UserModel currentUser)
        {
            double caloriesGained = double.Parse(CalroiesGainedDuetoLastMeal(currentUser.ID));
            double caloriesLost = double.Parse(CaloriesLostInpreviousWorkout(currentUser.ID));
            double actualCallories = caloriesGained - caloriesLost;
            double weightCalCulated = actualCallories / (7716.179176470716);
            double neededCalories = CalroiesNeeded(currentUser);
            if (actualCallories > neededCalories)
            {
                currentUser.Weight += Math.Round(weightCalCulated, 2);
            }
            else if (actualCallories < neededCalories)
            {
                currentUser.Weight -= Math.Round(weightCalCulated, 2);
            }

            Connection.Open();
            SqlCommand cmd11 = new SqlCommand("AddNewWeight", Connection);
            cmd11.CommandType = CommandType.StoredProcedure;
            cmd11.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            cmd11.Parameters.Add(new SqlParameter("@AddedWeight", currentUser.Weight));
            SqlDataReader DR1 = cmd11.ExecuteReader();
            DR1.Close();
            Connection.Close();
        }


    }
}
