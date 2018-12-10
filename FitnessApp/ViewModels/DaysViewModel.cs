using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class DaysViewModel
    {
        static SQLqueries SQLqueriesObject = new SQLqueries();
        private List<DayModel> dayModels;

        public DaysViewModel(int planID)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in DayModels Array

            dayModels = SQLqueriesObject.LoadPlanDays(planID);
        }

        public List<DayModel> DayModels
        {
            get => dayModels;
            set { }
        }
    }

}
