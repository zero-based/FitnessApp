namespace FitnessApp.Models
{
    class UserModel
    {
        //private string image;
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _username;
        private string _email;
        private string _password;
        private string _gender;
        private string _birthDate;
        private double _weight;
        private double _height;
        private double _targetWeight;
        private double _kilosToLose;
        private double _workoutsPerWeek;
        private double _workoutHoursPerDay;

        public UserModel(int id,              string firstName,   string lastName,
                         string username,     string email,       string password,
                         string gender,       string birthDate,   double weight,          double height,
                         double targetWeight, double kilosToLose, double workoutsPerWeek, double workoutHoursPerDay)
        { 
            _id = id;
            _firstName         = firstName;
            _lastName          = lastName;
            _username          = username;
            _email             = email;
            _password          = password;
            _gender            = gender;
            _birthDate         = birthDate;
            _weight            = weight;
            _height            = height;
            _targetWeight      = targetWeight;
            _kilosToLose       = kilosToLose;
            _workoutsPerWeek   = workoutsPerWeek;
            _workoutHoursPerDay= workoutHoursPerDay;
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

        public string Username
        {
            get { return _username; }
            set { _username = value; }
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

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public double TargetWeight
        {
            get { return _targetWeight; }
            set { _targetWeight = value; }
        }

        public double KilosToLose
        {
            get { return _kilosToLose; }
            set { _kilosToLose = value; }
        }

        public double WorkoutsPerWeek
        {
            get { return _workoutsPerWeek; }
            set { _workoutsPerWeek = value; }
        }

        public double WorkoutHoursPerDay
        {
            get { return _workoutHoursPerDay; }
            set { _workoutHoursPerDay = value; }
        }

    }
}
