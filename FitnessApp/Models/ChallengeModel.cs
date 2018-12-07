namespace FitnessApp.Models
{
    class ChallengeModel
    {
        //private string image;
        private int _id;
        private bool _isJoined;
        private string _type;
        private string _name;
        private string _description;
        private string _dueDate;
        private string _target;
        private string _reward;

        public ChallengeModel(int id, bool isJoined, string type, string name, string description, string dueDate, string target, string reward)
        {
            _id = id;
            _isJoined = isJoined;
            _type = type;
            _name = name;
            _description = description;
            _dueDate = dueDate;
            _target = target;
            _reward = reward;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool IsJoined
        {
            get { return _isJoined; }
            set { _isJoined = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
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
