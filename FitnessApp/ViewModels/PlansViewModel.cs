using FitnessApp.Models;

namespace FitnessApp.ViewModels
{
    class PlansViewModel
    {
        public PlansViewModel()
        {
            PlanModels = new[] {
        new PlanModel("Plan1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
            " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
            " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM"),

        new PlanModel("Plan2","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
            " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM"),

        new PlanModel("Plan3","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
            " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM")
            };
        }
        public PlanModel[] PlanModels { get; }
    }
}

    

