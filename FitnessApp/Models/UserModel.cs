﻿using FitnessApp.SQLserver;

namespace FitnessApp.Models
{
    public class UserModel
    {

        private int _id;
        private ImageModel _profilePhoto = new ImageModel() { Default = @"..\..\Images\AccountCircleDefaultIcon.png" };
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _gender;
        private string _birthDate;
        private int    _age;
        private double _weight;
        private double _height;
        private double _targetWeight;
        private double _kilosToLosePerWeek;
        private double _workoutsPerWeek;
        private double _workoutHoursPerDay;

        public UserModel() { }

        public UserModel(int userID)
        {
            UserModel temp = Database.GetUserData(userID);

            _id                     = userID;
            _profilePhoto.ByteArray = temp.ProfilePhoto.ByteArray;
            _firstName              = temp.FirstName;
            _lastName               = temp.LastName;
            _email                  = temp.Email;
            _password               = temp.Password;
            _gender                 = temp.Gender;
            _birthDate              = temp.BirthDate;
            _age                    = temp.Age;
            _weight                 = temp.Weight;
            _height                 = temp.Height;
            _targetWeight           = temp.TargetWeight;
            _kilosToLosePerWeek     = temp.KilosToLosePerWeek;
            _workoutsPerWeek        = temp.WorkoutsPerWeek;
            _workoutHoursPerDay     = temp.WorkoutHoursPerDay;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ImageModel ProfilePhoto
        {
            get { return _profilePhoto; }
            set { _profilePhoto = value; }
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

        public int Age
        {
            get { return _age; }
            set { _age = value; }
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

        public double KilosToLosePerWeek
        {
            get { return _kilosToLosePerWeek; }
            set { _kilosToLosePerWeek = value; }
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
