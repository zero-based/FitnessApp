using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.SQLdatabase
{
    class SQLqueries
    {

        ////////// SQL Connection string //////////
        // [IMPORTANT] Add your server name to data source.

        SqlConnection Connection = new SqlConnection("data source=MICHA\\SQLEXPRESS; database=FITNESSAPP; integrated security=SSPI");




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
            MailMessage message = new System.Net.Mail.MailMessage();

            // Reciever's Email
            message.To.Add(email);

            // Email Subject
            message.Subject = "Welcome To Our Humble Fitness Application ";

            // Sender's Email
            message.From = new System.Net.Mail.MailAddress("fitness.weightlossapp@gmail.com", "Fitness App");

            // Email Body
            message.IsBodyHtml = true;
            string htmlBody = "<h3>Hi '" + name + "'</h3><br><h3>Welcome&nbsp;to <strong><u>FitnessApp</u>,</strong></h3><br>" +
                              "<ul><li>Point 1</li><br><li>Point 2</li><br><li>Point 3</li><br></ul><br><p>Please contact us</p><br>";
            message.Body = htmlBody;


            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
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
            SqlCommand cmd = new SqlCommand("select*from AdminAndUserAccount where Email=@email AND Password=@password", Connection);
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
    }
}
