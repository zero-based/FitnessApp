using FitnessApp.AdminWindowPages;
using FitnessApp.Models;
using FitnessApp.SQLdatabase;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FitnessApp.Windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public static AdminWindow AdminWindowObject;
        public static AdminModel signedInAdmin;


        // Declare AdminWindowPages Objects
        public static AdminHomePage       AdminHomePageObject;
        public static ChallengesSetupPage ChallengesSetupPageObject;
        public static AdminSettingsPage   AdminSettingsPageObject;


        public AdminWindow(int signedInAdminID)
        {
            InitializeComponent();
            AdminWindowObject = this;

            signedInAdmin = new AdminModel(signedInAdminID);

            // Initialize DataContext with signedInAdmin Model
            DataContext = signedInAdmin;

            ControlUpdateNewAdminPasswordGrid(signedInAdmin.ID);

            // Initialize AdminWindowPages Objects

            AdminHomePageObject       = new AdminHomePage();
            ChallengesSetupPageObject = new ChallengesSetupPage();
            AdminSettingsPageObject   = new AdminSettingsPage();

            // Initialize Listbox Selected Index
            AdminWindowPagesListBox.SelectedIndex = 0;

            // Intialize MessagesQueue and Assign it to MessagesSnackbar's MessageQueue
            var MessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            MessagesSnackbar.MessageQueue = MessagesQueue;
        }

        private void AdminWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Close Side Menu Drawer
            SideMenuDrawer.IsLeftDrawerOpen = false;

            // Navigate to the selected Page and Highlight the chosen Item
            switch (AdminWindowPagesListBox.SelectedIndex)
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
            pageTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
            pageIcon     .Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
            PageHeaderTextBlock .Text = pageTextBlock.Text;
        }

        private void ControlUpdateNewAdminPasswordGrid(int accountID)
        {
            if (SQLqueries.IsNewAdmin(accountID))
            {
                UpdateNewAdminPasswordGrid.Visibility = Visibility.Visible;

            }
            else
                UpdateNewAdminPasswordGrid.Visibility = Visibility.Collapsed;
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (SQLqueries.EncryptPassword(OldPasswordTextBox.Password) != signedInAdmin.Password)
            {
                AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
                OldPasswordTextBox.Password = "";
            }
            else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
            {
                AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation Mismatch!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }
            else if (NewPasswordTextBox.Password.Length < 7)
            {
                AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password must be more than 7 Charachters!");
                NewPasswordTextBox.Password = "";
                ConfirmNewPasswordTextBox.Password = "";
            }
            else
            {
                // Update signedInAdmin User Model
                signedInAdmin.Password = SQLqueries.EncryptPassword(NewPasswordTextBox.Password);

                // Update Admin's Password in database
                SQLqueries.UpdateAdminPassword(signedInAdmin);

                // Confirmation Message
                AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");

                // Hide UpdateNewAdminPasswordGrid
                UpdateNewAdminPasswordGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void LogoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SigningWindow SigningWindowTemp = new SigningWindow();
            Close();
            SigningWindowTemp.Show();
        }
    }

}
