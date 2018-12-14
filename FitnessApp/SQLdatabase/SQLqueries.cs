using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System;
using FitnessApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Windows;

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

            Connection.Close();

            // Info from Weight Table
            currentUser.Weight = GetLastWeight(userID);

            Connection.Open();

            // Info from Accounts Table
            string query3 = "SELECT Email, Password FROM [Account] WHERE AccountID = @userID";
            SqlCommand cmd3 = new SqlCommand(query3, Connection);
            cmd3.Parameters.AddWithValue("@userID", userID);
            SqlDataReader dr3 = cmd3.ExecuteReader();

            dr3.Read();
            currentUser.Email    = dr3["Email"]   .ToString();
            currentUser.Password = dr3["Password"].ToString();
            dr3.Close();


            string query4 = "SELECT FLOOR (DATEDIFF (DAY, BirthDate, GETDATE()) / 365.25) " +
                            "FROM [User] WHERE PK_UserID = @userID";
            SqlCommand cmd4 = new SqlCommand(query4, Connection);
            cmd4.Parameters.AddWithValue("@userID", userID);

            currentUser.Age = Convert.ToInt16(cmd4.ExecuteScalar());

            Connection.Close();

            return currentUser;
        }

        public double GetLastWeight(int userID)
        {
            Connection.Open();

            string query = "SELECT Weight " +
                           "FROM [UserWeight] " +
                           "WHERE FK_UserWeight_UserID = @UserId";

            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@UserId", userID);

            double userWeight = (double)cmd.ExecuteScalar();

            Connection.Close();

            return userWeight;
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
            List<ChallengeModel> allChallengeModels = new List<ChallengeModel>();

            Connection.Open();

            string query = "SELECT [Challenge].*,[UserChallenge].FK_UserChallenge_UserID " +
                           "FROM [Challenge] Left JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "AND FK_UserChallenge_UserID = " + accountID;

            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ChallengeModel temp = new ChallengeModel();
                temp.ID             = (int)reader["PK_ChallengeID"];
                //temp.image        
                temp.Name           = reader["Name"].ToString();
                temp.Description    = reader["Description"].ToString();
                temp.TargetMinutes  = (int)reader["TargetMinutes"];
                temp.Reward         = reader["Reward"].ToString();
                temp.DueDate        = reader["DueDate"].ToString();
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
            Connection.Open();
            string query = "SELECT [Challenge].*,[UserChallenge].FK_UserChallenge_UserID " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE FK_UserChallenge_UserID = " + accountID;

            List<ChallengeModel> joinedChallengeModels = new List<ChallengeModel>();
            SqlCommand cmd = new SqlCommand(query, Connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ChallengeModel temp = new ChallengeModel();

                temp.ID             = (int)reader["PK_ChallengeID"];
                //temp.image        
                temp.Name           = reader["Name"].ToString();
                temp.Description    = reader["Description"].ToString();
                temp.TargetMinutes  = (int)reader["TargetMinutes"];
                temp.Reward         = reader["Reward"].ToString();
                temp.DueDate        = reader["DueDate"].ToString();
                temp.WorkoutType    = (int)reader["FK_Challenge_WorkoutID"];
                
                if (reader["FK_UserChallenge_UserID"] != DBNull.Value)
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
                           "(FK_UserChallenge_UserID, FK_UserChallenge_ChallengeID, JoiningDate) " +
                            "Values (" + accountID + ", " + ChallengeID + ", Convert(date, getdate()))";

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

        public int GetChallengeProgress(int accountID, string joiningDate, string endDate, int workoutType)
        {
            Connection.Open();

            string query = "SELECT SUM(MinutesOfWork) " +
                           "From UserWorkout " +
                           "WHERE FK_UserWorkout_UserID = " + accountID + " " +
                           "AND DateOfToday Between '" + joiningDate + "' and '" + endDate + "' " +
                           "AND FK_UserWorkout_WorkoutID = " + workoutType;

            SqlCommand cmd = new SqlCommand(query, Connection);

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int progress;
            if (reader[0] != DBNull.Value)
                progress = (int)reader[0];
            else
                progress = -1;

            Connection.Close();

            return progress;
        }

        public string GetChallengeJoiningDate(int accountID, int challengeID)
        {
            Connection.Open();

            string query = "SELECT JoiningDate " +
                           "FROM [UserChallenge] " +
                           "WHERE FK_UserChallenge_UserID = " + accountID + " " +
                           "AND FK_UserChallenge_ChallengeID = " + challengeID;

            SqlCommand cmd = new SqlCommand(query, Connection);

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            string joiningDate = reader["JoiningDate"].ToString();

            Connection.Close();

            return joiningDate;
        }


        // Plans queries and functions.
        public List<PlanModel> LoadPlans(int accountID)
        {
            Connection.Open();
            string query = "SELECT [Plan].*,[User].PK_UserID " + 
                           "FROM [Plan] Left JOIN [User] " +
                           "ON [Plan].PK_PlanID = [User].FK_User_PlanID";

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
            cmd.ExecuteReader();
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
            cmd.ExecuteReader();
            Connection.Close();
        }


        // Home Page queries and Functions

        public List<double> GetWeightValues(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from UserWeight where FK_UserWeight_UserID = @UserId", Connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", accountID);

            int countUserWeightInputs = (int)cmd.ExecuteScalar();

            SqlCommand CommandString = new SqlCommand("select Weight, DATENAME(month, Date)[0] from UserWeight where FK_UserWeight_UserID = @UserId order by Date", Connection);
            CommandString.CommandType = CommandType.Text;
            CommandString.Parameters.AddWithValue("@UserId", accountID);

            SqlDataReader ReaderString = CommandString.ExecuteReader();


            List<double> weightValues = new List<double>();

            if (countUserWeightInputs < 10)
            {
                for (int i = 0; ReaderString.Read(); i++)
                {
                    if (ReaderString.HasRows == true)
                    {
                        weightValues.Add((double)ReaderString["Weight"]);
                    }
                }
            }
            else
            {
                for (int i = 0; ReaderString.Read() && i < 10; i++)
                {
                    if (ReaderString.HasRows == true)
                    {
                        weightValues.Add((double)ReaderString["Weight"]);
                    }

                }
            }

            Connection.Close();

            return weightValues;
        }

        public List<string> GetWeightDateValues(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select count(*) from UserWeight where FK_UserWeight_UserID = @UserId", Connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", accountID);

            int countUserWeightInputs = (int)cmd.ExecuteScalar();

            SqlCommand CommandString = new SqlCommand("select Weight, DATENAME(month, Date)[0] from UserWeight where FK_UserWeight_UserID = @UserId order by Date", Connection);
            CommandString.CommandType = CommandType.Text;
            CommandString.Parameters.AddWithValue("@UserId", accountID);

            SqlDataReader ReaderString = CommandString.ExecuteReader();

            List<string> dateValues = new List<string>();

            if (countUserWeightInputs < 10)
            {
                for (int i = 0; ReaderString.Read(); i++)
                {
                    if (ReaderString.HasRows == true)
                        dateValues.Add((string)ReaderString["0"]);
                }
            }
            else
            {
                for (int i = 0; ReaderString.Read() && i < 10; i++)
                {
                    if (ReaderString.HasRows == true)
                        dateValues.Add((string)ReaderString["0"]);
                }
            }

            Connection.Close();

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

        public double GetTotalWeightLostPerWeek(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select SUM(Weight) from UserWeight where DATEPART(WEEK,Date) = DATEPART(WEEK,GETDATE())  AND FK_UserWeight_UserID = @id; ", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);

            double weightValue = Math.Round((double)cmd.ExecuteScalar(), 2);
           
            Connection.Close();

            return weightValue;
        }

        public double GetTotalWeightLostPerMonth(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select SUM(Weight) from UserWeight where DATEPART(month,Date)=DATEPART(month,GETDATE())  AND FK_UserWeight_UserID = @id; ", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);

            double weightValue = Math.Round((double)cmd.ExecuteScalar(), 2);

            Connection.Close();

            return weightValue;
        }

        public double GetTotalWeightLostPerYear(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select SUM(Weight) from UserWeight where DATEPART(year,Date)=DATEPART(year,GETDATE())  AND FK_UserWeight_UserID = @id; ", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);

            double weightValue = Math.Round((double)cmd.ExecuteScalar(), 2);

            Connection.Close();

            return weightValue;
        }

        public double GetAverageWeightLostPerWeek(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select Avg(Weight) from UserWeight where DATEPART(WEEK,Date) = DATEPART(WEEK,GETDATE())  AND FK_UserWeight_UserID = @id; ", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);

            double weightValue = Math.Round((double)cmd.ExecuteScalar(), 2);

            Connection.Close();

            return weightValue;
        }

        public double GetAverageWeightLostPerMonth(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select Avg(Weight) from UserWeight where DATEPART(MONTH,Date)=DATEPART(MONTH,GETDATE())  AND FK_UserWeight_UserID = @id; ", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);

            double weightValue = Math.Round((double)cmd.ExecuteScalar(), 2);

            Connection.Close();

            return weightValue;
        }

        public double GetAverageWeightLostPerYear(int accountID)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select Avg(Weight) from UserWeight where DATEPART(YEAR,Date)=DATEPART(YEAR,GETDATE()) AND FK_UserWeight_UserID = @id; ", Connection);
            cmd.Parameters.AddWithValue("@id", accountID);

            double weightValue = Math.Round((double)cmd.ExecuteScalar(), 2);

            Connection.Close();

            return weightValue;
        }

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
                totalCaloriesLost = ((currentUser.Age * 0.074) - (currentUser.LatestWeight * 0.05741) + (100 * 0.4472) - 20.4022) * (duration / 4.184);
            }
            else
            {
                totalCaloriesLost = ((currentUser.Age * 0.2017) - (currentUser.LatestWeight * 0.09036) + (100 * 0.6309) - 55.0969) * (duration / 4.184);
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

        //////// Joined Plan ////////
        
        // Get Joined Plan ID and Name 
        public int PlanId(int accountID)
        {
            int SQLplanID = 0;
            string query = "select PlanId from [User] where ID= @accountID;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {

                    SQLplanID = (int)dr["PlanId"];

                }
            }
            dr.Close();
            Connection.Close();

            return SQLplanID;

        }

        public string PlanName(int accountID)
        {
            int SQLplanID = PlanId(accountID);
            string SQLplanName = "";
            string query = "select Name from [Plan] where ID=@SQLplanID;";
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
        private string JoinedDate(int accountID)
        {
            string joinedDate = "";
            string query = "select PlanJoiningDate from [User] where ID= @accountID ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@accountID", accountID);

            try
            {
                joinedDate = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Connection.Close();

            return joinedDate;
        }

        public int GetDate(int accountID)
        {
            string joinedDate = JoinedDate(accountID);
            string dateDiff = "select DATEDIFF(day, @joinedDate , getdate()) from UserPlanDay where UserId = @accountID ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(dateDiff, Connection);
            cmd.Parameters.AddWithValue(" @joinedDate", joinedDate);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            int dayNumber = 0;
            try
            {
                dayNumber = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Connection.Close();

            // to make it begin with 1 instead of zero
            int SQLplanDay = dayNumber + 1;

            return SQLplanDay;
        }



        // Get Joined Plan items' Descriptions
        public string BreakfastDiscription(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            int SQLplanID = PlanId(accountID);
            string breakfastDiscription = "";
            string query = "select BreakfastDescription from PlanDayDescription where PlanId=@SQLplanID AND DayNumber = @SQLplanDay ;";
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

        public string LucnchDiscription(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            int SQLplanID = PlanId(accountID);
            string lucnchDiscription = "";
            string query = "select LunchDescription from PlanDayDescription where PlanId=@SQLplanID AND DayNumber = @SQLplanDay ;";
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

        public string DinnerDiscription(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            int SQLplanID = PlanId(accountID);
            string dinnerDiscription = "";
            string query = "select DinnerDescription from PlanDayDescription where PlanId=@SQLplanID AND DayNumber = @SQLplanDay ;";
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

        public string WorkoutDiscription(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            int SQLplanID = PlanId(accountID);
            string workoutDiscription = "";
            string query = "select WorkoutDescription from PlanDayDescription where PlanId=@SQLplanID AND DayNumber = @SQLplanDay ;";
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
        public bool SQLbreakfast(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            bool SqlBreakfast = false;
            string query = " select BreakfastIsDone from UserPlanDay where UserId = @accountID and DayNumber = @SQLplanDay ;";
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

        public bool SQLlunch(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            bool SqlLunch = false;
            string query = " select LunchIsDone from UserPlanDay where UserId = @accountID and DayNumber = @SQLplanDay ;";
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

        public bool SQLdinner(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            bool SqlDinner = false;
            string query = " select DinnerIsDone from UserPlanDay where UserId=@accountID and DayNumber = @SQLplanDay ;";
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

        public bool SQLworkout(int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            bool SqlWorkout = false;
            string query = " select WorkoutsIsDone from UserPlanDay where UserId= @accountID and DayNumber = @SQLplanDay ;";
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

        public void ModifyBreakfast(bool checkedBreakfast, int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            string query = "Update UserPlanDay SET  BreakfastIsDone=@checkedBreakfast where  UserId= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedBreakfast", checkedBreakfast);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
        }

        public void ModifyLunch(bool checkedLunch, int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            string query = "Update UserPlanDay SET  LunchIsDone=@checkedLunch where  UserId= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedLunch", checkedLunch);
            cmd.Parameters.AddWithValue("@accountID", accountID);
            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
        }

        public void ModifyDinner(bool checkedDinner, int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            string query = "Update UserPlanDay SET  DinnerIsDone=@checkedDinner where  UserId= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedDinner", checkedDinner);
            cmd.Parameters.AddWithValue("@accountID", accountID);

            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
        }

        public void ModifyWorkout(bool checkedWorkout, int accountID)
        {
            int SQLplanDay = GetDate(accountID);
            string query = "Update UserPlanDay SET  WorkoutsIsDone=@checkedWorkout where  UserId= @accountID and DayNumber = @SQLplanDay ;";
            Connection.Open();
            SqlCommand cmd = new SqlCommand(query, Connection);
            cmd.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            cmd.Parameters.AddWithValue("@checkedWorkout", checkedWorkout);
            cmd.Parameters.AddWithValue("@accountID", accountID);

            try
            {

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connection.Close();
        }
    }
}
