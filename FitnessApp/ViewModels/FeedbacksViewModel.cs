using FitnessApp.Models;
using FitnessApp.SQLserver;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class FeedbacksViewModel
    {
       

        private List<FeedbackModel> feedbackModels;
        public FeedbacksViewModel()
        {
            feedbackModels = Database.GetFeedbacks();
        }
        public List<FeedbackModel> FeedbackModels {  get => feedbackModels; set { } }
    }
}
