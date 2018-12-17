using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class FeedbacksViewModel
    {
       

        private List<FeedbackModel> feedbackModels;
        public FeedbacksViewModel()
        {
            feedbackModels = SQLqueries.GetFeedbacks();
        }
        public List<FeedbackModel> FeedbackModels {  get => feedbackModels; set { } }
    }
}
