using FitnessApp.Models;
using FitnessApp.SQLserver;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class DaysViewModel
    {
       
        private List<DayModel> dayModels;

        public DaysViewModel(int planID)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in DayModels Array

            dayModels = Database.GetPlanDays(planID);
        }

        public List<DayModel> DayModels
        {
            get => dayModels;
            set { }
        }
    }

}
