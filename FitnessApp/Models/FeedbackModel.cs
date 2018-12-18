namespace FitnessApp.Models
{
    public class FeedbackModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
            set { }
        }

        public string Feedback { get; set; }
    }
}
