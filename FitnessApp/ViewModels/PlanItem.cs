namespace FitnessApp.ViewModels
{
    class PlanItem
    {
        //private string planImage;
        private string planName;
        private string planDescription;
        private string planDuration;
        private string planHardness;

        public PlanItem(string _planName, string _planDescription, string _planDuration, string _planHardness)
        {
            planName = _planName;
            planDescription = _planDescription;
            planDuration = _planDuration;
            planHardness = _planHardness;
        }

        public string PlanName
        {
            get { return planName; }
            set { planName = value; }
        }

        public string PlanDescription
        {
            get { return planDescription; }
            set { planDescription = value; }
        }

        public string PlanDuration
        {
            get { return planDuration; }
            set { planDuration = value; }
        }

        public string PlanHardness
        {
            get { return planHardness; }
            set { planHardness = value; }
        }
    }
}
