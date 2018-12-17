using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class PlansViewModel
    {
       
        private List<PlanModel> planModels;

        public PlansViewModel(int accountID)
        {
            planModels = SQLqueries.GetPlans(accountID);
        }

        public List<PlanModel> PlanModels
        {
            get => planModels;
            set { }
        }
    }
}

    

