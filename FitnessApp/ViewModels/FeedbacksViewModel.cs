using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class FeedbacksViewModel
    {
        static SQLqueries SQLqueriesObject = new SQLqueries();

        private List<FeedbackModel> feedbackModels;
        public FeedbacksViewModel()
        {
            feedbackModels = SQLqueriesObject.GetFeedbacks();
        }
        public List<FeedbackModel> FeedbackModels {  get => feedbackModels; set { } }
    }
}
