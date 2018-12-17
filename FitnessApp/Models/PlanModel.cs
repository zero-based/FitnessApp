namespace FitnessApp.Models
{
    public class PlanModel
    {
        private int _id;
        private ImageModel _photo = new ImageModel() { Default = @"..\..\Images\No-Image.jpg" };
        private bool _isJoined;
        private string _name;
        private string _description;
        private string _duration;
        private string _hardness;

        public PlanModel() { }

        public PlanModel(int id, bool isJoined, string name, string description, string duration, string hardness)
        {
            _id = id;
            _isJoined = isJoined;
            _name = name;
            _description = description;
            _duration = duration;
            _hardness = hardness;
        }

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
            get { return _duration + " days"; }
            set { _duration = value; }
        }

        public string Hardness
        {
            get { return _hardness; }
            set { _hardness = value; }
        }
    }
}
