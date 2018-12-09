namespace FitnessApp.Models
{
    class DayModel
    {
        private int _planID;
        private string _planName;
        private string _dayNumber;
        private string _breakfastDescription;
        private string _lunchDescription;
        private string _dinnerDescription;
        private string _workoutDescription;
        private bool _isBreakfastDone;
        private bool _isLunchDone;
        private bool _isDinnerDone;
        private bool _isWorkoutDone;


        public DayModel(string dayNumber, string breakfastDescription, string lunchDescription, string dinnerDescription, string workoutDescription)
        {
            _dayNumber = dayNumber;
            _breakfastDescription = breakfastDescription;
            _lunchDescription = lunchDescription;
            _dinnerDescription = dinnerDescription;
            _workoutDescription = workoutDescription;
        }


        public DayModel(int planID, string planName, string dayNumber,
                        string breakfastDescription, string lunchDescription, string dinnerDescription, string workoutDescription,
                        bool isBreakfastDone, bool isLunchDone, bool isDinnerDone, bool isWorkoutDone)
        {
            _planID = planID;
            _planName = planName;
            _dayNumber = dayNumber;
            _breakfastDescription = breakfastDescription;
            _lunchDescription = lunchDescription;
            _dinnerDescription = dinnerDescription;
            _workoutDescription = workoutDescription;
            _isBreakfastDone = isBreakfastDone;
            _isLunchDone = isLunchDone;
            _isDinnerDone = isDinnerDone;
            _isWorkoutDone = isWorkoutDone;
        }


        public string DayNumber
        {
            get { return _dayNumber; }
            set { _dayNumber = value; }
        }

        public string BreakfastDescription
        {
            get { return _breakfastDescription; }
            set { _breakfastDescription = value; }
        }

        public string LunchDescription
        {
            get { return _lunchDescription; }
            set { _lunchDescription = value; }
        }

        public string DinnerDescription
        {
            get { return _dinnerDescription; }
            set { _dinnerDescription = value; }
        }

        public string WorkoutDescription
        {
            get { return _workoutDescription; }
            set { _workoutDescription = value; }
        }
    }
}
