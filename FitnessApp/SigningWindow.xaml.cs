using System.Windows;


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
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow AppWindow = new UserMainWindow();
            AppWindow.Show();
            SigningWindowObject.Close();
        }


        private void CreateAnAccount_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SignUpPages.Page1.PageOneObject);
        }
    }
}
