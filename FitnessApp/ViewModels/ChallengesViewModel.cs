
using FitnessApp.Models;

namespace FitnessApp.ViewModels
{
    class ChallengesViewModel
    {
        private ChallengeModel[] joinedChallengeModels;
        private ChallengeModel[] allChallengeModels;

        public ChallengesViewModel() { }

        public void JoinedChallengesViewModel()
        {
            joinedChallengeModels = new[] {
            new ChallengeModel("Challenge1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge2","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge3","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge4","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge5","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories")};
        }

        public void AllChallengesViewModel()
        {
            allChallengeModels = new[] {
            new ChallengeModel("Challenge1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge2","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge3","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge4","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel("Challenge5","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories")
            };
        }

        public ChallengeModel[] AllChallengeModels
        {
            get { return allChallengeModels; }
            set { }
        }

        public ChallengeModel[] JoinedChallengeModels
        {
            get { return joinedChallengeModels; }
            set { }
        }
    }
}
