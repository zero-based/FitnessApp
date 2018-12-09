using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public static UserMainWindow UserMainWindowObject;

        public UserMainWindow()
        {
            InitializeComponent();
            UserMainWindowObject = this;

            // Initialize Listbox Selected Index
            UserMainWindowPagesListBox.SelectedIndex = 0;

            // Intialize MessagesQueue and Assign it to MessagesSnackbar's MessageQueue
            var MessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            MessagesSnackbar.MessageQueue = MessagesQueue;
        }

        private void UserMainWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Close Side Menu Drawer
            SideMenuDrawer.IsLeftDrawerOpen = false;

            // Navigate to the selected Page and Highlight the chosen Item
            switch (UserMainWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.HomePage.HomePageObject);
                    HighlightItem(HomeTextBlock, HomeIcon);
                    break;

                case 1:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.ChallengesPage.ChallengesPageObject);
                    HighlightItem(ChallengesTextBlock, ChallengesIcon);
                    break;

                case 2:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.PlansPage.PlansPageObject);
                    HighlightItem(FitnessPlansTextBlock, FitnessPlansIcon);
                    break;

                case 3:
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.CaloriesCalculatorPage.CaloriesCalculatorPageObject);
                    HighlightItem(CaloriesCalculatorTextBlock, CaloriesCalculatorIcon);
                    break;

                case 4:
                    HighlightItem(SettingsTextBlock, SettingsIcon);
                    UserWindowPagesFrame.NavigationService.Navigate(UserMainWindowPages.SettingsPage.SettingsPageObject);
                    break;
            }
        }

        public void HighlightItem(TextBlock pageTextBlock, MaterialDesignThemes.Wpf.PackIcon pageIcon)
        {
            // Set all Items' Foreground to Black 

            // Home Item
            HomeTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            HomeIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Challenges Item
            ChallengesTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            ChallengesIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Fitness Plans Item
            FitnessPlansTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            FitnessPlansIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Calories Calculator Item
            CaloriesCalculatorTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            CaloriesCalculatorIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Settings Item
            SettingsTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            SettingsIcon     .Foreground = new SolidColorBrush(Colors.Black);


            // Highlight the required Item only and Change Page Header
            pageTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            pageIcon     .Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            PageHeaderTextBlock.Text  = pageTextBlock.Text;
        }


        private void UserProfilePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindowPagesListBox.SelectedIndex = 4;
        }

        private void LogoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SigningWindow SigningWindowTemp = new SigningWindow();
            Close();
            SigningWindowTemp.Show();
        }

    }
}
