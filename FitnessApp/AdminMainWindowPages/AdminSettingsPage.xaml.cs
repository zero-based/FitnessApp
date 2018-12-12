using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FitnessApp.AdminMainWindowPages
{
    /// <summary>
    /// Interaction logic for AdminSettingsPage.xaml
    /// </summary>
    public partial class AdminSettingsPage : Page
    {
        public static AdminSettingsPage AdminSettingsPageObject = new AdminSettingsPage();
        public AdminSettingsPage()
        {
            InitializeComponent();

            // Initialize Profile Expander to be expanded
            AccountExpander.IsExpanded = true;

            // Initialize DataContext with signedInUser Model
            DataContext = UserMainWindow.signedInUser;
        }

        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Remove Expanded Event Handler from all Expanders
            // to prevent calling Expander_Expanded Event Handler 
            // recursively.
            AccountExpander.Expanded  -= Expander_Expanded;
            SecurityExpander.Expanded -= Expander_Expanded;
            AboutExpander.Expanded    -= Expander_Expanded;
            HelpExpander.Expanded     -= Expander_Expanded;

            // Close all Expanders.
            AccountExpander.IsExpanded  = false;
            SecurityExpander.IsExpanded = false;
            AboutExpander.IsExpanded    = false;
            HelpExpander.IsExpanded     = false;

            // Re-add Expanded Event Handler to all Expanders.
            AccountExpander.Expanded  += Expander_Expanded;
            SecurityExpander.Expanded += Expander_Expanded;
            AboutExpander.Expanded    += Expander_Expanded;
            HelpExpander.Expanded     += Expander_Expanded;


            // Get Current Expander object from sender.
            Expander currentExpander = sender as Expander;

            // Remove Expanded Event Handler from Current Expander.
            currentExpander.Expanded -= Expander_Expanded;

            // Open current Expander only.
            currentExpander.IsExpanded = true;

            // Re-add Expanded Event Handler to Current Expander.
            currentExpander.Expanded += Expander_Expanded;

        }



        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


        private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Account Code Here...
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Password Code Here...
        }
    }
}
