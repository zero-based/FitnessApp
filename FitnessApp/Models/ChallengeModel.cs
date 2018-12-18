namespace FitnessApp.Models
{
    public class ChallengeModel
    {
        private int _id;
        private ImageModel _photo = new ImageModel() { Default = @"..\..\Images\No-Image.jpg" };
        private bool _isJoined;
        private int _progress;
        private int _workoutType;
        private string _name;
        private string _description;
        private string _dueDate;
        private int _targetMinutes;
        private string _reward;

        public ChallengeModel() { }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public ImageModel Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }

        public bool IsJoined
        {
            get { return _isJoined; }
            set { _isJoined = value; }
        }

        public int Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }

        public int WorkoutType
        {
            get { return _workoutType; }
            set { _workoutType = value; }
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

        public int TargetMinutes
        {
            get { return _targetMinutes; }
            set { _targetMinutes = value; }
        }

        public string StyledTargetMinutes
        {
            get { return TargetMinutes.ToString() + " minutes"; }
            set {  }
        }

        public string Reward
        {
            get { return _reward; }
            set { _reward = value; }
        }
    }
}
