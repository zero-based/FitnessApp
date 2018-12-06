using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.SQL_Database
{
    class SQLqueries
    {
        // Sign In Function

        public int accountID;
        public string accountType;

        public string PasswordEncryption(string password)
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

        public bool SignIn(string email, string password)
        {
            // Encrypt Password
            string encryptedPassword = PasswordEncryption(password);

            // Create Connection
            string ConnectionString = "data source=MICHA\\SQLEXPRESS; database=Fitness & Weight loss System; integrated security=SSPI";
            SqlConnection con = new SqlConnection(ConnectionString);

            // Create Command
            SqlCommand cmd = new SqlCommand("select*from[Admin & User Account] where E_Mail=@email AND Account_Password=@password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", encryptedPassword);

            // Open connection and start reading
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {

                    accountID = (int)dr["AccountID"];
                    accountType = (string)dr["Account_Type"];
                    return true; // If user exist return true
                }
            }
            if (dr.HasRows == false)
            {
                return false;  // If user doesn't exist return false
            }

            // Close connection
            con.Close();

            return false;
        }
    }
}
