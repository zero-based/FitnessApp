namespace FitnessApp.Models
{
    class PlanModel
    {
        //private string image;
        private string _name;
        private string _description;
        private string _duration;
        private string _hardness;

        public PlanModel(string name, string description, string duration, string hardness)
        {
            _name = name;
            _description = description;
            _duration = duration;
            _hardness = hardness;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        public string Hardness
        {
            get { return _hardness; }
            set { _hardness = value; }
        }
    }
}
