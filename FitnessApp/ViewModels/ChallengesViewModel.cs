
using FitnessApp.Models;

namespace FitnessApp.ViewModels
{
    class ChallengesViewModel
    {
        private ChallengeModel[] allChallengeModels;
        private ChallengeModel[] joinedChallengeModels;

        public ChallengesViewModel() { }

        public void AllChallengesViewModel()
        {
            allChallengeModels = new[] {
            new ChallengeModel(100, true, "Challenge1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel(398, false, "Challenge2","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel(999, false, "Challenge3","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel(990, true, "Challenge4","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel(558, false, "Challenge5","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories")};

        }

        public void JoinedChallengesViewModel()
        {
            joinedChallengeModels = new[] {
            new ChallengeModel(100, true, "Challenge1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeModel(990, true, "Challenge4","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories")};
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
