using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using FitnessApp.AdminMainWindowPages;
using FitnessApp.SQLdatabase;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public static AdminMainWindow AdminMainWindowObject;
        SQLqueries SQLqueriesObject = new SQLqueries();

        // Declare AdminMainWindowPages Objects
        public static AdminHomePage       AdminHomePageObject;
        public static ChallengesSetupPage ChallengesSetupPageObject;
        public static AdminSettingsPage   AdminSettingsPageObject;


        public AdminMainWindow()
        {
            InitializeComponent();
            AdminMainWindowObject = this;
            ControlUpdateNewAdminPasswordGrid(-1);

            // Initialize AdminMainWindowPages Objects

            AdminHomePageObject       = new AdminHomePage();
            ChallengesSetupPageObject = new ChallengesSetupPage();
            AdminSettingsPageObject   = new AdminSettingsPage();

            // Initialize Listbox Selected Index
            AdminMainWindowPagesListBox.SelectedIndex = 0;

            // Intialize MessagesQueue and Assign it to MessagesSnackbar's MessageQueue
            var MessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            MessagesSnackbar.MessageQueue = MessagesQueue;
        }

        private void AdminMainWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Close Side Menu Drawer
            SideMenuDrawer.IsLeftDrawerOpen = false;

            // Navigate to the selected Page and Highlight the chosen Item
            switch (AdminMainWindowPagesListBox.SelectedIndex)
            {
                case 0:
                    AdminWindowPagesFrame.NavigationService.Navigate(AdminHomePageObject);
                    HighlightItem(HomeTextBlock, HomeIcon);
                    break;

                case 1:
                    AdminWindowPagesFrame.NavigationService.Navigate(ChallengesSetupPageObject);
                    HighlightItem(SetupChallengesTextBlock, ChallengesIcon);
                    break;

                case 2:
                    AdminWindowPagesFrame.NavigationService.Navigate(AdminSettingsPageObject);
                    HighlightItem(SettingsTextBlock, SettingsIcon);
                    break;
            }
        }

        public void HighlightItem(TextBlock pageTextBlock, MaterialDesignThemes.Wpf.PackIcon pageIcon)
        {
            // Set all Items' Foreground to Black 

            // Home Item
            HomeTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            HomeIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Setup Challenges Item
            SetupChallengesTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            ChallengesIcon          .Foreground = new SolidColorBrush(Colors.Black);

            // Settings Item
            SettingsTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            SettingsIcon     .Foreground = new SolidColorBrush(Colors.Black);

            // Highlight the required Item only and Change Page Header
            pageTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            pageIcon     .Foreground = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            PageHeaderTextBlock .Text = pageTextBlock.Text;
        }

        private void ControlUpdateNewAdminPasswordGrid(int accountID)
        {
            if (SQLqueriesObject.IsNewAdmin(accountID))
            {
                UpdateNewAdminPasswordGrid.Visibility = Visibility.Visible;
            }
            else
                UpdateNewAdminPasswordGrid.Visibility = Visibility.Collapsed;
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateNewAdminPasswordGrid.Visibility = Visibility.Collapsed;
        }

        private void LogoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SigningWindow SigningWindowTemp = new SigningWindow();
            Close();
            SigningWindowTemp.Show();
        }
    }

}
