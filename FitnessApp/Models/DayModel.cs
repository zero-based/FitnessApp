namespace FitnessApp.Models
{
    public class DayModel
    {
        private int _dayNumber;
        private string _breakfastDescription;
        private string _lunchDescription;
        private string _dinnerDescription;
        private string _workoutDescription;

        public DayModel() { }

        public DayModel(int dayNumber, string breakfastDescription, string lunchDescription, string dinnerDescription, string workoutDescription)
        {
            _dayNumber = dayNumber;
            _breakfastDescription = breakfastDescription;
            _lunchDescription = lunchDescription;
            _dinnerDescription = dinnerDescription;
            _workoutDescription = workoutDescription;
        }

        public int DayNumber
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
