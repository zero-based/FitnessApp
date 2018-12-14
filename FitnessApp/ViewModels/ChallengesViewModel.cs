using FitnessApp.Models;
using System;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FitnessApp.ViewModels
{
    class ChallengesViewModel
    {
        static SQLqueries SQLqueriesObject = new SQLqueries();
        private List<ChallengeModel> allChallengeModels;
        private List<ChallengeModel> uncompletedJoinedChallengeModels = new List<ChallengeModel>();
        private List<ChallengeModel> completedJoinedChallengeModels = new List<ChallengeModel>();


        public ChallengesViewModel() { }

        public void AllChallengesViewModel(int accountID)
        {
            allChallengeModels = SQLqueriesObject.LoadAllChallenges(accountID);
        }

        public void JoinedChallengesViewModel(int accountID)
        {

            List<ChallengeModel> joinedChallengeModels = SQLqueriesObject.LoadJoinedChallenges(accountID);

            foreach (var item in joinedChallengeModels)
            {
                // Classify Challenges
                if (item.Progress < item.TargetMinutes)
                    uncompletedJoinedChallengeModels.Add(item);
                else
                    completedJoinedChallengeModels.Add(item);
            }

        }

        public List<ChallengeModel> AllChallengeModels
        {
            get => allChallengeModels;
            set { }
        }

        public List<ChallengeModel> UncompletedJoinedChallengeModels
        {
            get => uncompletedJoinedChallengeModels;
            set { }
        }

        public List<ChallengeModel> CompletedJoinedChallengeModels
        {
            get => completedJoinedChallengeModels;
            set { }
        }
    }
}
