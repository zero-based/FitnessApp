using System.Windows.Controls;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public static SettingsPage SettingsPageObject = new SettingsPage();

        public SettingsPage()
        {
            InitializeComponent();
            SettingsPageObject = this;
        }

        private void RatingBar_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<int> e)
        {

        }
    }
}
