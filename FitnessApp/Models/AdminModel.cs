namespace FitnessApp.Models
{
    public class AdminModel
    {
        SQLdatabase.SQLqueries SQLqueriesObject = new SQLdatabase.SQLqueries();

        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;

        public AdminModel() { }

        public AdminModel(int adminID)
        {
            AdminModel temp = SQLqueriesObject.LoadAdminData(adminID);

            _id = adminID;
            _firstName = temp.FirstName;
            _lastName = temp.LastName;
            _email = temp.Email;
            _password = temp.Password;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FullName
        {
            get { return _firstName + " " + _lastName; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
