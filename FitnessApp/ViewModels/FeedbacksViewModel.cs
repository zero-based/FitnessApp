using FitnessApp.Models;
using System.Collections.Generic;

namespace FitnessApp.ViewModels
{
    class FeedbacksViewModel
    {
        private List<FeedbackModel> feedbackModels;
        public FeedbacksViewModel()
        {
            feedbackModels = new List<FeedbackModel>()
            {
                new FeedbackModel() { UserName = "JHON DOE", Feedback = "Lorem iposem" },
                new FeedbackModel() { UserName = "JHON DOE", Feedback = "Lorem iposem" },
                new FeedbackModel() { UserName = "JHON DOE", Feedback = "Lorem iposem" }
            };
        }
        public List<FeedbackModel> FeedbackModels {  get => feedbackModels; set { } }
    }
}
