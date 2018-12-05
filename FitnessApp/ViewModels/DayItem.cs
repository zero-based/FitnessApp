namespace FitnessApp.ViewModels
{
    class DayItem
    {
        private string dayNumber;
        private string breakfastDescription;
        private string lunchDescription;
        private string dinnerDescription;
        private string workoutDescription;

        public DayItem(string _dayNumber, string _breakfastDescription, string _lunchDescription, string _dinnerDescription, string _workoutDescription)
        {
            dayNumber = _dayNumber;
            breakfastDescription = _breakfastDescription;
            lunchDescription = _lunchDescription;
            dinnerDescription = _dinnerDescription;
            workoutDescription = _workoutDescription;
        }

        public string DayNumber
        {
            get { return dayNumber; }
            set { dayNumber = value; }
        }

        public string BreakfastDescription
        {
            get { return breakfastDescription; }
            set { breakfastDescription = value; }
        }

        public string LunchDescription
        {
            get { return lunchDescription; }
            set { lunchDescription = value; }
        }

        public string DinnerDescription
        {
            get { return dinnerDescription; }
            set { dinnerDescription = value; }
        }

        public string WorkoutDescription
        {
            get { return workoutDescription; }
            set { workoutDescription = value; }
        }
    }
}
