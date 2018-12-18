using FitnessApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.SQLserver
{
    public static class Database
    {

        ////////// SQL connection string //////////
        // [IMPORTANT] Add your server name to ServerDetails Class.
        private static SqlConnection connection = new SqlConnection(ServerDetails.ConnectionString);
        private static string query;
        private static SqlCommand command;
        private static SqlDataReader dataReader;



        ////////// Local Fields //////////
        public static int accountID;
        public static string accountType;




        ////////// Helper Functions //////////

        // Encrypt given password
        public static string EncryptPassword(string password)
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


        // Generate a Random Password: Used When adding a new Admin 
        private static string GenerateRandomPassword()
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


        // Send email to user function (gmail only)
        private static void SendUserEmail(string email, string name)
        {
            MailMessage message = new MailMessage();

            // Reciever's Email
            message.To.Add(email);

            // Email Subject
            message.Subject = "Welcome To FitnessApp";

            // Sender's Email
            message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");

            // Email Body
            message.IsBodyHtml = true;
            string htmlBody = "<body>" +
                              "<img src=https://bit.ly/2PI1mx4>" +
                              "<p style=\"float: left; \">" +
                              "<img src=https://bit.ly/2STDZ62 height=\"100px\" width=\"100px\" hspace=\"5\" style=\"border - right: 1px solid black;\">" +
                              "</p> " +
                              "<p style=\"padding: 20px; \"> " +
                              " <font size=\"5px\" color=\"#0F88A8\">" +
                              "<b> Welcome " + name + " , </b>" +
                              "</font>" +
                              "<br>" +
                              "Thank you for choosing FitnessApp!" +
                              "<br> <br> " +
                              "Ready for <b>RESHAPING</b> &#9889;" +
                              "<br><br>" +
                              " <font size=\"2 px\">" +
                              "feel free to contact us  &#9786; " +
                              ",<br> " +
                              "<a href=\"fitness.weightlossapp @gmail.com\">" +
                              "fitness.weightlossapp@gmail.com</font>" +
                              "</p>" +
                              "</div>" +
                              "</body>";
            message.Body = htmlBody;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
            smtp.Send(message);
        }


        // Send Email to a recently added user
        public static void SendAdminEmail(string email, string randomPass)
        {

            MailMessage message = new MailMessage();
            // Reciever's Email
            message.To.Add(email);

            // Email Subject
            message.Subject = "Welcome To FitnessApp";

            // Sender's Email
            message.From = new MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");

            // Email Body
            message.IsBodyHtml = true;
            string htmlBody = "<body>" +
                                "<img src=https://bit.ly/2PI1mx4> " +
                                "<p style=\"float: left; \">" +
                                "<img src=https://bit.ly/2STDZ62 height=\"100px\" width=\"100px\" hspace=\"5\" style=\"border - right: 1px solid black;\">" +
                                "</p> " +
                                "<p style=\"padding: 20px; \">  " +
                                "<font size=\"5px\" color=\"#0F88A8\">" +
                                "<b> Welcome  </b>" +
                                "</font>" +
                                "<br>" +
                                "<b> We are so grateful for having you ! &#9786; </b>" +
                                "<br> <br>" +
                                "<font size =\"3px\"> Please change your password as soon as possible : </font> " + randomPass +
                                "<br> <br> <br>" +
                                "<em>Best regards,</em>" +
                                "<br>FitnessApp Team </p>" +
                                "</div> " +
                              "</body>";

            message.Body = htmlBody;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("fitness.weightlossapp@gmail.com", "m3leshyFitness21");
            smtp.Send(message);
        }





        ////////// Queries AND Main Functions //////////

        // Sign in query AND function.
        public static bool IsUserFound(string email, string password)
        {
            // Encrypt Password
            string encryptedPassword = EncryptPassword(password);

            // Create Command
            command = new SqlCommand("SELECT * FROM [Account] WHERE Email = @email AND Password = @password", connection);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", encryptedPassword);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    // Assigns the account ID AND account type to the global variables.
                    accountID = (int)dataReader["AccountID"];
                    accountType = (string)dataReader["Type"];
                    connection.Close();

                    // If user exists, return true.
                    return true;
                }
            }

            connection.Close();

            // If user doesn't exist or any other case, return false.
            return false;
        }

        // Check if the Username is taken or not; because Usernames must be unique
        public static bool IsUsernameTaken(string username)
        {
            // Create Command
            command = new SqlCommand("SELECT Username FROM [User] WHERE Username = @username ;", connection);
            command.Parameters.AddWithValue("@username", username);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    connection.Close();

                    // If the Username is taken exists, return true.
                    return true;
                }

            }

            connection.Close();

            // If the Username is not taken, return false.
            return false;
        }

        // Check if the Entered Email while signing up is taken or not
        public static bool IsEmailTaken(string email)
        {
            // Create Command
            command = new SqlCommand("SELECT Email FROM [Account] WHERE Email = @email ;", connection);
            command.Parameters.AddWithValue("@email", email);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    connection.Close();

                    // If the email is taken exists, return true.
                    return true;
                }

            }

            connection.Close();

            // If the email is not taken, return false.
            return false;
        }

        // Sign up function
        public static void AddUser(byte[] profilePhoto, string firstName, string lastName,
                            string username, string email, string password,
                            string gender, string birthDate, double weight, double height,
                            double targetWeight, double kilosToLosePerWeek, double workoutsPerWeek, double workoutHoursPerDay)
        {
            // Password Encryption
            string encryptedPassword = EncryptPassword(password);

            // Create Query amd Command
            query = "INSERT INTO [User] (Photo, FirstName, LastName, Username, BirthDate, Gender, " +
                            "TargetWeight, Height, KilosToLosePerWeek, WorkoutsPerWeek, WorkoutHoursPerDay)" +
                            "VALUES(@photo ,'" + firstName + "','" + lastName + "', '" + username + "','" + birthDate + "','" + gender + "','" +
                            targetWeight + "','" + height + "','" + kilosToLosePerWeek + "','" + workoutsPerWeek + "', '" + workoutHoursPerDay + "') ;";
            command = new SqlCommand(query, connection);

            if (profilePhoto == null)
                command.Parameters.Add("@photo", SqlDbType.Image).Value = DBNull.Value;
            else
                command.Parameters.AddWithValue("@photo", profilePhoto);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
            }

            connection.Close();


            // Get User's ID
            // Create Query amd Command
            query = "SELECT PK_UserID FROM [User] WHERE Username = @username";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    accountID = (int)dataReader["PK_UserID"];
                }

            }

            connection.Close();


            // INSERT User's email AND password
            // Create Query amd Command
            query = "INSERT INTO [Account](AccountID, Email, Password, Type) " +
                            "VALUES('" + accountID + "','" + email + "','" + encryptedPassword + "','User');";
            command = new SqlCommand(query, connection);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
            }

            connection.Close();


            // INSERT User's weight into multiValued weight table
            // Create Query amd Command
            query = "INSERT INTO UserWeight(FK_UserWeight_UserID, Weight, Date) " +
                            "VALUES('" + accountID + "','" + weight + "', GETDATE());";
            command = new SqlCommand(query, connection);

            // Open connection AND Start Reading
            connection.Open();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
            }

            connection.Close();


            // Sending email to gmails only
            if (email.Contains("gmail"))
            {
                SendUserEmail(email, firstName);
            }
        }




        // Load all User's Data
        public static UserModel GetUserData(int userID)
        {
            UserModel currentUser = new UserModel();

            connection.Open();

            // Info FROM User Table
            query = "SELECT * FROM [User] WHERE PK_UserID = @userID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", userID);
            dataReader = command.ExecuteReader();

            dataReader.Read();

            if (dataReader["Photo"] != DBNull.Value)
                currentUser.ProfilePhoto.ByteArray = (byte[])dataReader["Photo"];

            currentUser.FirstName = dataReader["FirstName"].ToString();
            currentUser.LastName = dataReader["LastName"].ToString();
            currentUser.Username = dataReader["Username"].ToString();
            currentUser.Gender = dataReader["Gender"].ToString();
            currentUser.BirthDate = dataReader["BirthDate"].ToString();
            currentUser.Height = (double)dataReader["Height"];
            currentUser.TargetWeight = (double)dataReader["TargetWeight"];
            currentUser.KilosToLosePerWeek = (double)dataReader["KilosToLosePerWeek"];
            currentUser.WorkoutsPerWeek = (double)dataReader["WorkoutsPerWeek"];
            currentUser.WorkoutHoursPerDay = (double)dataReader["WorkoutHoursPerDay"];

            dataReader.Close();

            // Info FROM Weight Table
            query = "SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID = @userID ORDER BY Date DESC";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", userID);
            currentUser.Weight = (double)command.ExecuteScalar();


            // Info FROM Accounts Table
            query = "SELECT Email, Password FROM [Account] WHERE AccountID = @userID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", userID);
            dataReader = command.ExecuteReader();

            dataReader.Read();
            currentUser.Email = dataReader["Email"].ToString();
            currentUser.Password = dataReader["Password"].ToString();
            dataReader.Close();

            // Get User Age
            query = "SELECT FLOOR (DATEDIFF (DAY, BirthDate, GETDATE()) / 365.25) " +
                            "FROM [User] WHERE PK_UserID = @userID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", userID);

            currentUser.Age = Convert.ToInt16(command.ExecuteScalar());

            connection.Close();

            return currentUser;
        }

        // UPDATE User Profile
        public static void UpdateUserProfile(UserModel currentUser)
        {
            connection.Open();

            query = "UPDATE [User] SET Photo = @Photo WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", currentUser.ID);
            command.Parameters.AddWithValue("@Photo", currentUser.ProfilePhoto.ByteArray);
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "INSERT INTO [UserWeight] VALUES (@UserId, @AddedWeight, GETDATE())";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@userID", accountID));
            command.Parameters.Add(new SqlParameter("@AddedWeight", currentUser.Weight));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET Height = @Height WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@Height", currentUser.Height));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET TargetWeight = @TargetWeight WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@TargetWeight", currentUser.TargetWeight));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET KilosToLosePerWeek = @KilosToLosePerWeek WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@KilosToLosePerWeek", currentUser.KilosToLosePerWeek));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET WorkoutsPerWeek = @WorkoutsPerWeek WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@WorkoutsPerWeek", currentUser.WorkoutsPerWeek));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET WorkoutHoursPerDay = @WorkoutHoursPerDay WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@WorkoutHoursPerDay", currentUser.WorkoutHoursPerDay));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            connection.Close();
        }

        // UPDATE User Account
        public static void UpdateUserAccount(UserModel currentUser)
        {
            connection.Open();

            query = "UPDATE [User] SET FirstName = @FirstName WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@FirstName", currentUser.FirstName));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET LastName = @LastName WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@LastName", currentUser.LastName));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [User] SET Username = @Username WHERE PK_UserID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@Username", currentUser.Username));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [Account] SET Email = @Email WHERE AccountID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@Email", currentUser.Email));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            connection.Close();
        }

        // UPDATE User Password
        public static void UpdateUserPassword(UserModel currentUser)
        {
            connection.Open();

            query = "UPDATE [Account] SET[Password] = @Password WHERE AccountID = @UserId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@UserId", currentUser.ID));
            command.Parameters.Add(new SqlParameter("@Password", currentUser.Password));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            connection.Close();
        }

        // Save Feedback
        public static void SaveFeedback(int userID, int rating, string feedback)
        {
            connection.Open();

            query = "INSERT INTO [Feedback] (FK_Feedback_UserID, Rating, Feedback) " +
                           "VALUES('" + userID + "','" + rating + "','" + feedback + "')";

            command = new SqlCommand(query, connection);
            command.ExecuteReader();

            connection.Close();
        }




        // Challenges queries AND functions.
        public static List<ChallengeModel> GetAllChallenges(int accountID)
        {

            // Remove All Overdue Challenges before reading data
            RemoveOverdueChallenges();

            List<ChallengeModel> allChallengeModels = new List<ChallengeModel>();

            connection.Open();

            query = "SELECT [Challenge].*,[UserChallenge].* " +
                           "FROM [Challenge] Left JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "AND FK_UserChallenge_UserID = " + accountID;

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                ChallengeModel temp = new ChallengeModel();
                temp.ID = (int)dataReader["PK_ChallengeID"];

                if (dataReader["Photo"] != DBNull.Value)
                    temp.Photo.ByteArray = (byte[])dataReader["Photo"];

                temp.Name = dataReader["Name"].ToString();
                temp.Description = dataReader["Description"].ToString();
                temp.TargetMinutes = (int)dataReader["TargetMinutes"];
                temp.Reward = dataReader["Reward"].ToString();
                temp.DueDate = dataReader["DueDate"].ToString().ToString().Split(' ')[0];
                temp.WorkoutType = (int)dataReader["FK_Challenge_WorkoutID"];

                if (dataReader["FK_UserChallenge_UserID"] != DBNull.Value)
                    temp.IsJoined = true;
                else
                    temp.IsJoined = false;

                allChallengeModels.Add(temp);
            }
            connection.Close();

            return allChallengeModels;
        }

        public static List<ChallengeModel> GetJoinedChallenges(int accountID)
        {
            // Remove All Overdue Challenges before reading data
            RemoveOverdueChallenges();

            List<ChallengeModel> joinedChallengeModels = new List<ChallengeModel>();

            connection.Open();

            query = "SELECT [Challenge].*,[UserChallenge].* " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE FK_UserChallenge_UserID = " + accountID;

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                ChallengeModel temp = new ChallengeModel();

                temp.ID = (int)dataReader["PK_ChallengeID"];
                temp.Name = dataReader["Name"].ToString();
                temp.Description = dataReader["Description"].ToString();
                temp.TargetMinutes = (int)dataReader["TargetMinutes"];
                temp.Reward = dataReader["Reward"].ToString();
                temp.DueDate = dataReader["DueDate"].ToString().Split(' ')[0];
                temp.WorkoutType = (int)dataReader["FK_Challenge_WorkoutID"];
                temp.Progress = (int)dataReader["Progress"];
                temp.IsJoined = true;

                joinedChallengeModels.Add(temp);
            }

            connection.Close();

            return joinedChallengeModels;
        }

        public static void JoinChallenge(int accountID, int ChallengeID)
        {
            connection.Open();
            query = "INSERT INTO [UserChallenge] " +
                           "(FK_UserChallenge_UserID, FK_UserChallenge_ChallengeID, JoiningDate, Progress) " +
                            "VALUES (" + accountID + ", " + ChallengeID + ", GETDATE(), 0)";

            command = new SqlCommand(query, connection);
            command.ExecuteReader();

            connection.Close();
        }

        public static void UnjoinChallenge(int accountID, int ChallengeID)
        {
            connection.Open();
            query = "DELETE [UserChallenge] " +
                           "WHERE [UserChallenge].FK_UserChallenge_UserID = " + accountID + " " +
                           "AND [UserChallenge].FK_UserChallenge_ChallengeID = " + ChallengeID;

            command = new SqlCommand(query, connection);
            command.ExecuteReader();

            connection.Close();
        }

        public static void RemoveOverdueChallenges()
        {

            connection.Open();

            query = "DELETE [UserChallenge] " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE [Challenge].DueDate <= GETDATE()";

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "DELETE FROM[Challenge] WHERE[Challenge].DueDate <= GETDATE()";

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            connection.Close();

        }

        public static void UpdateChallengesProgress(int accountID, string workout, double duration)
        {
            connection.Open();

            query = "UPDATE [UserChallenge] " +
                           "SET progress += @workoutDuration " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "RIGHT JOIN [Workout] " +
                           "ON [Challenge].Fk_Challenge_WorkoutID = [Workout].PK_WorkoutID " +
                           "WHERE FK_UserChallenge_UserID = @userID " +
                           "AND GETDATE() Between JoiningDate AND DueDate " +
                           "AND [Workout].[Name] = @workoutName";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@workoutDuration", duration);
            command.Parameters.AddWithValue("@userID", accountID);
            command.Parameters.AddWithValue("@workoutName", workout);
            command.ExecuteReader();

            connection.Close();
        }



        // Plans queries AND functions.
        public static List<PlanModel> GetPlans(int accountID)
        {
            connection.Open();
            query = "SELECT [Plan].*,[User].PK_UserID " +
                           "FROM [Plan] Left JOIN [User] " +
                           "ON [Plan].PK_PlanID = [User].FK_User_PlanID " +
                           "AND PK_UserID = @userID";

            List<PlanModel> plansModels = new List<PlanModel>();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", accountID);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                PlanModel temp = new PlanModel();

                temp.ID = (int)dataReader["PK_PlanID"];

                if (dataReader["Photo"] != DBNull.Value)
                    temp.Photo.ByteArray = (byte[])dataReader["Photo"];

                temp.Name = dataReader["Name"].ToString();
                temp.Description = dataReader["Description"].ToString();
                temp.Duration = dataReader["Duration"].ToString();
                temp.Hardness = dataReader["Hardness"].ToString();

                if (dataReader["PK_UserID"] != DBNull.Value)
                    temp.IsJoined = true;
                else
                    temp.IsJoined = false;

                plansModels.Add(temp);
            }
            connection.Close();

            return plansModels;
        }

        public static List<DayModel> GetPlanDays(int planID)
        {
            connection.Open();
            query = "SELECT * FROM[PlanDayDescription] " +
                           "WHERE[PlanDayDescription].FK_PlanDayDescription_PlanID = " + planID + " " +
                           "ORDER BY[PlanDayDescription].DayNumber";

            List<DayModel> dayModels = new List<DayModel>();
            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                DayModel temp = new DayModel();

                temp.DayNumber = (int)dataReader["DayNumber"];
                //temp.image
                temp.BreakfastDescription = dataReader["BreakfastDescription"].ToString();
                temp.LunchDescription = dataReader["LunchDescription"].ToString();
                temp.DinnerDescription = dataReader["DinnerDescription"].ToString();
                temp.WorkoutDescription = dataReader["WorkoutDescription"].ToString();

                dayModels.Add(temp);
            }
            connection.Close();

            return dayModels;
        }

        public static bool IsInPlan(int accountID)
        {
            connection.Open();
            query = "SELECT FK_User_PlanID " +
                           "FROM [USER] " +
                           "WHERE PK_UserID = " + accountID;

            command = new SqlCommand(query, connection);

            if (command.ExecuteScalar() != DBNull.Value)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }

        }

        public static void JoinPlan(int accountID, int planID)
        {
            connection.Open();
            query = "UPDATE [User] " +
                           "SET FK_User_PlanID = " + planID + ", " +
                           "PlanJoiningDate = CONVERT(date, GETDATE()) " +
                           "WHERE [User].PK_UserID = " + accountID;

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "INSERT INTO UserPlanDay (FK_UserPlanDay_UserID, DayNumber) VALUES (@userID, 1)";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", accountID);
            dataReader = command.ExecuteReader();

            connection.Close();
        }

        public static void UnjoinPlan(int accountID)
        {
            connection.Open();
            query = "UPDATE [User] " +
                           "SET FK_User_PlanID = NULL, " +
                           "PlanJoiningDate = NULL " +
                           "WHERE [User].PK_UserID = " + accountID;

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "DELETE FROM UserPlanDay WHERE FK_UserPlanDay_UserID = @userID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", accountID);
            dataReader = command.ExecuteReader();

            connection.Close();
        }



        //////// Weight ////////

        // Weight chart
        public static List<double> GetWeightValues(int accountID)
        {
            List<double> weightValues = new List<double>();

            connection.Open();

            SqlCommand CommandString = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID = @userID ORDER BY Date DESC", connection);
            CommandString.CommandType = CommandType.Text;
            CommandString.Parameters.AddWithValue("@userID", accountID);
            dataReader = CommandString.ExecuteReader();

            for (int i = 0; dataReader.Read() && i < 10; i++)
            {
                weightValues.Add((double)dataReader["Weight"]);
            }

            connection.Close();

            // Reverse List
            weightValues.Reverse();

            return weightValues;
        }

        public static List<string> GetWeightDateValues(int accountID)
        {
            List<string> dateValues = new List<string>();

            connection.Open();

            SqlCommand CommandString = new SqlCommand("SELECT FORMAT(Date, 'MMM yy') AS [Date] FROM UserWeight WHERE FK_UserWeight_UserID = @userID ORDER BY Date DESC", connection);
            CommandString.CommandType = CommandType.Text;
            CommandString.Parameters.AddWithValue("@userID", accountID);

            dataReader = CommandString.ExecuteReader();

            for (int i = 0; dataReader.Read() && i < 10; i++)
            {
                dateValues.Add((string)dataReader["Date"]);
            }

            connection.Close();

            // Reverse List
            dateValues.Reverse();

            return dateValues;
        }

        public static void AddNewWeight(double NewWeight, int accountID)
        {
            connection.Open();

            query = "INSERT INTO [UserWeight] VALUES (@userID, @AddedWeight, GETDATE())";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@userID", accountID));
            command.Parameters.Add(new SqlParameter("@AddedWeight", NewWeight));
            command.ExecuteReader();

            connection.Close();
        }

        // Total Weight Lost
        public static double GetTotalWeightLostPerWeek(int accountID)
        {

            double WeekWeightLost = 0;
            List<double> WeekWeight = new List<double>();

            connection.Open();

            command = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID=@id AND DATEPART(WEEK,Date) = DATEPART(WEEK,GETDATE()) ORDER BY Date", connection);
            command.Parameters.AddWithValue("@id", accountID);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                WeekWeight.Add((double)(dataReader["Weight"]));
            }

            connection.Close();

            for (int i = 0; i < (WeekWeight.Count - 1); i++)
            {
                WeekWeightLost += (WeekWeight[i] - WeekWeight[i + 1]);
            }

            return Math.Round(WeekWeightLost, 2);

        }

        public static double GetTotalWeightLostPerMonth(int accountID)
        {

            double MonthWeightLost = 0;
            List<double> MonthWeight = new List<double>();

            connection.Open();

            command = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID=@id AND DATEPART(MONTH,Date) = DATEPART(MONTH,GETDATE()) ORDER BY Date", connection);
            command.Parameters.AddWithValue("@id", accountID);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                MonthWeight.Add((double)(dataReader["Weight"]));
            }

            connection.Close();

            for (int i = 0; i < (MonthWeight.Count - 1); i++)
            {
                MonthWeightLost += (MonthWeight[i] - MonthWeight[i + 1]);
            }

            return Math.Round(MonthWeightLost, 2);

        }

        public static double GetTotalWeightLostPerYear(int accountID)
        {

            double YearWeightLost = 0;
            List<double> YearWeight = new List<double>();

            connection.Open();

            command = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID=@id AND DATEPART(YEAR,Date) = DATEPART(YEAR,GETDATE()) ORDER BY Date", connection);
            command.Parameters.AddWithValue("@id", accountID);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                YearWeight.Add((double)(dataReader["Weight"]));
            }

            connection.Close();

            for (int i = 0; i < (YearWeight.Count - 1); i++)
            {
                YearWeightLost += (YearWeight[i] - YearWeight[i + 1]);
            }

            return Math.Round(YearWeightLost, 2);

        }

        // Average Weight Lost
        public static double GetAverageWeightLostPerWeek(int accountID)
        {
            double WeekWeightLost = 0;
            List<double> WeekWeight = new List<double>();

            connection.Open();

            command = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID=@id AND DATEPART(WEEK,Date) = DATEPART(WEEK,GETDATE()) ORDER BY Date", connection);
            command.Parameters.AddWithValue("@id", accountID);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                WeekWeight.Add((double)(dataReader["Weight"]));
            }

            connection.Close();

            for (int i = 0; i < (WeekWeight.Count - 1); i++)
            {
                WeekWeightLost += (WeekWeight[i] - WeekWeight[i + 1]);
            }

            return Math.Round((WeekWeightLost / WeekWeight.Count), 2);

        }

        public static double GetAverageWeightLostPerMonth(int accountID)
        {
            double MonthWeightLost = 0;
            List<double> MonthWeight = new List<double>();

            connection.Open();

            command = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID=@id AND DATEPART(MONTH,Date) = DATEPART(MONTH,GETDATE()) ORDER BY Date", connection);
            command.Parameters.AddWithValue("@id", accountID);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                MonthWeight.Add((double)(dataReader["Weight"]));
            }

            connection.Close();

            for (int i = 0; i < (MonthWeight.Count - 1); i++)
            {
                MonthWeightLost += (MonthWeight[i] - MonthWeight[i + 1]);
            }

            return Math.Round((MonthWeightLost / MonthWeight.Count), 2);

        }

        public static double GetAverageWeightLostPerYear(int accountID)
        {

            double YearWeightLost = 0;
            List<double> YearWeight = new List<double>();

            connection.Open();

            command = new SqlCommand("SELECT Weight FROM UserWeight WHERE FK_UserWeight_UserID=@id AND DATEPART(YEAR,Date) = DATEPART(YEAR,GETDATE()) ORDER BY Date", connection);
            command.Parameters.AddWithValue("@id", accountID);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                YearWeight.Add((double)(dataReader["Weight"]));
            }

            connection.Close();

            for (int i = 0; i < (YearWeight.Count - 1); i++)
            {
                YearWeightLost += (YearWeight[i] - YearWeight[i + 1]);
            }

            return Math.Round((YearWeightLost / YearWeight.Count), 2);

        }



        //////// Motivational Quote ////////

        public static string GetMotivationalQuote()
        {
            connection.Open();

            query = "SELECT Quote FROM MotivationalQuote " +
                           "WHERE PK_MotivationalQuoteID = DATEPART(DAY,GETDATE())";
            command = new SqlCommand(query, connection);

            string Quote = command.ExecuteScalar().ToString();

            connection.Close();

            return Quote;
        }



        //////// Calories ////////

        public static double GetCaloriesGainedToday(int accountID)
        {
            double caloriesGained = 0;

            connection.Open();

            query = "SELECT SUM(CaloriesGained) " +
                    "FROM UserFood " +
                    "WHERE FK_UserFood_UserID = @id " +
                    "AND convert (date, DateOfToday) = convert (date, getdate())";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", accountID);

            if (command.ExecuteScalar() != DBNull.Value)
                caloriesGained = (double)command.ExecuteScalar();

            connection.Close();

            return caloriesGained;
        }

        public static double GetCaloriesLostToday(int accountID)
        {
            double caloriesLost = 0;

            connection.Open();

            query = "SELECT SUM(CaloriesLost) " +
                    "FROM UserWorkout " +
                    "WHERE FK_UserWorkout_UserID = @id " +
                    "AND convert (date, DateOfToday) = convert (date, getdate())";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", accountID);

            if (command.ExecuteScalar() != DBNull.Value)
                caloriesLost = (double)command.ExecuteScalar();

            connection.Close();

            return caloriesLost;
        }




        //////// Food/Workout ////////

        public static List<String> GetAllFood()
        {
            connection.Open();

            List<String> food = new List<string>();

            command = new SqlCommand("SELECT Name FROM Food", connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string FoodName = dataReader["Name"].ToString();
                food.Add(FoodName);
            }
            dataReader.Close();

            connection.Close();

            return food;
        }

        public static List<String> GetAllWorkouts()
        {
            connection.Open();

            List<String> workouts = new List<string>();

            command = new SqlCommand("SELECT Name FROM Workout", connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string WorkoutName = dataReader["Name"].ToString();
                workouts.Add(WorkoutName);
            }
            dataReader.Close();

            connection.Close();

            return workouts;
        }

        public static void AddFood(string food, double quantity, int accountID)
        {
            connection.Open();

            int foodID = 0;
            double totalCaloriesGained = 0;

            // Calculate Total Calories gained
            command = new SqlCommand("SELECT Type FROM Food WHERE Name=@food", connection);
            command.Parameters.AddWithValue("@food", food);
            string foodType = command.ExecuteScalar().ToString();
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
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
            dataReader.Close();

            // Get Food ID
            command = new SqlCommand("SELECT PK_FoodID FROM Food WHERE Name=@food", connection);
            command.Parameters.AddWithValue("@food", food);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                foodID = (int)(dataReader["PK_FoodID"]);
            }
            dataReader.Close();

            // INSERT Food in UserFood Table
            command = new SqlCommand("INSERT into [UserFood] (FK_UserFood_UserID, FK_UserFood_FoodID, CaloriesGained,DateOfToday) VALUES (@userID, @foodID, @calories, GETDATE())", connection);
            command.Parameters.AddWithValue("@userID", accountID);
            command.Parameters.AddWithValue("@foodID", foodID);
            command.Parameters.AddWithValue("@calories", totalCaloriesGained);
            command.ExecuteReader();

            connection.Close();

        }

        public static void AddWorkout(string workout, double duration, UserModel currentUser)
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

            connection.Open();

            // Get Workout ID
            command = new SqlCommand("SELECT PK_WorkoutID FROM Workout WHERE Name=@name", connection);
            command.Parameters.AddWithValue("@name", workout);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                workoutID = (int)(dataReader["PK_WorkoutID"]);
            }
            dataReader.Close();

            // INSERT Workout in UserWorkout Table
            command = new SqlCommand("INSERT into UserWorkout (FK_UserWorkout_UserID,FK_UserWorkout_WorkoutID,MinutesOfWork,CaloriesLost,DateOfToday) VALUES(@userID, @workoutID, @duration, @calories, GETDATE())", connection);
            command.Parameters.AddWithValue("@userID", currentUser.ID);
            command.Parameters.AddWithValue("@workoutID", workoutID);
            command.Parameters.AddWithValue("@duration", duration);
            command.Parameters.AddWithValue("@calories", Math.Round(totalCaloriesLost, 2));
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static int GetWorkoutID(string workoutName)
        {
            int id = -1;

            connection.Open();
            query = "SELECT PK_WorkoutID FROM Workout WHERE Name=@name;";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", workoutName);
            id = (int)command.ExecuteScalar();
            connection.Close();

            return id;
        }


        //////// Joined Plan ////////

        // Get Joined Plan ID AND Name 
        public static int GetJoinedPlanID(int accountID)
        {
            int SQLplanID = 0;
            query = "SELECT FK_User_PlanID FROM [User] WHERE PK_UserID= @accountID;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {

                    SQLplanID = (int)dataReader["FK_User_PlanID"];

                }
            }
            dataReader.Close();
            connection.Close();

            return SQLplanID;

        }

        public static string GetJoinedPlanName(int accountID)
        {
            int SQLplanID = GetJoinedPlanID(accountID);
            string SQLplanName = "";
            query = "SELECT Name FROM [Plan] WHERE PK_PlanID=@SQLplanID;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            dataReader = command.ExecuteReader();
            dataReader.Read();
            SQLplanName = (string)dataReader["Name"];
            connection.Close();
            return SQLplanName;
        }

        // Get Joined Plan Day Number
        public static int GetJoinedPlanDayNumber(int accountID)
        {
            connection.Open();

            string dateDiff = "SELECT DATEDIFF(day, PlanJoiningDate , GETDATE()) FROM [user] WHERE PK_UserID = @userID";
            command = new SqlCommand(dateDiff, connection);
            command.Parameters.AddWithValue("@userID", accountID);

            int dayNumber = 0;

            dayNumber = (int)command.ExecuteScalar();

            connection.Close();

            // to make it begin with 1 instead of zero
            int SQLplanDay = dayNumber + 1;

            return SQLplanDay;
        }

        // Get Joined Plan items' Descriptions
        public static string GetDayBreakfastDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string breakfastDiscription = "";
            query = "SELECT BreakfastDescription FROM PlanDayDescription WHERE FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    breakfastDiscription = (string)dataReader["BreakfastDescription"];

                }
            }

            connection.Close();
            return breakfastDiscription;
        }

        public static string GetDayLucnchDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string lucnchDiscription = "";
            query = "SELECT LunchDescription FROM PlanDayDescription WHERE FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    lucnchDiscription = (string)dataReader["LunchDescription"];

                }
            }

            connection.Close();
            return lucnchDiscription;
        }

        public static string GetDayDinnerDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string dinnerDiscription = "";
            query = "SELECT DinnerDescription FROM PlanDayDescription WHERE FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    dinnerDiscription = (string)dataReader["DinnerDescription"];

                }
            }

            connection.Close();
            return dinnerDiscription;
        }

        public static string GetDayWorkoutDescription(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            int SQLplanID = GetJoinedPlanID(accountID);
            string workoutDiscription = "";
            query = "SELECT WorkoutDescription FROM PlanDayDescription WHERE FK_PlanDayDescription_PlanID=@SQLplanID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanID", SQLplanID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    workoutDiscription = (string)dataReader["WorkoutDescription"];

                }
            }

            connection.Close();
            return workoutDiscription;
        }


        // Get Joined Plan Checkboxes' Status
        public static bool GetDayBreakfastStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlBreakfast = false;
            query = " SELECT BreakfastIsDone FROM UserPlanDay WHERE FK_UserPlanDay_UserID = @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    SqlBreakfast = (bool)dataReader["BreakfastIsDone"];

                }
            }
            connection.Close();

            return SqlBreakfast;
        }

        public static bool GetDayLunchStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlLunch = false;
            query = " SELECT LunchIsDone FROM UserPlanDay WHERE FK_UserPlanDay_UserID = @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    SqlLunch = (bool)dataReader["LunchIsDone"];

                }
            }
            connection.Close();

            return SqlLunch;
        }

        public static bool GetDayDinnerStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlDinner = false;
            query = " SELECT DinnerIsDone FROM UserPlanDay WHERE FK_UserPlanDay_UserID=@accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    SqlDinner = (bool)dataReader["DinnerIsDone"];

                }
            }
            connection.Close();

            return SqlDinner;
        }

        public static bool GetDayWorkoutStatus(int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            bool SqlWorkout = false;
            query = " SELECT WorkoutsIsDone FROM UserPlanDay WHERE FK_UserPlanDay_UserID= @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                if (dataReader.HasRows == true)
                {
                    SqlWorkout = (bool)dataReader["WorkoutsIsDone"];

                }
            }
            connection.Close();

            return SqlWorkout;
        }


        //Modify Joined Plan Checkboxes

        public static void UpdatePlanDayNumber(int accountID, int dayNumber)
        {
            connection.Open();
            query = "SELECT DayNumber " +
                           "FROM UserPlanDay " +
                           "WHERE FK_UserPlanDay_UserID = @userID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userID", accountID);

            if ((int)command.ExecuteScalar() != dayNumber)
            {
                query = "UPDATE [UserPlanDay] " +
                        "SET DayNumber   = @dayNumber, " +
                        "BreakfastIsDone = 0, " +
                        "LunchIsDone     = 0, " +
                        "DinnerIsDone    = 0, " +
                        "WorkoutsIsDone  = 0 " +
                        "WHERE FK_UserPlanDay_UserID = @userID";

                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@dayNumber", dayNumber);
                command.Parameters.AddWithValue("@userID", accountID);

                dataReader = command.ExecuteReader();
            }

            connection.Close();
        }

        public static void UpdateDayBreakfastStatus(bool checkedBreakfast, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            query = "UPDATE UserPlanDay SET  BreakfastIsDone=@checkedBreakfast WHERE  FK_UserPlanDay_UserID= @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            command.Parameters.AddWithValue("@checkedBreakfast", checkedBreakfast);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateDayLunchStatus(bool checkedLunch, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            query = "UPDATE UserPlanDay SET  LunchIsDone=@checkedLunch WHERE  FK_UserPlanDay_UserID= @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            command.Parameters.AddWithValue("@checkedLunch", checkedLunch);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateDayDinnerStatus(bool checkedDinner, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            query = "UPDATE UserPlanDay SET  DinnerIsDone=@checkedDinner WHERE  FK_UserPlanDay_UserID= @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            command.Parameters.AddWithValue("@checkedDinner", checkedDinner);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void UpdateDayWorkoutStatus(bool checkedWorkout, int accountID)
        {
            int SQLplanDay = GetJoinedPlanDayNumber(accountID);
            query = "UPDATE UserPlanDay SET  WorkoutsIsDone=@checkedWorkout WHERE  FK_UserPlanDay_UserID= @accountID AND DayNumber = @SQLplanDay ;";
            connection.Open();
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SQLplanDay", SQLplanDay);
            command.Parameters.AddWithValue("@checkedWorkout", checkedWorkout);
            command.Parameters.AddWithValue("@accountID", accountID);
            command.ExecuteNonQuery();


            connection.Close();
        }



        ///////////// Admin's Queries AND Functions /////////////


        // Load all Admin's Data
        public static AdminModel GetAdminData(int adminID)
        {
            AdminModel currentAdmin = new AdminModel();

            connection.Open();

            // Info FROM Admin Table
            query = "SELECT * FROM [Admin] WHERE PK_AdminID = @adminID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@adminID", adminID);
            dataReader = command.ExecuteReader();

            dataReader.Read();
            currentAdmin.FirstName = dataReader["FirstName"].ToString();
            currentAdmin.LastName = dataReader["LastName"].ToString();
            dataReader.Close();

            // Info FROM Accounts Table
            query = "SELECT Email, Password FROM [Account] WHERE AccountID = @adminID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@adminID", adminID);
            dataReader = command.ExecuteReader();

            dataReader.Read();
            currentAdmin.Email = dataReader["Email"].ToString();
            currentAdmin.Password = dataReader["Password"].ToString();
            dataReader.Close();

            connection.Close();

            return currentAdmin;
        }

        // UPDATE User Account
        public static void UpdateAdminAccount(AdminModel currentAdmin)
        {
            connection.Open();

            query = "UPDATE [Admin] Set FirstName = @FirstName WHERE PK_AdminID = @AdminId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@AdminId", currentAdmin.ID));
            command.Parameters.Add(new SqlParameter("@FirstName", currentAdmin.FirstName));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [Admin] Set LastName = @LastName WHERE PK_AdminID = @AdminId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@AdminId", currentAdmin.ID));
            command.Parameters.Add(new SqlParameter("@LastName", currentAdmin.LastName));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            query = "UPDATE [Account] Set Email = @Email WHERE AccountID = @AdminId";
            command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@AdminId", currentAdmin.ID));
            command.Parameters.Add(new SqlParameter("@Email", currentAdmin.Email));
            dataReader = command.ExecuteReader();
            dataReader.Close();

            connection.Close();
        }

        public static void UpdateAdminPassword(AdminModel currentAdmin)
        {
            connection.Open();

            query = "UPDATE Account SET Password = @newPassword, Type = @type WHERE AccountID = @accountID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@newPassword", currentAdmin.Password);
            command.Parameters.AddWithValue("@type", "Admin");
            command.Parameters.AddWithValue("@accountID", currentAdmin.ID);
            command.ExecuteNonQuery();

            connection.Close();

        }

        public static void AddNewAdmin(string email, string firstName, string lastName)
        {
            connection.Open();
            query = "INSERT INTO Admin(FirstName,LastName) VALUES (@firstName,@lastName)";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.ExecuteReader();

            connection.Close();

            string password = GenerateRandomPassword();
            string encryptedPassword = EncryptPassword(password);
            InsertNewAdminAccount(email, encryptedPassword);

            // Sending email to gmails only
            if (email.Contains("gmail"))
                SendAdminEmail(email, password);
        }

        public static void InsertNewAdminAccount(string email, string password)
        {
            connection.Open();

            query = "SELECT MIN(PK_AdminID) FROM Admin ";
            command = new SqlCommand(query, connection);
            int adminId = (int)command.ExecuteScalar();

            query = "INSERT INTO Account VALUES(@adminId , @email, @password , @type);";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@adminId", adminId);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@type", "Admin*");
            command.ExecuteReader();

            connection.Close();
        }

        public static List<int> GetAppRatingValues()
        {
            connection.Open();
            List<int> ratingList = new List<int>();

            for (int i = 1; i <= 5; i++)
            {
                query = "SELECT COUNT(FK_Feedback_UserID) FROM Feedback WHERE Rating = @ratingValue;";

                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ratingValue", i);

                ratingList.Add((int)command.ExecuteScalar());
            }

            connection.Close();

            return ratingList;
        }

        public static List<FeedbackModel> GetFeedbacks()
        {

            List<FeedbackModel> allFeedbackModels = new List<FeedbackModel>();

            connection.Open();

            query = "SELECT [user].FirstName, [user].LastName, [Feedback].Feedback " +
                           "FROM [User] RIGHT JOIN [Feedback] " +
                           "ON [User].PK_UserID  = Feedback.FK_Feedback_UserID";

            command = new SqlCommand(query, connection);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (!string.IsNullOrWhiteSpace(dataReader["Feedback"].ToString()))
                {
                    FeedbackModel temp = new FeedbackModel();

                    temp.FirstName = dataReader["FirstName"].ToString();
                    temp.LastName = dataReader["LastName"].ToString();
                    temp.Feedback = dataReader["Feedback"].ToString();

                    allFeedbackModels.Add(temp);
                }
            }

            connection.Close();

            return allFeedbackModels;
        }

        public static void DeleteFeedback(string feedbackBody)
        {
            connection.Open();

            query = "DELETE FROM [Feedback] WHERE [Feedback].Feedback LIKE @feedbackBody";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@feedbackBody", feedbackBody);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public static int GetAppUsersNumber()
        {
            int appUsersNumber;

            connection.Open();
            query = "SELECT COUNT(*) FROM [User]";
            command = new SqlCommand(query, connection);
            appUsersNumber = (int)command.ExecuteScalar();

            connection.Close();

            return appUsersNumber;

        }

        public static bool IsNewAdmin(int accountID)
        {
            connection.Open();

            query = "SELECT Type FROM Account WHERE AccountID = @accountID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@accountID", accountID);

            dataReader = command.ExecuteReader();
            dataReader.Read();
            string type = (string)dataReader["Type"];

            connection.Close();

            if (type == "Admin*")
                return true;
            else
                return false;
        }


        // Challenges Managing
        public static void AddNewChallenge(byte[] photo, string name, string description, int targetMinutes,
                                    string reward, DateTime? dueDate, int workoutID)
        {
            connection.Open();

            query = "INSERT INTO [Challenge] (Photo, Name, Description, TargetMinutes, Reward, DueDate, Fk_Challenge_WorkoutID) " +
                     "VALUES (@Photo, @Name, @Description, @TargetMinutes, @Reward, @DueDate, @WorkoutID)";

            command = new SqlCommand(query, connection);

            if (photo == null)
                command.Parameters.Add("@Photo", SqlDbType.Image).Value = DBNull.Value;
            else
                command.Parameters.AddWithValue("@Photo", photo);

            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@TargetMinutes", targetMinutes);
            command.Parameters.AddWithValue("@Reward", reward);
            command.Parameters.AddWithValue("@DueDate", dueDate);
            command.Parameters.AddWithValue("@WorkoutID", workoutID);
            command.ExecuteReader();

            connection.Close();

        }

        public static void DeleteChallenge(int challengeID)
        {

            connection.Open();

            query = "DELETE [UserChallenge] " +
                           "FROM [Challenge] RIGHT JOIN [UserChallenge] " +
                           "ON [Challenge].PK_ChallengeID = [UserChallenge].FK_UserChallenge_ChallengeID " +
                           "WHERE [Challenge].PK_ChallengeID = @challengeID";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@challengeID", challengeID);
            dataReader = command.ExecuteReader();
            dataReader.Close();


            query = "DELETE FROM[Challenge] WHERE[Challenge].PK_ChallengeID = @challengeID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@challengeID", challengeID);
            dataReader = command.ExecuteReader();

            connection.Close();

        }



        public static void DeleteUser(int accountID)
        {

            string feedbackDelete = "delete FROM Feedback WHERE FK_Feedback_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(feedbackDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string userWorkoutDelete = "delete FROM UserWorkout WHERE  FK_UserWorkout_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(userWorkoutDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string accountDelete = "delete FROM  Account WHERE  AccountID=@accountID;";
            connection.Open();
            command = new SqlCommand(accountDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string challengeDelete = "delete FROM  UserChallenge WHERE FK_UserChallenge_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(challengeDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string planDelete = "delete FROM  UserPlanDay WHERE FK_UserPlanDay_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(planDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string weightDelete = "delete FROM  UserWeight WHERE  FK_UserWeight_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(weightDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string foodDelete = "delete FROM  UserFood WHERE FK_UserFood_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(foodDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

            string userDelete = "delete FROM [User] WHERE PK_UserID=@accountID;";
            connection.Open();
            command = new SqlCommand(userDelete, connection);
            command.Parameters.AddWithValue("@accountID", accountID);
            dataReader = command.ExecuteReader();
            dataReader.Close();
            connection.Close();

        }

        public static List<UserModel> SearchForUser(string search)
        {
            List<UserModel> AllUsers = new List<UserModel>();

            connection.Open();
            query = "SELECT PK_UserID, Photo , FirstName , LastName , Username, Email " +
                            "FROM [User] inner JOIN Account ON PK_UserID = AccountID " +
                            "WHERE FirstName LIKE '%' + @search + '%' " +
                            "or LastName LIKE '%' + @search + '%' " +
                            "or Username LIKE '%' + @search + '%' " +
                            "or Email LIKE '%' + @search + '%'";

            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@search", search);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                UserModel temp = new UserModel();

                temp.ID = (int)dataReader["PK_UserID"];

                if (dataReader["Photo"] != DBNull.Value)
                    temp.ProfilePhoto.ByteArray = (byte[])dataReader["Photo"];

                temp.FirstName = dataReader["FirstName"].ToString();
                temp.LastName = dataReader["LastName"].ToString();
                temp.Email = dataReader["Email"].ToString();

                AllUsers.Add(temp);

            }

            connection.Close();

            return AllUsers;
        }

    }
}
