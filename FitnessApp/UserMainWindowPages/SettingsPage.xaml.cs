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

            // Initialize Profile Expander to be expanded
            ProfileExpander.IsExpanded = true;
        }

        private void RatingBar_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<int> e)
        {

        }


        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Remove Expanded Event Handler from all Expanders
            // to prevent calling Expander_Expanded Event Handler 
            // recursively.
            ProfileExpander .Expanded -= Expander_Expanded;
            AccountExpander .Expanded -= Expander_Expanded;
            SecurityExpander.Expanded -= Expander_Expanded;
            AboutExpander   .Expanded -= Expander_Expanded;
            FeedbackExpander.Expanded -= Expander_Expanded;
            HelpExpander    .Expanded -= Expander_Expanded;

            // Close all Expanders.
            ProfileExpander .IsExpanded = false;
            AccountExpander .IsExpanded = false;
            SecurityExpander.IsExpanded = false;
            AboutExpander   .IsExpanded = false;
            FeedbackExpander.IsExpanded = false;
            HelpExpander    .IsExpanded = false;

            // Re-add Expanded Event Handler to all Expanders.
            ProfileExpander .Expanded += Expander_Expanded;
            AccountExpander .Expanded += Expander_Expanded;
            SecurityExpander.Expanded += Expander_Expanded;
            AboutExpander   .Expanded += Expander_Expanded;
            FeedbackExpander.Expanded += Expander_Expanded;
            HelpExpander    .Expanded += Expander_Expanded;


            // Get Current Expander object from sender.
            Expander currentExpander = sender as Expander;

            // Remove Expanded Event Handler from Current Expander.
            currentExpander.Expanded -= Expander_Expanded;

            // Open current Expander only.
            currentExpander.IsExpanded = true;

            // Re-add Expanded Event Handler to Current Expander.
            currentExpander.Expanded += Expander_Expanded;

        }

    }
}
