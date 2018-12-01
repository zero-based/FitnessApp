using System.Windows.Controls;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static HomePage HomePageObject = new HomePage();

        public HomePage()
        {
            InitializeComponent();
            HomePageObject = this;
        }
    }
}
