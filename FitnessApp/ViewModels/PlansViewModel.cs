using FitnessApp.Models;
using FitnessApp.SQLserver;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class PlansViewModel
    {
       
        private List<PlanModel> planModels;

        public PlansViewModel(int accountID)
        {
            planModels = Database.GetPlans(accountID);
        }

        public List<PlanModel> PlanModels
        {
            get => planModels;
            set { }
        }
    }
}

    

