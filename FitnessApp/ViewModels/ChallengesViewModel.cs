namespace FitnessApp.ViewModels
{
    class ChallengesViewModel
    {
        private ChallengeItem[] joinedChallengeItems;
        private ChallengeItem[] allChallengeItems;

        public ChallengesViewModel() { }

        public void JoinedChallengesViewModel()
        {
            joinedChallengeItems = new[] {
            new ChallengeItem("Challenge1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge2","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge3","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge4","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge5","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories")};
        }

        public void AllChallengesViewModel()
        {
            allChallengeItems = new[] {
            new ChallengeItem("Challenge1", "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation" +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge2","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge3","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge4","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories"),

            new ChallengeItem("Challenge5","Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
                " ullamco laboris nisi ut aliquip ex ea.", "30-Nov-18", "Run 10 KM", "Lose 1500 Calories")};
        }

        public ChallengeItem[] AllChallengeItems
        {
            get { return allChallengeItems; }
            set { }
        }

        public ChallengeItem[] JoinedChallengeItems
        {
            get { return joinedChallengeItems; }
            set { }
        }
    }
}
