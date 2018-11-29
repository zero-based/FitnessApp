using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace FitnessApp.SignUpPages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public static Page2 PageTwoObject = new Page2();

        public Page2()
        {
            InitializeComponent();
            PageTwoObject = this;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Page3.PageThreeObject);

            //Change Back Card Header
            SigningWindow.SigningWindowObject.BackArrowButton.Visibility = Visibility.Hidden;
            SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Text = "Set up your Profile";
            SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Margin = new Thickness(-15);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Page1.PageOneObject);
        }
    }
}
