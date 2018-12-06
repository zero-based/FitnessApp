using System.Windows;
using MaterialDesignThemes.Wpf;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SigningWindow : Window
    {
        public static SigningWindow SigningWindowObject;

        public SigningWindow()
        {
            InitializeComponent();
            SigningWindowObject = this;

            // Intialize ErrorMessagesQueue and Assign it to ErrorsSnackbar's MessageQueue
            var ErrorMessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            ErrorsSnackbar.MessageQueue = ErrorMessagesQueue;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            // Open User Main Window
            UserMainWindow UserMainWindowTemp = new UserMainWindow();
            UserMainWindowTemp.ShowDialog();

            // Open Admin Main Window
            //AdminMainWindow AdminMainWindowTemp = new AdminMainWindow();
            //AdminMainWindowTemp.ShowDialog();

            // Close Signing Window
            SigningWindowObject.Close();
        }


        private void CreateAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SignUpPages.SignUpFirstPage.SignUpFirstPageObject);
        }
    }
}
