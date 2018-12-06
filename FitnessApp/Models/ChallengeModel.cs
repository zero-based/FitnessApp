namespace FitnessApp.Models
{
    class ChallengeModel
    {
        //private string image;
        private string _name;
        private string _description;
        private string _dueDate;
        private string _target;
        private string _reward;

        public ChallengeModel(string name, string description, string dueDate, string target, string reward)
        {
            _name = name;
            _description = description;
            _dueDate = dueDate;
            _target = target;
            _reward = reward;
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

        public string DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        public string Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public string Reward
        {
            get { return _reward; }
            set { _reward = value; }
        }
    }
}
