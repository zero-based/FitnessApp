using FitnessApp.Models;
using FitnessApp.UserWindowPages;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FitnessApp.Windows
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public static UserWindow UserWindowObject;
        public static UserModel signedInUser;

        // Declare UserWindowPages Objects
        public static HomePage HomePageObject;
        public static ChallengesPage ChallengesPageObject;
        public static PlansPage PlansPageObject;
        public static CaloriesCalculatorPage CaloriesCalculatorPageObject;
        public static SettingsPage SettingsPageObject;

        public UserWindow(int signedInUserID)
        {
            InitializeComponent();
            UserWindowObject = this;

            // Initialize User Model
            signedInUser = new UserModel(signedInUserID);

            // Initialize DataContext with signedInUser Model
            DataContext = signedInUser;

            // Initialize UserWindowPages Objects
            HomePageObject               = new HomePage();
            ChallengesPageObject         = new ChallengesPage();
            PlansPageObject              = new PlansPage();
            CaloriesCalculatorPageObject = new CaloriesCalculatorPage();
            SettingsPageObject           = new SettingsPage();

            // Initialize Listbox Selected Index
            UserWindowPagesListBox.SelectedIndex = 0;

            // Intialize MessagesQueue and Assign it to MessagesSnackbar's MessageQueue
            var MessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            MessagesSnackbar.MessageQueue = MessagesQueue;
        }

        private void UserWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Close Side Menu Drawer
            SideMenuDrawer.IsLeftDrawerOpen = false;

            // Navigate to the selected Page and Highlight the chosen Item
            switch (UserWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    UserWindowPagesFrame.NavigationService.Navigate(HomePageObject);
                    HighlightItem(HomeTextBlock, HomeIcon);
                    break;

                case 1:
                    UserWindowPagesFrame.NavigationService.Navigate(ChallengesPageObject);
                    HighlightItem(ChallengesTextBlock, ChallengesIcon);
                    break;

                case 2:
                    UserWindowPagesFrame.NavigationService.Navigate(PlansPageObject);
                    HighlightItem(FitnessPlansTextBlock, FitnessPlansIcon);
                    break;

                case 3:
                    UserWindowPagesFrame.NavigationService.Navigate(CaloriesCalculatorPageObject);
                    HighlightItem(CaloriesCalculatorTextBlock, CaloriesCalculatorIcon);
                    break;

                case 4:
                    HighlightItem(SettingsTextBlock, SettingsIcon);
                    UserWindowPagesFrame.NavigationService.Navigate(SettingsPageObject);
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
            pageTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
            pageIcon     .Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
            PageHeaderTextBlock.Text  = pageTextBlock.Text;
        }


        private void UserProfilePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindowPagesListBox.SelectedIndex = 4;
        }

        private void LogoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SigningWindow SigningWindowTemp = new SigningWindow();
            Close();
            SigningWindowTemp.Show();
        }

    }
}
