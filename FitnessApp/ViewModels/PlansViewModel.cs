using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class PlansViewModel
    {
        static SQLqueries SQLqueriesObject = new SQLqueries();
        private List<PlanModel> planModels;

        public PlansViewModel(int accountID)
        {
            planModels = SQLqueriesObject.GetPlans(accountID);
        }

        public List<PlanModel> PlanModels
        {
            get => planModels;
            set { }
        }
    }
}

    

