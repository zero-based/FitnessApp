namespace FitnessApp.ViewModels
{
    class DaysViewModel
    {
        public DaysViewModel(int planNumber)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in DayItems Array

            DayItems = new[] {
            new DayItem("Day1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

            new DayItem("Day2", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

            new DayItem("Day3", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

            new DayItem("Day4", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

           new DayItem("Day5", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),
        };
        }


        public DayItem[] DayItems { get; }
    }

}
