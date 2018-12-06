using FitnessApp.Models;

namespace FitnessApp.ViewModels
{
    class DaysViewModel
    {
        public DaysViewModel(int planNumber)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in DayModels Array

            DayModels = new[] {
            new DayModel("Day1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

            new DayModel("Day2", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

            new DayModel("Day3", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

            new DayModel("Day4", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli"),

           new DayModel("Day5", "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli",
            "Lorem ipsum dolor sit amet, consectetur adipiscing eli")
        };
        }

        public DayModel[] DayModels { get; }
    }

}
